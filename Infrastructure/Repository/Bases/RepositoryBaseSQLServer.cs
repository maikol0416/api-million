using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Port;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Util.Common;

namespace Infrastructure.Repository.Bases
{
    public class RepositoryBaseSQLServer<T> : IRepositoryBase<T>
            where T : BaseEntitySQLServer, new()
    {
        public IMainContextSQLServer MainContext { get; set; }
        protected DbSet<T> entity;

        public RepositoryBaseSQLServer(IMainContextSQLServer mainContext)
        {
            MainContext = mainContext;
            entity = MainContext.Set<T>();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await entity.FindAsync(id);
        }

        public virtual async Task<List<T>> TolistModel()
        {
            return await entity.ToListAsync();
        }

        public virtual async Task<bool> DeleteModel(int id)
        {
            bool returnDelete = false;

            T obj = await GetById(id);

            if (obj != null)
            {
                entity.Remove(obj);
                returnDelete = await MainContext.SaveChangesAsync()>0;
            }

            return returnDelete;
        }

        public virtual async Task<T> CreateModel(T obj)
        {
            await entity.AddAsync(obj);
            await MainContext.SaveChangesAsync();
            return obj;
        }

        public virtual async Task CreateModels(List<T> listObjs)
        {
            await entity.AddRangeAsync(listObjs);
            await MainContext.SaveChangesAsync();
        }

        public virtual async Task<T> UpdateModel(T obj)
        {
            SetPropertyValue("DateLastUpdate", obj, DateTime.UtcNow.AddHours(-6));
            entity.Update(obj);
            int result = await MainContext.SaveChangesAsync();
            return obj;
        }

        public async Task<List<T>> ToListModelBy(Expression<Func<T, bool>> expression)
        {
            return await entity.Where(expression).ToListAsync();
        }

        public async Task<T> FirstOrDefautlModelBy(Expression<Func<T, bool>> expression)
        {
            return await entity.Where(expression).SingleOrDefaultAsync();
        }

        public virtual async Task<Paginate<T>> Paginate(int pagina, int tamaño)
        {
            return await Paginator<T>.Paginate(entity.AsQueryable(), pagina, tamaño);
        }

        public virtual async Task<Paginate<T>> Paginate(Paginate<T> paginadoDto)
        {
            IQueryable<T> Listabase;
            if (paginadoDto.Filters != null && paginadoDto.Filters.Count > 0)
            {
                Listabase = ConfigurateFilters(entity, paginadoDto);

            }
            else
            {
                Listabase = entity.AsQueryable<T>() ;
            }

            return await Paginator<T>.Paginate(Listabase, paginadoDto.Page, paginadoDto.Count);
        }

        protected IQueryable<T> ConfigurateFilters(IQueryable<T> query, Paginate<T> paginate)
        {
            if (paginate != null)
            {
                query = ConfigureOrderBy(query, paginate);
                query = ConfigureBaseOfList(query, paginate);
            }
            return query;
        }

        private IQueryable<T> ConfigureOrderBy(IQueryable<T> query, Paginate<T> paginate)
        {
            query = ConfigureValueObject(query, paginate);

            if (paginate.Orders.IsNotNull())
            {
                query = query.OrderByDynamic(paginate.Orders);
            }
            return query;
        }

        protected virtual IQueryable<T> ConfigureValueObject(IQueryable<T> query, Paginate<T> paginate)
        {
            if (paginate != null)
            {
                List<string> list = new();
                Action<Type, string, List<string>> listDel = (x, y, l) => l.AddRange((from asm in x.GetProperties()
                                                                                      where asm.MemberType == System.Reflection.MemberTypes.Property && asm.PropertyType.BaseType != null
                                                                                      && (asm.PropertyType.BaseType.Name.Contains(nameof(ValueObjectString)))
                                                                                      select y.ToLower() + asm.Name.ToLower())?.ToList());
                listDel(typeof(T), "", list);

                typeof(T).GetProperties()
                    .Where(a => a.PropertyType.BaseType != null).ToList()
                    .ForEach(a =>
                    {
                        a.PropertyType.GetProperties()
                            .Where(y => y.PropertyType.BaseType != null).ToList()
                            .ForEach(y => listDel(y.PropertyType, $"{a.Name}.{y.Name}.", list));
                        listDel(a.PropertyType, $"{a.Name}.", list);
                    });

                if (list.Count > 0)
                {
                    if (paginate.Filters != null && paginate.Filters.Any(x => list.Any(a => a == x.Property.ToLower())))
                        paginate.Filters?.Where(x => list.Any(a => a == x.Property.ToLower())).ToList().ForEach(item => item.Property += "." + nameof(ValueObjectString));

                    if (paginate.Orders != null && paginate.Orders.Any(x => list.Any(a => a == x.Name.ToLower())))
                    {
                        var listSort = paginate.Orders.Where(x => list.Any(a => a == x.Name.ToLower())).ToList();
                        for (int i = 0; i < listSort.Count; i++)
                        {
                            listSort[i] = new() {
                                Name = $"{listSort[i].Name}.{nameof(ValueObjectString.Value)}",
                                Direction = listSort[i].Direction
                            };
                        }
                        paginate.Orders.RemoveAll(x => list.Any(a => a == x.Name.ToLower()));
                        paginate.Orders.AddRange(listSort);
                    }
                }
            }
            return query;
        }

        protected virtual IQueryable<T> ConfigureBaseOfList(IQueryable<T> query, Paginate<T> paginate)
        {
            if (paginate != null)
            {
                query = ConfigureValueObject(query, paginate);
                query = ConfigureFilter(query, paginate);
            }
            return query;
        }

        protected virtual IQueryable<T> ConfigureFilter(IQueryable<T> query, Paginate<T> paginate)
        {
            if (paginate.Filters != null && !paginate.Filters.Select(a => a.Value).ToList().Any(x=>x == null))
            {
                query = query.WhereDynamic(paginate.Filters);
            }
            return query;
        }

        private string GetPropertyValue(string NameProperty, T obj)
        {
            return obj.GetType().GetProperty(NameProperty).GetValue(obj, null).ToString();
        }

        private T SetPropertyValue<v>(string NameProperty, T obj, v value)
        {
            obj.GetType().GetProperty(NameProperty).SetValue(obj, value);
            return obj;
        }
        public async Task<bool> Exist(Expression<Func<T, bool>> expression)
        {
            var result = await entity.Where(expression).ToListAsync();
            return result.Count() > 0;
        }

        public async Task<T> GetById(string id)
        {
            return await entity.FirstOrDefaultAsync(f=>f.Code==id);
        }

        public async Task<bool> DeleteModel(string id)
        {
            bool returnDelete = false;

            T obj = await GetById(id);

            if (obj != null)
            {
                entity.Remove(obj);
                returnDelete = await MainContext.SaveChangesAsync() > 0;
            }

            return returnDelete;
        }
    }
}

