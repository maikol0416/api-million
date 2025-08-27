using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User 
    {
        public User()
        {

        }

        public string UserName { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public string Rol { get; set; }
        public string CodeClient { get; set; }
    }
}
 