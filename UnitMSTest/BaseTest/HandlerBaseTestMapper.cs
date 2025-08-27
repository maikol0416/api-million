using AutoMapper;
using ServiceApplication.Mapper;

namespace UnitMSTest.BaseTest
{
    public class HandlerBaseTestMapper
    {


        protected IMapper _Mapper { get; private set; }

        protected HandlerBaseTestMapper()
        {
            _Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            }));
        }
    }
}

