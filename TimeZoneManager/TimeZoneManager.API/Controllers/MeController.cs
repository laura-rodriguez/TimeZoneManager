using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using TimeZoneManager.API.Contracts;
using TimeZoneManager.API.Filters;

namespace TimeZoneManager.API.Controllers
{
    [WebAPIAuthorizeAttribute]
    public class MeController : ApiController
    {
        // GET: api/Me
        public IHttpActionResult Get()
        {
            var userInfoDTO = new UserInfoDTO();

            userInfoDTO.Email = User.Identity.Name;
            userInfoDTO.Roles = 
                ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();

            return Ok(userInfoDTO);
        }
    }
}
