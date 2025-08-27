using Api.Base;
using Api6.Common;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using Utilidades;

namespace Api.Controllers
{
    [AllowAnonymous]
    [Route(ConstantsAPI.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class RolController : HandlerBaseLiteController<UserDto>
    {
        private readonly IRolService _seguridadService;

        public RolController(IMediator mediator, IRolService service)
            : base(mediator)
        {
            this._seguridadService = service;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("createRol")]
        public async Task<IActionResult> RegistrarRol(string rol)
        {
            return this.HandlerResponse(await this._seguridadService.CreateRol(rol));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("listRoles")]
        public async Task<IActionResult> ObtenerRoles()
        {
            return this.HandlerResponse(await this._seguridadService.ToListRol());
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteRol")]
        public async Task<IActionResult> EliminarRol(string rol)
        {
            return this.HandlerResponse(await this._seguridadService.DeleteRol(rol));
        }
    }
}
