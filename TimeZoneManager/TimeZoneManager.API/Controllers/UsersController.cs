using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeZoneManager.API.Contracts;
using TimeZoneManager.API.Filters;
using TimeZoneManager.API.Services;

namespace TimeZoneManager.API.Controllers
{
    [WebAPIAuthorizeAttribute(Roles = "UserManager, Admin")]
    public class UsersController : ApiController
    {
        // GET: api/Users
        public IHttpActionResult Get()
        {
            try
            {
                var usersService = new UsersService();
                return Ok(usersService.GetAll());
            }
            catch (Exception e) {
                return InternalServerError(e);
            }
        }

        // GET: api/Users/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                var usersService = new UsersService();
                var user = usersService.GetByID(id);

                if (user != null)
                {
                    return Ok(user);
                }
               
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // POST: api/Users
        public IHttpActionResult Post([FromBody]UserDTO value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest();
                }
                else
                {
                    var usersService = new UsersService();

                    return Ok(usersService.Add(value));
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // PUT: api/Users/5
        public IHttpActionResult Put(string id, [FromBody]UserDTO value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest();
                }
                else
                {
                    var usersService = new UsersService();
                    value.ID = id;
                    usersService.Update(value);

                    return Ok(HttpStatusCode.NoContent);
                } 
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE: api/Users/5
        public IHttpActionResult Delete(string id)
        {
            try
            {
                var usersService = new UsersService();

                var user = usersService.GetByID(id);

                if (user == null)
                {
                    return NotFound();
                }

                if (user.Email == User.Identity.Name)
                {
                    return BadRequest("You can't delete your own user.");
                }

                usersService.Remove(id);

                return Ok(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        
    }
}
