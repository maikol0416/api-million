using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
	public class BaseEntitySQLServer 
	{
        public BaseEntitySQLServer()
        {
            DateCreation = DateTime.UtcNow.AddHours(-6);
            DateLastUpdate = null;
            Status = States.Active.ToString();
            Code = Guid.NewGuid().ToString();
        }

		[Key]
		public int Id { get; set; }
        public string Code { get;  set; }
        public DateTime DateCreation { get;  set; }
        public string Status { get;  set; }
        public DateTime? DateLastUpdate { get;  set; }
        public string CodeClient { get; set; }

        public void ChangeDateLastUpdate()
        {
            DateLastUpdate = DateTime.UtcNow.AddHours(-6);
        }
    }
}

