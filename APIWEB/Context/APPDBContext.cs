using APIWEB.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIWEB.Context
{
    public class APPDBContext : IdentityDbContext
    {
        public APPDBContext(DbContextOptions<APPDBContext> options): base(options)
        {

        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
