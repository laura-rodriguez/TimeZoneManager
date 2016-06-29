using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeZoneManager.API.Contracts;
using TimeZoneManager.API.Filters;
using TimeZoneManager.API.Services;
using TimeZoneManager.DAL.Repository;
namespace TimeZoneManager.API.Controllers
{
    [WebAPIAuthorizeAttribute(Roles = "Admin, User")]
    public class TimeZonesController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable<TimeZoneDTO> items = new List<TimeZoneDTO>();
                var timeZoneService = new TimeZoneService(new TimeZoneRepository());

                if (User.IsInRole("Admin"))
                {
                    items = timeZoneService.GetAll();
                }
                else if (User.IsInRole("User"))
                {
                    items = timeZoneService.GetByOwner(User.Identity.Name);
                }
                else
                {
                    return InternalServerError();
                }

                return Ok(items);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var timeZoneService = new TimeZoneService(new TimeZoneRepository());
                TimeZoneDTO item = timeZoneService.GetByID(id);

                if (!User.IsInRole("Admin") && (item.Owner != User.Identity.Name))
                {
                    return Unauthorized();
                }

                return Ok(item);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]TimeZoneDTO item)
        {
            try
            {
                var timeZoneService = new TimeZoneService(new TimeZoneRepository());
                item.Owner = User.Identity.Name;
                return Ok(timeZoneService.Add(item));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]TimeZoneDTO item)
        {
            try
            {
                var timeZoneService = new TimeZoneService(new TimeZoneRepository());
                TimeZoneDTO timeZone = timeZoneService.GetByID(id);

                if (!User.IsInRole("Admin") && (item.Owner != User.Identity.Name))
                {
                    return Unauthorized();
                }

                item.ID = id;
                timeZoneService.Update(item);

                return Ok(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var timeZoneService = new TimeZoneService(new TimeZoneRepository());
                TimeZoneDTO timeZone = timeZoneService.GetByID(id);

                if (!User.IsInRole("Admin") && (timeZone.Owner != User.Identity.Name))
                {
                    return Unauthorized();
                }

                timeZoneService.Remove(id);

                return Ok(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}