using Microsoft.EntityFrameworkCore;
using NiceGallery.Api.Models;


namespace NiceGallery.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }



        public DbSet<Product> Produtos { get; set; }

       
    }
}
