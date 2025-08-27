
using System;
using Infrastructure.Repository.Bases;
using Domain.Entities;
using Domain.Port;

namespace Infraestructure 
{
    public class PropertyRepository : RepositoryBaseSQLServer<Property>, IRepositoryBase<Property>, IPropertyRepository
    {
        

        
        public PropertyRepository(IMainContextSQLServer mainContext)
            : base(mainContext)
            {
                

                
            }
        
        
    }
}