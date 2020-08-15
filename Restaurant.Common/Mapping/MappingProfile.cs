using Restaurant.Common.DTO.Requests;
using Restaurant.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Restaurant.Common.DTO.Responses;

namespace Restaurant.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuEntity, MenuResponseDTO>();
            CreateMap<MenuCardEntity, MenuCardResponseDTO>();
            CreateMap<MenuCardEntity, MenuCardWithMenuResponseDTO>();
        }

    }
}