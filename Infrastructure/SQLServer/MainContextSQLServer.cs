using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common.Constants;
using Domain.Entities;
using Domain.Port;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Net.Mail;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.SQLServer
{
	public partial class MainContextSQLServer : IdentityDbContext<UserApplication>, IMainContextSQLServer
	{

        private readonly IConfiguration _configuration;

        public MainContextSQLServer()
        {
        }

        public MainContextSQLServer(DbContextOptions<MainContextSQLServer> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public MainContextSQLServer(DbContextOptions<MainContextSQLServer> options) : base(options)
		{
		}
		public MainContextSQLServer(string connectionString) : base(GetOptions(connectionString,null))
		{
		}
		public MainContextSQLServer(string connectionString, Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction)
			: base(GetOptions(connectionString, sqlServerOptionsAction))
        {

		}
		private static DbContextOptions GetOptions(string connectionString, Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction)
		{
			return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString, sqlServerOptionsAction).Options;
		}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relation:Collation", SQLServerConstants.RelationCollation);
            modelBuilder.HasDefaultSchema(SQLServerConstants.DefaultSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContextSQLServer).Assembly);


            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

             modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Owner__3214EC071546F37F");

            entity.ToTable("Owner", "pro");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Code).HasMaxLength(250);
            entity.Property(e => e.CodeClient).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(250);
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Property__3214EC078267F690");

            entity.ToTable("Property", "pro");

            entity.HasIndex(e => e.CodeInternal, "UQ__Property__F25D9D35911D01B3").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Code).HasMaxLength(250);
            entity.Property(e => e.CodeClient).HasMaxLength(250);
            entity.Property(e => e.CodeInternal).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(250);

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.Properties)
                .HasForeignKey(d => d.IdOwner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Property_Owner");
        });

        modelBuilder.Entity<PropertyImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Property__3214EC07F451200B");

            entity.ToTable("PropertyImage", "pro");

            entity.Property(e => e.Code).HasMaxLength(250);
            entity.Property(e => e.CodeClient).HasMaxLength(250);
            entity.Property(e => e.Enabled)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Status).HasMaxLength(250);

            entity.HasOne(d => d.IdPropertyNavigation).WithMany(p => p.PropertyImages)
                .HasForeignKey(d => d.IdProperty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertyImage_Property");
        });

        modelBuilder.Entity<PropertyTrace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Property__3214EC07128A2354");

            entity.ToTable("PropertyTrace", "pro");

            entity.Property(e => e.Code).HasMaxLength(250);
            entity.Property(e => e.CodeClient).HasMaxLength(250);
            entity.Property(e => e.DateSale).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(250);
            entity.Property(e => e.Tax).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Value).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPropertyNavigation).WithMany(p => p.PropertyTraces)
                .HasForeignKey(d => d.IdProperty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertyTrace_Property");
        });

        base.OnModelCreating(modelBuilder);
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration[$"{nameof(ConfigurateSQLServer)}:{nameof(ConfigurateSQLServer.ConnectionString)}"];
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}

