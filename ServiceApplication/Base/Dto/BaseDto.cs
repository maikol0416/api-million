using System;
using Domain.Common;

namespace ServiceApplication.Dto
{
    public class BaseDto
    {

        public int Id { get; set; }
        public string Code { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateCreation { get; set; } = DateTime.UtcNow.AddHours(-5);
        public string Status { get; set; } = States.Active.ToString();
        public string CodeClient { get; set; }
        public DateTime? DateLastUpdate { get; set; }
    }
}

