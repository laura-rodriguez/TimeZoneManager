using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeZoneManager.API.Filters;
using TimeZoneManager.API.Services;

namespace TimeZoneManager.API.Controllers
{
    [WebAPIAuthorizeAttribute(Roles = "UserManager, Admin")]
    public class RolesController : ApiController
    {
        // GET: api/Roles
        public IHttpActionResult Get()
        {
            try
            {
                var roleService = new RoleService();
                return Ok(roleService.GetAll());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }
    }
}
