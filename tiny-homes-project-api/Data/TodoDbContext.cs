using Microsoft.EntityFrameworkCore;
using TinyHomesProjectAPI.Models;

namespace tiny_homes_project_api.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) :base(options)
        {
            
        }

        public DbSet<TodoItem> TodoItems {  get; set; }
    }
}
