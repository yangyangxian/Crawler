namespace Yang.Entities
{
    public class BaseRepository
    {
        protected SeashellContext context = null;

        public BaseRepository(SeashellContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
