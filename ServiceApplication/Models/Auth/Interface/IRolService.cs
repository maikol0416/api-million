using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceApplication
{
    public interface IRolService
    {
        Task<IdentityRole> CreateRol(string nameRol);
        Task<List<IdentityRole>> ToListRol();
        Task<bool> DeleteRol(string nameRol);

    }
}
