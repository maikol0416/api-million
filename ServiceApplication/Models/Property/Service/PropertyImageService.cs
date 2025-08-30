
using System;
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using Util.Common;

namespace ServiceApplication 
{
    public class PropertyImageService : BaseServiceApplication<PropertyImage,PropertyImageDto>, IBaseServiceApplication<PropertyImage,PropertyImageDto>, IPropertyImageService
    {
        

        
        public PropertyImageService(IPropertyImageRepository propertyimageRepository,IUtil util)
            : base(propertyimageRepository,util)
            {
                

                
                CreateMapperExpresion<PropertyImage, PropertyImageDto>(cnf =>
                {
                    PropertyImageMapper.Expresion(cnf);
                });        
            
                    
            }
        
        
    }
}