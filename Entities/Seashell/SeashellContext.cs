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
            //Alternate Key is the logic primary key
            modelBuilder.Entity<Community>()
                .ToTable("Community")
                .HasAlternateKey(c => new { c.CommunityName, c.AdministrativeDistrictId });

            modelBuilder.Entity<CommunityHistoryInfo>()
                .HasAlternateKey(p => new { p.CommunityId, p.DataTime });

            modelBuilder.Entity<Home>()
                .HasAlternateKey(h => new { h.External_id });

            modelBuilder.Entity<HomeListingPrice>()
                .HasAlternateKey(h => new { h.ListingPriceDate, h.HomeId });

            modelBuilder.Entity<AdministrativeDistrict>().HasData(
                new AdministrativeDistrict() { AdministrativeDistrictId = 1, AdministrativeDistrictName = "碑林区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/beilin/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 2, AdministrativeDistrictName = "新城区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/xinchengqu/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 3, AdministrativeDistrictName = "莲湖区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/lianhu/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 4, AdministrativeDistrictName = "雁塔区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/yanta/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 5, AdministrativeDistrictName = "未央区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/weiyang/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 6, AdministrativeDistrictName = "灞桥区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/baqiao/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 7, AdministrativeDistrictName = "长安区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/changan7/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 8, AdministrativeDistrictName = "高陵区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/gaoling/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 9, AdministrativeDistrictName = "鄠邑区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/huyiqu/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 10, AdministrativeDistrictName = "临潼区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/lintong/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 11, AdministrativeDistrictName = "蓝田县", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/lantian/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 12, AdministrativeDistrictName = "周至县", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/zhouzhi/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 13, AdministrativeDistrictName = "西咸新区（西安）", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/xixianxinquxian/pg{0}/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 14, AdministrativeDistrictName = "阎良区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/yanliang/pg{0}/" });         
        }

        public DbSet<Community> Communities { get; set; }

        public DbSet<CommunityHistoryInfo> CommunityHistoryInfos { get; set; }

        public DbSet<AdministrativeDistrict> AdministrativeDistrict { get; set; }

        public DbSet<Home> Home { get; set; }

        public DbSet<HomeListingPrice> HomeListingPrice { get; set; }
    }
}
