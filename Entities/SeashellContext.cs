using Microsoft.EntityFrameworkCore;

namespace Yang.Entities
{
    public class SeashellContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=YangData;User Id=sa;password=sa");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Community>()
                .ToTable("Community");

            modelBuilder.Entity<CommunityHistoryInfo>()
                .HasNoKey();
        }

        public DbSet<Community> Communities { get; set; }

        public DbSet<CommunityHistoryInfo> CommunityHistoryInfos { get; set; }
    }
}
