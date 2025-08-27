
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
    public class OwnerController : HandlerBaseController<Owner,OwnerDto>
    {
        

        
        public OwnerController(IValidator<OwnerDto> validator,IMediator mediator)
            : base(validator,mediator)
            {
                

                
            }
        
        
    }
}