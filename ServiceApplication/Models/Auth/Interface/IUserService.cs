using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceApplication
{
    public interface IUserService
    {
        Task<Login> Login(Login login);
        Task<UserApplication> RegistrarUsuario(User user);
        Task<bool> DeleteUser(string user);
        Task<UserApplication> UpdateUser(UserApplication usuario);

    }
}
