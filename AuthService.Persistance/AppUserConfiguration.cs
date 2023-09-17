using AuthService.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Persistance;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        var hasher = new PasswordHasher<AppUser>();
        
        builder.HasData(new AppUser
        {
            Id = "1",
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            PasswordHash = hasher.HashPassword(null,"12345"),
            SecurityStamp = string.Empty,
        });
    }
}