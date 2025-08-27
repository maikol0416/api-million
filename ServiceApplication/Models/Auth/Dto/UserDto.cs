using System.Collections.Generic;

namespace ServiceApplication.Dto
{
    public class UserDto: BaseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
