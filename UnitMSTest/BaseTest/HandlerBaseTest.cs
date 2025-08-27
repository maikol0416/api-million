using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Base;

namespace UnitMSTest.BaseTest
{
    [TestClass]
    public partial class HandlerBaseTest<ENT, DTO>
        where ENT : class, new()
        where DTO : class, new()
    {
        //      public  Mock<IRepositoryBase<ENT>> _mockRepository = new();

        //public TService _service;

        //private IList<ENT> _entities;

        //public HandlerBaseTest(IList<ENT> entities)
        //{
        //	_entities = entities;
        //}

        //[TestMethod]
        //public  async Task CreateAsyncTest()
        //{
        //	ENT entity = _entities.FirstOrDefault();
        //	_mockRepository.Setup(s => s.CreateModel(entity)).ReturnsAsync(entity);

        //	_service = new BaseServiceApplication<Test>(_mockRepository.,Mapeo);


        //	await Assert.AreEqual<ENT>(entity, Ma);
        //}


    }
}

