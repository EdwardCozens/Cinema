using AutoMapper;
using Cinema.DataAccess.Models;

namespace Cinema.DTOs.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<Movie, MovieGetDTO>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Genre, GenrePostDTO>().ReverseMap();
        }
    }
}
