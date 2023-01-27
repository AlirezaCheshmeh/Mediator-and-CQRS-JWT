using AutoMapper;
using mediatorCqrs.Application.DTOs;
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
            CreateMap<CreateUserDtos , User>();
        }
    }
}
