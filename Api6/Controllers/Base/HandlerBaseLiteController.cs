using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Util.Common;

namespace Api.Base
{
    public class HandlerBaseLiteController<DTO> : Controller
    {
        protected readonly IMediator _mediator;

        public HandlerBaseLiteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected IActionResult HandlerResponse<DTO>(DTO dato)
        {
            return this.Ok(new ResponseApi<DTO> { Data = dato, Status = true, Message = "Operation carried out successfully." });
        }
    }
}
