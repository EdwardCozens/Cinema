using AutoMapper;
using Cinema.DataAccess.Models;

namespace Cinema.DTOs.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();
        }
    }
}
