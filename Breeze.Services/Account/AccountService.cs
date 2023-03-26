using AutoMapper;
using Breeze.DbCore.UnitOfWork;
using Breeze.Models.Constants;
using Breeze.Models.Dtos.User.Request;
using Breeze.Models.Dtos.User.Response;
using Breeze.Models.Dtos.User.SP;
using Breeze.Models.Entities;
using Breeze.Models.GenericResponses;
using Breeze.Services.Token;
using Breeze.Utilities;
using Dapper;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Breeze.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<GenericResponse<UserResponseDto>> RegisterUser(RegisterUserRequestDto requestDto)
        {
            if (UserExist(requestDto.UserName))
            {
                return GenericResponse<UserResponseDto>.Failure(ApiResponseMessage.RecordAlreadyExist, ApiStatusCode.RecordAlreadyExist);
            }

            var user = _mapper.Map<UserEntity>(requestDto);

            using var hmac = new HMACSHA512();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestDto.Password));
            user.PasswordSalt = hmac.Key;
            user.CreatedBy = Username.SystemGenerated;
            user.CreatedDate = Helper.GetCurrentDate();
            user.ModifiedBy = Username.SystemGenerated;

            var userRepo = _unitOfWork.GetRepository<UserEntity>();

            userRepo.Add(user);

            await _unitOfWork.CommitAsync();

            var responseDto = new UserResponseDto()
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

            return GenericResponse<UserResponseDto>.Success(responseDto, ApiResponseMessage.RecordSavedSuccessfully, ApiStatusCode.RecordSavedSuccessfully);

        }

        public async Task<GenericResponse<UserResponseDto>> LoginUser(LoginUserRequestDto requestDto)
        {
            var user = await _unitOfWork.GetRepository<UserEntity>().FindByFirstOrDefaultAsync
                (x => x.UserName.ToLower() == requestDto.UserName && x.Deleted == false);


            if (Helper.IsNullOrEmpty(user))
            {
                return GenericResponse<UserResponseDto>.Failure(ApiResponseMessage.InvalidUserName, ApiStatusCode.InvalidUserName);
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestDto.Password));

            for(int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i])
                {
                    return GenericResponse<UserResponseDto>.Failure(ApiResponseMessage.InvalidPassword, ApiStatusCode.InvalidPassword);
                }
            }

            var response = new UserResponseDto()
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

             return GenericResponse<UserResponseDto>.Success(response, ApiResponseMessage.SuccessfullLogin, ApiStatusCode.SuccessfullLogin);
        }

        private bool UserExist(string userName)
        {
            DynamicParameters parameters = new();
            parameters.Add("@Username", userName, DbType.String, direction: ParameterDirection.Input);
            var result = _unitOfWork.DapperSpSingleWithParams<UserSPDto>("exec " + StoreProcedureName.GetUserStoreProcedure + " @Username", parameters);
            return result != null;
        }


    }
}
