
using System;
using Domain.Entities;
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator 
{
    public class OwnerValidator : AbstractValidator<OwnerDto>
    {
        private readonly IOwnerRepository _ownerRepository;

        
        public OwnerValidator(IOwnerRepository ownerRepository)
            
            {
                _ownerRepository = ownerRepository;

                
            }
        
        
    }
}