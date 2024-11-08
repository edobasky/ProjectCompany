﻿using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForCtorParam("FulllAddress",
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Address)));
        }
    }
}
