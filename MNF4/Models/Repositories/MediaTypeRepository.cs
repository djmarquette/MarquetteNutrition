using System;
using System.Linq;

namespace MNF4.Models
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        public IQueryable<MediaType> All { get; private set; }
        public void InsertOrUpdate(MediaType table)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IQueryable<MediaType> ListPromoCodes()
        {
            throw new NotImplementedException();
        }

        public IQueryable<MediaType> FindByName(string q)
        {
            throw new NotImplementedException();
        }

        public MediaType Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}