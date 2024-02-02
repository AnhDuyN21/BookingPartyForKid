using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Infrastructures
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        //DBSet
        // Tương ứng với các bảng trong DB
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Party> Party { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountDetailsConfiguration).Assembly);

        //}


    }
}
