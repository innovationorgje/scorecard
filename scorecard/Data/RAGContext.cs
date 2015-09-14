using scorecard.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace scorecard.Data
{
    public class RAGContext : ApplicationDbContext
    {
        public RAGContext()
            ////: base("DefaultConnection")
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
        public DbSet<StatusUpdate> StatusUpdates { get; set; }
////        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}