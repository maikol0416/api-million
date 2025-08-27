
using System;
using Api.Base;
using Api6.Common;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.Dto;

namespace Api.Controllers 
{
    [Route(ConstantsAPI.UriForDefaultWebApi+"[controller]")]
    [ApiController]
    public class PropertyController : HandlerBaseController<Property,PropertyDto>
    {
        

        
        public PropertyController(IValidator<PropertyDto> validator,IMediator mediator)
            : base(validator,mediator)
            {
                

                
            }
        
        
    }
}