
using System;
using Infrastructure.Repository.Bases;
using Domain.Entities;
using Domain.Port;

namespace Infraestructure 
{
    public class OwnerRepository : RepositoryBaseSQLServer<Owner>, IRepositoryBase<Owner>, IOwnerRepository
    {
        

        
        public OwnerRepository(IMainContextSQLServer mainContext)
            : base(mainContext)
            {
                

                
            }
        
        
    }
}