
using System;
using Domain.Entities;
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator 
{
    public class PropertyTraceValidator : AbstractValidator<PropertyTraceDto>
    {
        private readonly IPropertyTraceRepository _propertytraceRepository;

        
        public PropertyTraceValidator(IPropertyTraceRepository propertytraceRepository)
            
            {
                _propertytraceRepository = propertytraceRepository;

                
            }
        
        
    }
}