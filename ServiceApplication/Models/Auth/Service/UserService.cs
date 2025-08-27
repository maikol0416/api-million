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

    public class UserService :  IUserService
    {


        private readonly IConfiguration _configurate;
        private readonly UserManager<UserApplication> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public UserService(UserManager<UserApplication> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration
            )
        {
            _configurate = configuration;
            UserManager = userManager;
            RoleManager = roleManager;
            //CreateMapperExpresion<User, UserDto>(cnf =>
            //{
            //    UserMapper.Expresion(cnf, rolService);
            //});
        }

        public async Task<UserApplication> RegistrarUsuario(User registro)
        {
            var userExists = await UserManager.FindByNameAsync(registro.UserName);
            if (userExists != null)
                throw new DomainException("Este nombre de usuario ya esta registrado");

            UserApplication user = new UserApplication()
            {
                Email = registro.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registro.UserName,
                CodeClient = registro.CodeClient
            };
            var result = await UserManager.CreateAsync(user, registro.Password);
            if (!result.Succeeded)
            {
                throw new DomainException("Error al crear el usuario, valide sus datos y vuelva a intentarlo");
            }

            var RolResult = await UserManager.AddToRoleAsync(user, registro.Rol);

            if (!RolResult.Succeeded)
            {
                await DeleteUser(registro.UserName);
                throw new DomainException("Error al asignar el rol al usuario, valide sus datos y vuelva a intentarlo");
            }
            return user;
        }

        public async Task<Login> Login(Login login)
        {
            var user = await UserManager.FindByNameAsync(login.UserName);
            if (user != null)
            {
                if (!await UserManager.CheckPasswordAsync(user, login.Password))
                {
                    throw new DomainException("La contraseña digitada es incorrecta");
                }
                var userRoles = await UserManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name, user.UserName,user.CodeClient ),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurate["JWT:SecretKey"]));
                var token = new JwtSecurityToken(
                issuer: _configurate["JWT:ValidIssuer"],
                audience: _configurate["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new Login()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expira = token.ValidTo,
                    UserName = login.UserName,
                    CodeClient = user.CodeClient
                };
            }
            throw new DomainException("El usuario " + login.UserName + " es incorrecto");
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

        public async Task<UserApplication> UpdateUser(UserApplication usuario)
        {
            var userExists = await UserManager.FindByNameAsync(usuario.Email);
            if (userExists is null)
                throw new DomainException("Este nombre de usuario no está registrado");

            var result = await UserManager.UpdateAsync(usuario);
            if (!result.Succeeded)
            {
                throw new DomainException("Error al actualizar el usuario, valide sus datos y vuelva a intentarlo");
            }

            return usuario;
        }

        public async Task<bool> DeleteUser(string user)
        {
            var userExists = await UserManager.FindByNameAsync(user);
            if (userExists is null)
                throw new DomainException("Este nombre de usuario no está registrado");

            var result = await UserManager.DeleteAsync(userExists);
            return result.Succeeded;
        }
    }
}
