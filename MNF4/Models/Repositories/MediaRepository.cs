using System.Linq;

namespace MNF4.Models
{
    public class MediaRepository<T> : IMediaRepository<T>
    {
        public IQueryable<Media> All { get; private set; }
        public void InsertOrUpdate(Media table)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Media> ListPromoCodes()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Media> FindByName(string q)
        {
            throw new System.NotImplementedException();
        }

        public Media Find(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}