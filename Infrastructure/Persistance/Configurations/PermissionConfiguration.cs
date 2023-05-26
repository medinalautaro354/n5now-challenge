using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5NowApi.Domain.Entities;
using N5NowApi.Domain.ValueObjects.Permission;

namespace Infrastructure.Persistance.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(x => x.Id);
            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd();

            builder.Property(f => f.EmployeeForename)
                .HasConversion(f => f.Value,
                    value => EmployeeForename.Create(value))
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(f => f.EmployeeSurname)
                .HasConversion(f => f.Value,
                    value => EmployeeSurname.Create(value))
                .HasMaxLength(50)
                .IsRequired();
            
            // builder.OwnsOne(f => f.EmployeeForename)
            //     .Property(f => f.Value)
            //     .HasColumnName("EmployeeForename")
            //     .HasMaxLength(50)
            //     .IsRequired();
            //
            // builder.OwnsOne(f => f.EmployeeSurname)
            //     .Property(f => f.Value)
            //     .HasColumnName("EmployeeSurname")
            //     .HasMaxLength(50)
            //     .IsRequired();

            builder.Ignore(f => f.Events);
        }
    }
}
