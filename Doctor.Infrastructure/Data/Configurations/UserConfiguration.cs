using Doctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.Data.Configurations;


    internal class UserConfiguration  : IEntityTypeConfiguration<User>
    {
    

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(t => t.IdUser);
        builder.Property(t => t.IdUser).UseIdentityColumn();

        builder.Property(t => t.UserName).IsRequired();
        builder.Property(t => t.CodeM);
        builder.Property(t => t.Role);
        builder.Property(t => t.PasswordHash);
      
    }
}


