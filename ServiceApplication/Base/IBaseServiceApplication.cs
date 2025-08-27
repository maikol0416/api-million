using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Util.Common;

namespace ServiceApplication
{
    public interface IBaseServiceApplication<ENT, DTO>
        where ENT : class, new()
         where DTO : class, new()
    {
        Task<DTO> CreateModel(DTO dto);

        Task<bool> CreateModels(List<DTO> dtos);

        Task<List<DTO>> TolistModel();

        Task<bool> DeleteModel(int id);
        Task<bool> DeleteModel(string id);

        Task<DTO> SearchModel(string property, string value);

        Task<List<DTO>> SearchListModel(string property, string value);

        Task<DTO> UpdateModel(DTO entity);

        Task<DTO> GetById(int id);
        Task<DTO> GetById(string id);

        Task<List<ENT>> ToListModelBy(Expression<Func<ENT, bool>> expression);

        Task<List<DTO>> ToListModelDtoBy(Expression<Func<ENT, bool>> expression);

        Task<ENT> FirstOrDefautlModelBy(Expression<Func<ENT, bool>> expression);

        Task<Paginate<DTO>> Paginate(int pagina, int Count);

        Task<Paginate<DTO>> Paginate(Paginate<DTO> paginado);


        void CreateMapperExpresion(Action<IMapperConfigurationExpression> configure);

        DTO MapToDTO<ENT, DTO>(ENT entity);

        ENT MapToENT<ENT, DTO>(DTO dto);

        List<DTO> MapLstToDTO<ENT, DTO>(List<ENT> entity);

        List<ENT> MapLstToENT<ENT, DTO>(List<DTO> dto);

    }
}
