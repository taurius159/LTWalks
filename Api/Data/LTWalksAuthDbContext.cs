using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class LTWalksAuthDbContext : IdentityDbContext
{
    public LTWalksAuthDbContext(DbContextOptions<LTWalksAuthDbContext> dbContextOptions) :base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var readerRoleId = "7dce9596-474b-45d5-9c7d-82dcee035f40";
        var writeRoleId = "508d2df0-4b8a-4a44-a745-890148ebc3bb";

        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper()
            },
            new IdentityRole
            {
                Id = writeRoleId,
                ConcurrencyStamp = writeRoleId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper()
            }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}