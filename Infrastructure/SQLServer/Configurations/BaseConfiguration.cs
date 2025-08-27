using System;
using Domain.Common;
using Domain.Common.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SQLServer.Configurations
{
	public class BaseConfiguration<T> where T: BaseEntitySQLServer
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(typeof(T).Name, SQLServerConstants.DefaultSchema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DateCreation)
                .HasColumnType("datetime");
            builder.Property(x => x.DateLastUpdate)
                .HasColumnType("datetime")
                .IsRequired(false);
            builder.Property(x => x.Status)
                .HasColumnType("nvarchar(20)")
                .HasDefaultValue("Active")
                .IsRequired(true);


        }
    }
}

