
using System;
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using Util.Common;

namespace ServiceApplication 
{
    public class OwnerService : BaseServiceApplication<Owner,OwnerDto>, IBaseServiceApplication<Owner,OwnerDto>, IOwnerService
    {
        

        
        public OwnerService(IOwnerRepository ownerRepository,IUtil util)
            : base(ownerRepository,util)
            {
                

                
                CreateMapperExpresion<Owner, OwnerDto>(cnf =>
                {
                    OwnerMapper.Expresion(cnf);
                });        
            
                    
            }
        
        
    }
}