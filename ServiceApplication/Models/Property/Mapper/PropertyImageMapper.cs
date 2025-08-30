
using System;
using Domain.Entities;
using ServiceApplication.Dto;
using AutoMapper;

namespace ServiceApplication 
{
    public static class PropertyImageMapper 
    {
            
            
    public static void Expresion (IMapperConfigurationExpression cnf)
    {
        cnf.CreateMap<PropertyImage, PropertyImageDto>().ReverseMap();
    }
                            
    }
}