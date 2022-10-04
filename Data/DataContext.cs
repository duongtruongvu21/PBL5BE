using Microsoft.EntityFrameworkCore;
using PBL5BE.API.Data.Entities;

namespace PBL5BE.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
    }
}