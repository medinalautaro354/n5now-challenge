using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5NowApi.Domain.Dtos;
using N5NowApi.Domain.Entities;
using N5NowApi.Domain.ValueObjects.PermissionType;

namespace Infrastructure.Persistance.Configurations
{
    public class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.ToTable("PermissionTypes");

            builder.HasKey(x => x.Id);
            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd();

            builder.Property(f => f.Description)
                .HasConversion(
                    f => f.Value,
                    value =>
                        Description.Create(value));

            builder.HasMany(f => f.Permission)
                .WithOne(f => f.PermissionType)
                .HasPrincipalKey(f => f.Id)
                .HasForeignKey(f => f.PermissionTypeId);

            builder.HasData(
                new PermissionType(1, "User"),
                new PermissionType(2, "Admin")
            );
        }
    }
}