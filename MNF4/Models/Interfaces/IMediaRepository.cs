using System.Linq;

namespace MNF4.Models
{
    interface IMediaRepository<T> : IRepository<Media>
    {
        IQueryable<Media> ListPromoCodes();
        IQueryable<Media> FindByName(string q);
        Media Find(int id);
    }
}