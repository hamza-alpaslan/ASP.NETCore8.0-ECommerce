using e_commerce.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Data
{
    public class ApplicationDbcContext: DbContext
    {
        public ApplicationDbcContext(DbContextOptions<ApplicationDbcContext>options): base(options)
        {
        }
        public DbSet<Product> products { get; set; }
    }
}
