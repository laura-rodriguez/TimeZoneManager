using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneManager.DAL.Model;

namespace TimeZoneManager.DAL.Repository
{
    public class TimeZoneRepository : IRepository<Model.TimeZone>
    {
        public Model.TimeZone Add(Model.TimeZone entity)
        {
            using (var context = new TimeZoneManagerEntities())
            {
                context.TimeZones.Add(entity);
                context.SaveChanges();

                return entity;
            }
        }

        public IEnumerable<Model.TimeZone> GetAll()
        {
            using (var context = new TimeZoneManagerEntities())
            {
                return context.TimeZones.ToList();
            }
        }

        public Model.TimeZone GetByID(int id)
        {
            using (var context = new TimeZoneManagerEntities())
            {
                return context.TimeZones.SingleOrDefault(e => e.ID == id);
            }
        }

        public void Remove(int id)
        {
            using (var context = new TimeZoneManagerEntities())
            {
                var entity = context.TimeZones.Single(e => e.ID == id);

                if (entity != null)
                {
                    context.TimeZones.Remove(entity);
                    context.SaveChanges();
                }
            }
        }

        public void Update(Model.TimeZone entity)
        {
            using (var context = new TimeZoneManagerEntities())
            {
                var entityToUpdate = context.TimeZones.SingleOrDefault(e => e.ID == entity.ID);
                if (entityToUpdate != null)
                {
                    entityToUpdate.Name = entity.Name;
                    entityToUpdate.City = entity.City;
                    entityToUpdate.GMTOffset = entity.GMTOffset;

                    context.SaveChanges();
                }
                
            }
        }

        public IEnumerable<Model.TimeZone> GetByOwner(string owner) {
            using (var context = new TimeZoneManagerEntities())
            {
                return context.TimeZones.Where(e => e.Owner == owner).ToList();
            }
        }
    }
}
