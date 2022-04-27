using Microsoft.EntityFrameworkCore;
using psw_ftn.Models;

namespace psw_ftn.Data
{
   public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}