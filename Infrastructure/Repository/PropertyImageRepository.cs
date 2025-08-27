
using System;
using Infrastructure.Repository.Bases;
using Domain.Entities;
using Domain.Port;

namespace Infraestructure 
{
    public class PropertyImageRepository : RepositoryBaseSQLServer<PropertyImage>, IRepositoryBase<PropertyImage>, IPropertyImageRepository
    {
        

        
        public PropertyImageRepository(IMainContextSQLServer mainContext)
            : base(mainContext)
            {
                

                
            }
        
        
    }
}