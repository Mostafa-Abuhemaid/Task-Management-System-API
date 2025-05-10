using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.CategoryDTO;
using Web.Application.DTOs.UserDTO;
using Web.Domain.Entites;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, GetGategoryDto>();

            CreateMap<AppUser, UserDto>();

        }
    }
}
