using System;
using Domain.Common;

namespace Domain.Entities
{
	public class Test :BaseEntitySQLServer
	{
		public Test()
		{
		}
		public string Name { get; set; }
	}
}

