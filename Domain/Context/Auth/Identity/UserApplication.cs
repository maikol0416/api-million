using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class UserApplication : IdentityUser
    {
        public string CodeClient { get; set; }
    }
}
