
using System;
using Domain.Entities;
using ServiceApplication.Dto;
using AutoMapper;

namespace ServiceApplication 
{
    public static class PropertyTraceMapper 
    {
            
            
    public static void Expresion (IMapperConfigurationExpression cnf)
    {
        cnf.CreateMap<PropertyTrace, PropertyTraceDto>().ReverseMap();
    }
                            
    }
}