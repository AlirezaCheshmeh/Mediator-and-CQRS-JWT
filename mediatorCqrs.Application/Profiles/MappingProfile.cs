using AutoMapper;
using mediatorCqrs.Application.DTOs;
using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Application.DTOs.Referesh;
using mediatorCqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User , UserDtos>().ReverseMap();
            CreateMap<UserDtos , User>();
            CreateMap<CreateUserDtos , User>();


            // Mapping Two Model in One Data Transfer Object 
            // Mean Get Data Include From Model And AutoMap in 
            //DataTransferObject Cs File 
            //==========>
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Refreshtoken, RefreshTokenDTO>()
                .ForMember(dst => dst.customerDTO, opt => opt.MapFrom(src => src.customer))
                .ReverseMap()
                .ReverseMap();
            //==========>



        }
    }
}
