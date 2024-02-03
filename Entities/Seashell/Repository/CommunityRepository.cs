using Microsoft.EntityFrameworkCore;

namespace Yang.Entities
{
    public class CommunityRepository : BaseRepository
    {       
        public CommunityRepository(SeashellContext context) : base(context)
        {
            this.context = context;
        }

        public Community FindByName(string communityName, int administrativeDistrictId)
        {
            Community community = this.context.Communities.FirstOrDefault(c => c.CommunityName == communityName && c.AdministrativeDistrictId == administrativeDistrictId);
            return community;
        }

        public void Update(Community communityEntity)
        {
            ArgumentNullException.ThrowIfNull(communityEntity);

            Community existingEntity = context.Communities.Where(c => c.CommunityName == communityEntity.CommunityName && c.AdministrativeDistrictId == communityEntity.AdministrativeDistrictId).FirstOrDefault();

            if (existingEntity == null)
                throw new Exception("Can not update. Not found an existing community in the database.");

            communityEntity.CommunityId = existingEntity.CommunityId;

            existingEntity.BuildingNumber = communityEntity.BuildingNumber;
            existingEntity.Unit = communityEntity.Unit;
            existingEntity.SeashellURL = communityEntity.SeashellURL;
            existingEntity.Neighborhood = communityEntity.Neighborhood;
            existingEntity.External_id = communityEntity.External_id;
            existingEntity.UpdateDate = DateTime.Now;

            foreach (CommunityHistoryInfo info in communityEntity.CommunityHistoryInfo)
            {
                CommunityHistoryInfoRepository chiRepo = new CommunityHistoryInfoRepository(context);
                chiRepo.AddOrUpdate(info);
            }

            context.Communities.Update(existingEntity);
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
                existingEntity.UpdateDate = DateTime.Now;

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

        public void Update(List<Community> communityEntities)
        {
            foreach (Community communityEntity in communityEntities)
            {
                Update(communityEntity);
            }
        }
    }
}
