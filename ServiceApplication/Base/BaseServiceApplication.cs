using AutoMapper;
using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.Port;
using Domain.ValueObject;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Util.Common;

namespace ServiceApplication.Base
{
    public abstract partial class BaseServiceApplication<ENT, DTO> : IBaseServiceApplication<ENT, DTO>
        where ENT : class, new()
        where DTO : class, new()
    {




        public IRepositoryBase<ENT> RepositoryBase { get; set; }
        private MapperConfigurationExpression configurationmapper;
        private IMapper Mapper;
        protected readonly IUtil _util;

        public BaseServiceApplication(IRepositoryBase<ENT> repositoryBase, IUtil util)
        {
            RepositoryBase = repositoryBase;
            _util = util;
        }
        public BaseServiceApplication()
        {

        }

        /// <summary>
        /// crear una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<DTO> CreateModel(DTO dto)
        {
            await this.ValidadorNegocio(dto);
            var entity = MapToENT<ENT, DTO>(dto);
            return MapToDTO<ENT, DTO>(await RepositoryBase.CreateModel(entity));
        }

        /// <summary>
        /// crear lista de entidades
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<bool> CreateModels(List<DTO> dtos)
        {
            var entities = MapLstToENT<ENT, DTO>(dtos);
            await RepositoryBase.CreateModels(entities);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Lista de todos los registros
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<DTO>> TolistModel()
        {
            var client = _util.GetHeaderRequest(EHeaders.CodeClient);
            var dtos = await RepositoryBase.TolistModel();
            return MapLstToDTO<ENT, DTO>(dtos);

        }
        /// <summary>
        /// Consultar el Registro por ID
        /// </summary>
        /// <returns></returns>
        public virtual async Task<DTO> GetById(int id)
        {
            return MapToDTO<ENT, DTO>(await RepositoryBase.GetById(id));

        }
        /// <summary>
        /// Eliminar modelo por un ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteModel(int id)
        {

            return await RepositoryBase.DeleteModel(id);
        }

        /// <summary>
        /// Eliminar modelo por un ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<DTO> SearchModel(string property, string value)
        {
            var dtos = await this.TolistModel();
            var dto = dtos.FirstOrDefault(w => GetPropertyValue(property, w).Contains(value));
            return dto;
        }

        /// <summary>
        /// Eliminar modelo por un ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<List<DTO>> SearchListModel(string property, string value)
        {
            var dtos = await this.TolistModel();
            var dto = dtos.Where(w => GetPropertyValue(property, w).Contains(value)).ToList();
            return dto;
        }

        /// <summary>
        /// Actualizar entidad generico
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<DTO> UpdateModel(DTO dto)
        {
            return MapToDTO<ENT, DTO>(await RepositoryBase.UpdateModel(MapToENT<ENT, DTO>(dto)));
        }

        /// <summary>
        /// Actualizar entidad generico
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual async Task<ENT> UpdateModel(ENT ent)
        {
            return await RepositoryBase.UpdateModel(ent);
        }

        /// <summary>
        /// Lista de registros basado en una condición
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<List<ENT>> ToListModelBy(Expression<Func<ENT, bool>> expression)
        {
            return await RepositoryBase.ToListModelBy(expression);
        }

        /// <summary>
        /// Lista de registros basado en una condición
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<List<DTO>> ToListModelDtoBy(Expression<Func<ENT, bool>> expression)
        {
            return MapLstToDTO<ENT, DTO>(await RepositoryBase.ToListModelBy(expression));
        }

        /// <summary>
        /// Primero por defecto por expresión
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<ENT> FirstOrDefautlModelBy(Expression<Func<ENT, bool>> expression)
        {
            return await RepositoryBase.FirstOrDefautlModelBy(expression);
        }


        /// <summary>
        /// Validacion de reglas de negocio trans
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task ValidadorNegocio(DTO dto)
        {
            if (dto is null)
            {
                throw new Exception(typeof(ENT).Name + " no puede ser nula");
            }
            await Task.FromResult(0);
        }



        /// <summary>
        /// Metodo para sincronizae datos
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> SyncData(List<DTO> entities)
        {
            await ValidateSyncData(entities);
            await CreateModels(entities);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Reglas de negocio para cada entidad a sincronizar
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task ValidateSyncData(List<DTO> entities)
        {
            foreach (var item in entities)
            {
                await ValidadorNegocio(item);
            }
        }


        public virtual async Task<Paginate<DTO>> Paginate(int pagina, int Count)
        {
            return MapToDTO<Paginate<ENT>, Paginate<DTO>>(await RepositoryBase.Paginate(pagina, Count));

        }

        public virtual async Task<Paginate<DTO>> Paginate(Paginate<DTO> paginado)
        {
            var client = _util.GetHeaderRequest(EHeaders.CodeClient);
            if (paginado.Filters is null)
                paginado.Filters = new List<FilterPaginate>();
            return MapToDTO<Paginate<ENT>, Paginate<DTO>>(await RepositoryBase.Paginate(MapToENT<Paginate<ENT>, Paginate<DTO>>(paginado)));

        }

        protected string GetPropertyValue(string NameProperty, DTO obj)
        {
            return obj.GetType().GetProperty(NameProperty).GetValue(obj, null).ToString();
        }

        protected string GetPropertyValue(string NameProperty, ENT obj)
        {
            return obj.GetType().GetProperty(NameProperty).GetValue(obj, null).ToString();
        }

        protected ENT SetPropertyValue<v>(string NameProperty, ENT obj, v value)
        {
            obj.GetType().GetProperty(NameProperty).SetValue(obj, value);
            return obj;
        }

        protected DTO SetPropertyValue<v>(string NameProperty, DTO obj, v value)
        {
            obj.GetType().GetProperty(NameProperty).SetValue(obj, value);
            return obj;
        }

        protected void CreateMapper()
        {
            MapperConfiguration cnfMapper = new(configurationmapper);
            Mapper = cnfMapper.CreateMapper();
        }
        /// <summary>
        /// Registro de reglas de mapeo de objetos de DTO o Entity
        /// </summary>
        /// <typeparam name="ENT">TSource conversion</typeparam>
        /// <typeparam name="DTO">TDestination conversion</typeparam>
        protected void CreateMapper<ENT, DTO>() where ENT : class, new() where DTO : class, new()
        {
            configurationmapper = CreateConfiguration<ENT, DTO>();
            CreateMapper();
        }

        public void CreateMapperExpresion<ENT, DTO>(Action<IMapperConfigurationExpression> configure) where ENT : class, new() where DTO : class, new()
        {
            configurationmapper = CreateConfiguration<ENT, DTO>();
            CreateMapperExpresion(configure);
        }

        public void CreateMapperExpresion(Action<IMapperConfigurationExpression> configure)
        {
            configure(configurationmapper);
            CreateMapper();
        }

        private static MapperConfigurationExpression CreateConfiguration<ENT, DTO>() where ENT : class, new() where DTO : class, new()
        {
            var cnf = new MapperConfigurationExpression
            {
                AllowNullCollections = true
            };
            cnf.CreateMap<ENT, DTO>();
            cnf.CreateMap<DTO, ENT>();
            cnf.CreateMap<Paginate<ENT>, Paginate<DTO>>();
            cnf.CreateMap<Paginate<DTO>, Paginate<ENT>>();
            cnf.CreateMap<ValueObjectString, string>().ConvertUsing(n => n.Value);
            cnf.CreateMap<NameValueObject, string>().ConvertUsing(n => n.Value);
            cnf.CreateMap<DateTimeOffset, DateTime>().ConvertUsing(n => n.UtcDateTime);
            cnf.CreateMap<DateTime, DateTimeOffset>().ConvertUsing(n => DateTime.SpecifyKind(n, DateTimeKind.Utc));
            return cnf;

        }
        /// <summary>
        /// Metodo para mapear de entidad a DTO
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DTO MapToDTO<ENT, DTO>(ENT entity)
        {
            return Mapper.Map<DTO>(entity);
        }

        /// <summary>
        /// Metodo para mapear de DTO a entidad
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ENT MapToENT<ENT, DTO>(DTO dto)
        {
            return Mapper.Map<ENT>(dto);
        }

        /// <summary>
        /// Metodo para mapear de entidad a DTO
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<DTO> MapLstToDTO<ENT, DTO>(List<ENT> entity)
        {
            return Mapper.Map<List<DTO>>(entity);
        }

        /// <summary>
        /// Metodo para mapear de DTO a entidad
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ENT> MapLstToENT<ENT, DTO>(List<DTO> dto)
        {
            return Mapper.Map<List<ENT>>(dto);
        }

        public async Task<bool> DeleteModel(string id)
        {
            return await RepositoryBase.DeleteModel(id);
        }

        public async Task<DTO> GetById(string id)
        {
            return MapToDTO<ENT, DTO>(await RepositoryBase.GetById(id));
        }
    }
}
