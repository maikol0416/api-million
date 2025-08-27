using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Models.Auth.Mapper;
using Util.Common;

namespace ServiceApplication
{
    public class TestService : BaseServiceApplication<Test, TestDto>, IBaseServiceApplication<Test, TestDto>, ITestService
    {
        public TestService(ITestRepository repositorioBase,IUtil util)
            : base(repositorioBase, util)
        {
            CreateMapperExpresion<Test, TestDto>(cnf =>
            {
                TestMapper.Expresion(cnf);
            });
        } 
    }
}
