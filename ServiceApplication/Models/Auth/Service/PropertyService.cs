
using System;
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using Util.Common;

namespace ServiceApplication 
{
    public class PropertyService : BaseServiceApplication<Property,PropertyDto>, IBaseServiceApplication<Property,PropertyDto>, IPropertyService
    {
        

        
        public PropertyService(IPropertyRepository propertyRepository,IUtil util)
            : base(propertyRepository,util)
            {
                

                
                CreateMapperExpresion<Property, PropertyDto>(cnf =>
                {
                    PropertyMapper.Expresion(cnf);
                });        
            
                    
            }
        
        
    }
}