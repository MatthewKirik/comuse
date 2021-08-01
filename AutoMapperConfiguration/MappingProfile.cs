using AutoMapper;
using DataAccess.Entities;
using DataTransfer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperConfiguration
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserDTO>().ReverseMap();
            CreateMap<double, decimal>().ConvertUsing(d => Convert.ToDecimal(d));
            CreateMap<decimal, double>().ConvertUsing(d => Convert.ToDouble(d));
        }
    }
}
