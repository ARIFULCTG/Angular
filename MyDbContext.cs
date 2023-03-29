using Microsoft.EntityFrameworkCore;

namespace NG6_R51
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
        { }

        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }
        public DbSet<trainer> trainers { get; set; }
        public DbSet<player> players { get; set; }
    }
}


