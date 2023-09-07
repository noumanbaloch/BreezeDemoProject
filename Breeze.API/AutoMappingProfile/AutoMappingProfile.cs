using AutoMapper;
using Breeze.Models.Dtos.User.Request;
using Breeze.Models.Entities;

namespace Breeze.API.AutoMappingProfile
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            #region Create methods for mapping.
            ConfigureDtoToDtoMapping();
            ConfigureDtoToEntityMapping();
            ConfigureEntityToDtoMapping();
            ConfigureEntityToEntityMapping();
            #endregion
            
        }

        private void ConfigureDtoToDtoMapping()
        {
        }

        private void ConfigureDtoToEntityMapping()
        {
            CreateMap<RegisterUserRequestDto, UserEntity>().ReverseMap();
        }

        private void ConfigureEntityToDtoMapping()
        {
        }

        private void ConfigureEntityToEntityMapping()
        {
        }
    }
}
