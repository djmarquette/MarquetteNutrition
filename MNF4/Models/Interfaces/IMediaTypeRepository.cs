using System.Linq;

namespace MNF4.Models
{
    interface IMediaTypeRepository : IRepository<MediaType>
    {
        IQueryable<MediaType> ListPromoCodes();
        IQueryable<MediaType> FindByName(string q);
        MediaType Find(int id);
    }
}