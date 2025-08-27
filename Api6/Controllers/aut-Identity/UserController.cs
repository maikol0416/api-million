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
    public class UserController : HandlerBaseLiteController<UserDto>
    {
        private readonly IUserService _seguridadService;

        public UserController(IMediator mediator, IUserService service)
            : base(mediator)
        {
            this._seguridadService = service;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login login) => this.HandlerResponse(await this._seguridadService.Login(login));


        [AllowAnonymous]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> RegistrarUsuario(User registro)
        {
            return this.HandlerResponse(await this._seguridadService.RegistrarUsuario(registro));
        }

        [Authorize]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> ActualizarUsuario(UserApplication usuario)
        {
            return this.HandlerResponse(await this._seguridadService.UpdateUser(usuario));
        }

        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> EliminarUsuario(string user)
        {
            return this.HandlerResponse(await this._seguridadService.DeleteUser(user));
        }
    }
}
    