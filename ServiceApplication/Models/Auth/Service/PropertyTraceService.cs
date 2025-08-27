
using System;
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using Util.Common;

namespace ServiceApplication 
{
    public class PropertyTraceService : BaseServiceApplication<PropertyTrace,PropertyTraceDto>, IBaseServiceApplication<PropertyTrace,PropertyTraceDto>, IPropertyTraceService
    {
        

        
        public PropertyTraceService(IPropertyTraceRepository propertytraceRepository,IUtil util)
            : base(propertytraceRepository,util)
            {
                

                
                CreateMapperExpresion<PropertyTrace, PropertyTraceDto>(cnf =>
                {
                    PropertyTraceMapper.Expresion(cnf);
                });        
            
                    
            }
        
        
    }
}