using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles{
    public class GerenteProfile : Profile{
        public GerenteProfile(){
            CreateMap<CreateGerenteDto, Gerente>();
            CreateMap<Gerente, CreateGerenteDto>();
        }
    }
}