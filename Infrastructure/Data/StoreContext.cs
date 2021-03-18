using Core.Entities;
using Microsoft.EntityFrameworkCore;  //DbContext

namespace Infrastructure.Data
{
    public class StoreContext : DbContext                                                                                       // DbContext is combination of unit of work and repository patterns  // we abstract our database away from our code .. we don't query our database directly
    {
        public StoreContext( DbContextOptions<StoreContext> options) : base(options)    
        {                                                                                                                                            // we need this constructor to provide options, in this case connection string -> passes the options up inot base(options) constructor
        }

        public DbSet<Product> Products{get; set;}                                                                       // allows us to query our entities from database
    }
}

