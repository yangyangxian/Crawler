using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                .ToTable("Community")
                .HasAlternateKey(c => new { c.CommunityName, c.AdministrativeDistrictId });

            modelBuilder.Entity<AdministrativeDistrict>().HasData(
                new AdministrativeDistrict() { AdministrativeDistrictId = 1, AdministrativeDistrictName = "碑林区" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 2, AdministrativeDistrictName = "新城区" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 3, AdministrativeDistrictName = "莲湖区" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 4, AdministrativeDistrictName = "雁塔区" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 5, AdministrativeDistrictName = "未央区" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 6, AdministrativeDistrictName = "灞桥区" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 7, AdministrativeDistrictName = "长安区" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 8, AdministrativeDistrictName = "高陵区" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 9, AdministrativeDistrictName = "鄠邑区" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 10, AdministrativeDistrictName = "临潼区" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 11, AdministrativeDistrictName = "蓝田县" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 12, AdministrativeDistrictName = "周至县" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 13, AdministrativeDistrictName = "西咸新区（西安）" });
        }

        public DbSet<Community> Communities { get; set; }

        public DbSet<CommunityHistoryInfo> CommunityHistoryInfos { get; set; }

        public DbSet<AdministrativeDistrict> AdministrativeDistrict { get; set; }
    }
}
