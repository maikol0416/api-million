
using System;
using Domain.Entities;
using ServiceApplication.Dto;
using AutoMapper;

namespace ServiceApplication 
{
    public static class OwnerMapper 
    {
            
            
    public static void Expresion (IMapperConfigurationExpression cnf)
    {
        cnf.CreateMap<Owner, OwnerDto>().ReverseMap();
    }
                            
    }
}