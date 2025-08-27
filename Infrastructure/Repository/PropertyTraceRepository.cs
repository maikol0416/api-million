
using System;
using Infrastructure.Repository.Bases;
using Domain.Entities;
using Domain.Port;

namespace Infraestructure 
{
    public class PropertyTraceRepository : RepositoryBaseSQLServer<PropertyTrace>, IRepositoryBase<PropertyTrace>, IPropertyTraceRepository
    {
        

        
        public PropertyTraceRepository(IMainContextSQLServer mainContext)
            : base(mainContext)
            {
                

                
            }
        
        
    }
}