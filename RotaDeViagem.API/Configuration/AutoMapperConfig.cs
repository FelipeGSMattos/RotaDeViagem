using AutoMapper;
using RotaDeViagem.API.ViewModels;
using RotaDeViagem.Domain.Entities;

namespace RotaDeViagem.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Rota, RotaViewModel>().ReverseMap();
        }
    }
}
