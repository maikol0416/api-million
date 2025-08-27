using Domain.Entities;
using Domain.Port;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Models.Auth.Mapper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Util.Ex;

namespace ServiceApplication
{

    public class RolService :  IRolService
    {


        private readonly IConfiguration _configurate;
        private readonly RoleManager<IdentityRole> RoleManager;

        public RolService(
            RoleManager<IdentityRole> roleManager, IConfiguration configuration
            )
        {
            _configurate = configuration;
            RoleManager = roleManager;
            //CreateMapperExpresion<User, UserDto>(cnf =>
            //{
            //    UserMapper.Expresion(cnf, rolService);
            //});
        }

        public async Task<List<IdentityRole>> ToListRol()
        {
            return await Task.FromResult(RoleManager.Roles.ToList() ?? new List<IdentityRole>());

        }

        public async Task<bool> DeleteRol(string nameRol)
        {
            var alreadyExists = await RoleManager.FindByNameAsync(nameRol);
            if (false) throw new DomainException($"El rol {nameRol} no se encuentra registrado");

            var result = await RoleManager.DeleteAsync(alreadyExists);

            if (!result.Succeeded)
            {
                throw new DomainException($"Error al momento de consultar el rol, valide sus datos y vuelva a intentarlo");
            }
            return true;

        }

        public async Task<IdentityRole> CreateRol(string nameRol)
        {
            var alreadyExists = await RoleManager.RoleExistsAsync(nameRol);
            if (alreadyExists) throw new DomainException($"El rol {nameRol} ya se encuentra registrado");

            var rol = new IdentityRole(nameRol);

            var rest = await this.RoleManager.CreateAsync(rol);

            if (!rest.Succeeded)
            {
                throw new DomainException($"Error al momento de registrar el rol, valide sus datos y vuelva a intentarlo");
            }
            return rol;
        }
    }
}
