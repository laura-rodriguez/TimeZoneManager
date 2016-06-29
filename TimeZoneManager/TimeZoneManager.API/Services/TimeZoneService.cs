using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeZoneManager.API.Contracts;
using TimeZoneManager.DAL.Repository;
using TimeZoneManager.DAL.Model;

namespace TimeZoneManager.API.Services
{
    public class TimeZoneService
    {
        private IRepository<DAL.Model.TimeZone> repository;

        public TimeZoneService(IRepository<DAL.Model.TimeZone> repository) {
            this.repository = repository;
        }

        public IEnumerable<TimeZoneDTO> GetAll()
        {
            return repository.GetAll().Select(t => ConvertToDTO(t));
        }

        public TimeZoneDTO GetByID(int id)
        {
            var item = repository.GetByID(id);

            if (item != null) {
                return ConvertToDTO(item);
            }
            return null;
        }

        public IEnumerable<TimeZoneDTO> GetByOwner(string owner)
        {
            return repository.GetByOwner(owner).Select(t => ConvertToDTO(t));
        }

        public TimeZoneDTO Add(TimeZoneDTO timeZone)
        {
            return ConvertToDTO(repository.Add(ConvertFromDTO(timeZone)));
        }

        public void Update(TimeZoneDTO timeZone)
        {
            repository.Update(ConvertFromDTO(timeZone));
        }

        public void Remove(int id)
        {
            repository.Remove(id);
        }

        private TimeZoneDTO ConvertToDTO(DAL.Model.TimeZone t)
        {
            var timeZoneDTO = new TimeZoneDTO();

            timeZoneDTO.ID = t.ID;
            timeZoneDTO.Name = t.Name;
            timeZoneDTO.Owner = t.Owner;
            timeZoneDTO.City = t.City;
            timeZoneDTO.GMTOffset = t.GMTOffset;

            return timeZoneDTO;
        }

        private DAL.Model.TimeZone ConvertFromDTO(TimeZoneDTO dto)
        {
            var timeZone = new DAL.Model.TimeZone();

            timeZone.ID = dto.ID;
            timeZone.Name = dto.Name;
            timeZone.Owner = dto.Owner;
            timeZone.City = dto.City;
            timeZone.GMTOffset = dto.GMTOffset;

            return timeZone;
        }
    }
}