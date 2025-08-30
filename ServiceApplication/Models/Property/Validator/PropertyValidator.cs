
using System;
using Domain.Entities;
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator 
{
    public class PropertyValidator : AbstractValidator<PropertyDto>
    {
        private readonly IPropertyRepository _propertyRepository;

        
        public PropertyValidator(IPropertyRepository propertyRepository)
            
            {
                _propertyRepository = propertyRepository;

                
            }
        
        
    }
}