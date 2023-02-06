using AutoMapper;
using FilmeApi.Data.DTOS;
using FilmeApi.Model;

namespace FilmeApi.Profiles
{
    public class FilmeProfile : Profile
    {

        public FilmeProfile() {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme, UpdateFilmeDto>();
        }
    }
}
