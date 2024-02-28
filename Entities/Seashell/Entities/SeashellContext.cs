using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Yang.Utilities;

namespace Yang.Entities
{
    public class SeashellContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=YangData;User Id=yy;password=yy;TrustServerCertificate=true");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Alternate Key is the logic primary key
            modelBuilder.Entity<Community>()
                .ToTable("Community")
                .HasAlternateKey(c => new { c.External_id });

            modelBuilder.Entity<CommunityHistoryInfo>()
                .HasAlternateKey(p => new { p.CommunityId, p.DataTime });

            modelBuilder.Entity<Home>()
                .HasAlternateKey(h => new { h.External_id });

            modelBuilder.Entity<HomeListingPrice>()
                .HasAlternateKey(h => new { h.ListingPriceDate, h.HomeId });

            modelBuilder.Entity<AdministrativeDistrict>().HasData(
                new AdministrativeDistrict() { AdministrativeDistrictId = 1, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "碑林区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/beilin/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 2, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "新城区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/xinchengqu/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 3, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "莲湖区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/lianhu/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 4, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "雁塔区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/yanta/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 5, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "未央区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/weiyang/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 6, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "灞桥区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/baqiao/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 7, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "长安区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/changan7/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 8, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "高陵区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/gaoling/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 9, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "鄠邑区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/huyiqu/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 10, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "临潼区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/lintong/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 11, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "蓝田县", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/lantian/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 12, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "周至县", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/zhouzhi/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 13, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "西咸新区（西安）", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/xixianxinquxian/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 14, City = SeashellConst.City.西安.ToString(), AdministrativeDistrictName = "阎良区", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/yanliang/pg{0}cro22/" },

                new AdministrativeDistrict() { AdministrativeDistrictId = 20, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "鼓楼", CommunityMainPageURL = "https://nj.ke.com/xiaoqu/gulou/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 21, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "建邺", CommunityMainPageURL = "https://nj.ke.com/xiaoqu/jianye/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 22, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "秦淮", CommunityMainPageURL = "https://nj.ke.com/xiaoqu/qinhuai/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 23, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "玄武", CommunityMainPageURL = "https://nj.ke.com/xiaoqu/xuanwu/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 24, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "雨花台", CommunityMainPageURL = "https://nj.ke.com/xiaoqu/yuhuatai/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 25, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "栖霞", CommunityMainPageURL = "https://nj.ke.com/xiaoqu/qixia/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 26, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "江宁", CommunityMainPageURL = "https://nj.ke.com/xiaoqu/jiangning/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 27, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "浦口", CommunityMainPageURL = "https://nj.ke.com/xiaoqu/pukou/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 28, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "六合", CommunityMainPageURL = "https://nj.ke.com/xiaoqu/liuhe/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 29, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "溧水", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/lishui/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 30, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "高淳", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/gaochun/pg{0}cro22/" },
                new AdministrativeDistrict() { AdministrativeDistrictId = 31, City = SeashellConst.City.南京.ToString(), AdministrativeDistrictName = "句容", CommunityMainPageURL = "https://xa.ke.com/xiaoqu/jurong/pg{0}cro22/" });         
        }

        public DbSet<Community> Communities { get; set; }

        public DbSet<CommunityHistoryInfo> CommunityHistoryInfos { get; set; }

        public DbSet<AdministrativeDistrict> AdministrativeDistrict { get; set; }

        public DbSet<Home> Home { get; set; }

        public DbSet<HomeListingPrice> HomeListingPrice { get; set; }
    }
}
