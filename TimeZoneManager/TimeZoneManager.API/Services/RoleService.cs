using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TimeZoneManager.API.Models;

namespace TimeZoneManager.API.Services
{
    public class RoleService
    {
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public ApplicationDbContext DbContext { get; private set; }

        public RoleService()
        {
            DbContext = new ApplicationDbContext();
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(DbContext));
        }

        public IEnumerable<string> GetAll()
        {
            var allRoles = RoleManager.Roles.Select(r => r.Name).ToList();
            return allRoles;
        }

    }
}