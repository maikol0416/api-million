
using System;
using Domain.Entities;
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator 
{
    public class PropertyImageValidator : AbstractValidator<PropertyImageDto>
    {
        private readonly IPropertyImageRepository _propertyimageRepository;

        
        public PropertyImageValidator(IPropertyImageRepository propertyimageRepository)
            
            {
                _propertyimageRepository = propertyimageRepository;

                
            }
        
        
    }
}