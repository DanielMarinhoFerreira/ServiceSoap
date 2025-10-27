using Microsoft.EntityFrameworkCore;
using ServiceSoap.Models;

namespace ServiceSoap.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Usuario> Usuarios { get; set;}
    }
}
