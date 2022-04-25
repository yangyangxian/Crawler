using Microsoft.EntityFrameworkCore;

namespace Yang.Entities
{
    public class CommunityRepository : BaseRepository
    {       
        public CommunityRepository(SeashellContext context) : base(context)
        {
            this.context = context;
        }
        
        public void AddOrUpdate(Community communityEntity)
        {
            Community existingEntity = context.Communities.Where(c => c.CommunityName == communityEntity.CommunityName && c.AdministrativeDistrictId == communityEntity.AdministrativeDistrictId).FirstOrDefault();
            
            if (existingEntity != null)
            {
                communityEntity.CommunityId = existingEntity.CommunityId;

                existingEntity.BuildingNumber = communityEntity.BuildingNumber;
                existingEntity.Unit = communityEntity.Unit;
                existingEntity.SeashellURL = communityEntity.SeashellURL;                
                existingEntity.Neighborhood = communityEntity.Neighborhood;
                existingEntity.External_id = communityEntity.External_id;

                foreach (CommunityHistoryInfo info in communityEntity.CommunityHistoryInfo)
                {
                    CommunityHistoryInfoRepository chiRepo = new CommunityHistoryInfoRepository(context);
                    chiRepo.AddOrUpdate(info);

                    //if (context.CommunityHistoryInfos.Where(ch => ch.CommunityId == communityEntity.CommunityId && ch.DataTime == info.DataTime).FirstOrDefault() == null)
                    //{
                    //    existingEntity.CommunityHistoryInfo.Add(info);
                    //}
                }              

                context.Communities.Update(existingEntity);
            }
            else
            {
                context.Communities.Add(communityEntity);
            }
        }

        public void AddOrUpdate(List<Community> communityEntities)
        {
            foreach (Community communityEntity in communityEntities)
            {
                AddOrUpdate(communityEntity);
            }
        }
    }
}
