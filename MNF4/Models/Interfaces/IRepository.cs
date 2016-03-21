using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MNF4.Models
{
    public interface IRepository<T>
    {
        IQueryable<T> All { get; }
        void InsertOrUpdate(T table);
        void Delete(int id);
        void Save();
    }
}
