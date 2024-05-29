using Microsoft.EntityFrameworkCore;
using TaskManagerWebApp.API.Models;

namespace TaskManagerWebApp.API.Data
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions options) : base (options) 
        { 

        }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<Users> Users { get; set; }
    }
}
