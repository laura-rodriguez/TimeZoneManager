using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneManager.DAL.Model;

namespace TimeZoneManager.DAL.Repository
{
    public class MockTimeZoneRepository : IRepository<Model.TimeZone>
    {
        protected ICollection<Model.TimeZone> repository = new List<Model.TimeZone>();

        public Model.TimeZone Add(Model.TimeZone entity)
        {
            repository.Add(entity);
            return entity;
        }

        public IEnumerable<Model.TimeZone> GetAll()
        {
            return repository.ToList();
        }

        public Model.TimeZone GetByID(int id)
        {
            return repository.SingleOrDefault( i => i.ID == id);
        }

        public IEnumerable<Model.TimeZone> GetByOwner(string owner)
        {
            return repository.Where(e => e.Owner == owner).ToList();
        }

        public void Remove(int id)
        {
            var entity = repository.Single(e => e.ID == id);
            repository.Remove(entity);
        }

        public void Update(Model.TimeZone entity)
        {
            var currentEntity = repository.Single(e => e.ID == entity.ID);

            if (currentEntity != null)
            {
                var entityToUpdate = currentEntity;
                entityToUpdate.Name = entity.Name;
                entityToUpdate.City = entity.City;
                entityToUpdate.GMTOffset = entity.GMTOffset;

                repository.Remove(currentEntity);
                repository.Add(entityToUpdate);
            }
        }
    }
}
