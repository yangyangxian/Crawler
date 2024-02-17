﻿using Microsoft.EntityFrameworkCore;

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

            Community existingEntity = context.Communities.Where(c => c.External_id == communityEntity.External_id).Include(c => c.CommunityHistoryInfo).FirstOrDefault();

            if (existingEntity == null)
                throw new Exception("Can not update. Not found an existing community in the database.");

            communityEntity.CommunityId = existingEntity.CommunityId;

            existingEntity.BuildingNumber = communityEntity.BuildingNumber;
            existingEntity.Unit = communityEntity.Unit;
            existingEntity.SeashellURL = communityEntity.SeashellURL;
            existingEntity.Neighborhood = communityEntity.Neighborhood;
            existingEntity.UpdateDate = DateTime.Now;

            foreach (CommunityHistoryInfo info in communityEntity.CommunityHistoryInfo)
            {
                info.CommunityId = existingEntity.CommunityId;
                existingEntity.AddCommunityHistoryInfo(info);
                //CommunityHistoryInfoRepository chiRepo = new CommunityHistoryInfoRepository(context);
                //chiRepo.Add(info);
            }

            context.Communities.Update(existingEntity);
        }
        
        //2024.2.4: Update the existing community by external id since the name of the community may change
        public void AddOrUpdate(Community communityEntity)
        {
            Community existingEntity = context.Communities.Where(c => c.External_id == communityEntity.External_id).Include(c => c.CommunityHistoryInfo).FirstOrDefault();
            
            if (existingEntity != null)
            {
                communityEntity.CommunityId = existingEntity.CommunityId;

                existingEntity.CommunityName = !string.IsNullOrEmpty(communityEntity.CommunityName) ? communityEntity.CommunityName : existingEntity.CommunityName;
                existingEntity.BuildingNumber = communityEntity.BuildingNumber != 0 ? communityEntity.BuildingNumber : existingEntity.BuildingNumber;
                existingEntity.Unit = communityEntity.Unit != 0 ? communityEntity.Unit : existingEntity.Unit;
                existingEntity.SeashellURL = !string.IsNullOrEmpty(communityEntity.SeashellURL) ? communityEntity.SeashellURL : existingEntity.SeashellURL;                
                existingEntity.Neighborhood = !string.IsNullOrEmpty(communityEntity.Neighborhood) ? communityEntity.Neighborhood : existingEntity.Neighborhood;
                existingEntity.UpdateDate = DateTime.Now;

                foreach (CommunityHistoryInfo info in communityEntity.CommunityHistoryInfo)
                {
                    info.CommunityId = existingEntity.CommunityId;
                    existingEntity.AddCommunityHistoryInfo(info);
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
