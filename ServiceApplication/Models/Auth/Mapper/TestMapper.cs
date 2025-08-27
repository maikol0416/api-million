using System;
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Models.Auth.Mapper
{
    public static class TestMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {

            cnf.CreateMap<Test, TestDto>();
            cnf.CreateMap<TestDto, Test>();


        }
    }
}
