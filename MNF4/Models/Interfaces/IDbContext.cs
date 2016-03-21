using System.Linq;

namespace MNF4.Models.Interfaces
{
    public interface IDbContext
    {
        IQueryable<Contact> Contacts { get; }
        IQueryable<Client> Clients { get; }
        IQueryable<Source> Sources { get; }
        IQueryable<Appointment> Appointments { get; }
        IQueryable<Chart> Charts { get; }

        IQueryable<Media> Media { get; }
        IQueryable<EventType> MediaTypes { get; }
        IQueryable<MNFEvent> Events { get; }
        IQueryable<EventType> EventTypes { get; }
        IQueryable<PromoCode> PromoCodes { get; }


        int SaveChanges();
        T Attach<T>(T entity) where T : class;
        T Add<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;
    }
}