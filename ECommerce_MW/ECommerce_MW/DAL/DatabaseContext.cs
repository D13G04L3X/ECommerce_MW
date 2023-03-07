using ECommerce_MW.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MW.DAL
{
    public class DatabaseContext : DbContext                                        //Todo lo que haga se verá reflejado en la Bd
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<Country> Countries { get; set; }                               //Variable del database en plural
        
    }
}
