using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        //public DbSet<Area> Areas { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
