using AutoMapper;
using Breeze.Models.Dtos.User.Request;
using Breeze.Models.Entities;

namespace Breeze.API.AutoMappingProfile
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<RegisterUserRequestDto, UserEntity>();
        }
    }
}
