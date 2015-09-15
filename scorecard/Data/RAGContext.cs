using scorecard.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace scorecard.Data
{
    public class RAGContext : IdentityDbContext
    {
        public RAGContext()
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
        public DbSet<StatusUpdate> StatusUpdates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}