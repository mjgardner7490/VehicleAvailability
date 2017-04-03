using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VehicleAvailability.Entities
{
    public partial class SoapDatabaseContext : DbContext
    {
        public virtual DbSet<VehicleTbl> VehicleTbl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:project2server.database.windows.net,1433;Initial Catalog=SoapDatabase;Persist Security Info=False;User ID=vlogin;Password=0Vpassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleTbl>(entity =>
            {
                entity.HasKey(e => e.VehicleVin)
                    .HasName("PK__VEHICLE___19908E3112350A77");

                entity.ToTable("VEHICLE_TBL");

                entity.Property(e => e.VehicleVin)
                    .HasColumnName("VEHICLE_VIN")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.VehicleCity)
                    .IsRequired()
                    .HasColumnName("VEHICLE_CITY")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.VehicleLastupdate)
                    .HasColumnName("VEHICLE_LASTUPDATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.VehicleName)
                    .IsRequired()
                    .HasColumnName("VEHICLE_NAME")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.VehicleStatus)
                    .IsRequired()
                    .HasColumnName("VEHICLE_STATUS")
                    .HasColumnType("varchar(20)");
            });
        }
    }
}