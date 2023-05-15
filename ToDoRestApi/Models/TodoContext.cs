using Microsoft.EntityFrameworkCore;
using ToDoListDomain.Entities;

namespace ToDoRestApi.Models
{
    public class TodoContext: DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
        {
        }

        public DbSet<ToDoModel> TodoItems { get; set; } = null!;
    }
}
