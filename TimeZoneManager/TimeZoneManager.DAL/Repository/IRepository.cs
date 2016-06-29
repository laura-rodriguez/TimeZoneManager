using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeZoneManager.DAL.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetByID(int id);

        T Add(T entity);

        void Update(T entity);

        void Remove(int id);

        IEnumerable<T> GetByOwner(string owner);
    }
}
