using AutoMapper;
using Proyectos_API.Models;
using Proyectos_API.Models.Dto;

namespace Proyectos_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            CreateMap<Villa, VillaDtoCreate>().ReverseMap();
            CreateMap<Villa, VillaDtoUpdate>().ReverseMap();
        }
    }
}
