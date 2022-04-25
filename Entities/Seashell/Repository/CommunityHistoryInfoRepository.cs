namespace Yang.Entities
{
    public class CommunityHistoryInfoRepository : BaseRepository
    {
        public CommunityHistoryInfoRepository(SeashellContext context) : base(context)
        {
            this.context = context;
        }
        
        public void AddOrUpdate(CommunityHistoryInfo communityInfo)
        {
            CommunityHistoryInfo existingEntity = context.CommunityHistoryInfos.Where(x => x.CommunityId == communityInfo.CommunityId && x.DataTime == communityInfo.DataTime).FirstOrDefault();

            if (communityInfo.CommunityHistoryInfoId == 0 && existingEntity == null)
            {
                context.CommunityHistoryInfos.Add(communityInfo);
            }
            else if (communityInfo.CommunityHistoryInfoId != 0 && existingEntity != null)
            {
                context.CommunityHistoryInfos.Update(communityInfo);
            }
            else
                throw new Exception("Please check the input parameter communityInfo. It causes the unexpected situation: communityInfo.CommunityHistoryInfoId is 0 but existingEntity is not null;communityInfo.CommunityHistoryInfoId is not 0 but existingEntity is null ");
        }
    }
}
