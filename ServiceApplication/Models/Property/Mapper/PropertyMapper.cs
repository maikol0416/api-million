
using System;
using Domain.Entities;
using ServiceApplication.Dto;
using AutoMapper;

namespace ServiceApplication 
{
    public static class PropertyMapper 
    {
            
            
    public static void Expresion (IMapperConfigurationExpression cnf)
    {
        cnf.CreateMap<Property, PropertyDto>().ReverseMap();
    }
                            
    }
}