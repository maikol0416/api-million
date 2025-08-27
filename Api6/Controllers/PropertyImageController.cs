
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
    public class PropertyImageController : HandlerBaseController<PropertyImage,PropertyImageDto>
    {
        

        
        public PropertyImageController(IValidator<PropertyImageDto> validator,IMediator mediator)
            : base(validator,mediator)
            {
                

                
            }
        
        
    }
}