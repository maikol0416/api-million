using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SQLServer
{
	public partial class MainContextSQLServer
	{
		public virtual DbSet<Test> Tests { get; set; }
		public virtual DbSet<Owner> Owners { get; set; }

		public virtual DbSet<Property> Properties { get; set; }

		public virtual DbSet<PropertyImage> PropertyImages { get; set; }

		public virtual DbSet<PropertyTrace> PropertyTraces { get; set; }
    }
}

