using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TimeZoneManager.API.Contracts;
using TimeZoneManager.API.Helpers;
using TimeZoneManager.API.Models;

namespace TimeZoneManager.API.Services
{
    public class UsersService
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public ApplicationDbContext DbContext { get; private set; }

        public UsersService()
        {
            DbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DbContext));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(DbContext));
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var allUsers = UserManager.Users.ToList().Select(u => ConvertToDTO(u));
            return allUsers;
        }

        public UserDTO GetByID(string id)
        {
            var user = UserManager.Users.Single(u => u.Id == id);

            if (user != null)
            {
                return ConvertToDTO(user);
            }

            return null;
        }

        public UserDTO Add(UserDTO user)
        {
            if (UserManager.Users.Where(u => u.Email == user.Email).Any())
            {
                throw new Exception("User with the same email already exists.");      
            }

            var newUser = new ApplicationUser();
            newUser.UserName = user.Email;
            newUser.Email = user.Email;
            
            var adminresult = UserManager.Create(newUser, user.Password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                foreach(var roleName in user.Roles)
                {
                    //Find Role Admin
                    var role = RoleManager.FindByName(roleName);
                    var result = UserManager.AddToRole(newUser.Id, role.Name);

                    if (!result.Succeeded)
                    {
                        throw new Exception("Error creating user.");
                    }
                }
            }
            else
            {
                throw new Exception(MessageHelper.GetErrors(adminresult));
            }

            user.ID = newUser.Id;
            return user;
        }

        public void Remove(string id)
        {
            var user = UserManager.FindById(id);
            var logins = user.Logins;
            foreach (var login in logins)
            {
                UserManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            }

            var rolesForUser = UserManager.GetRoles(id);

            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser.ToList())
                {
                    // item should be the name of the role
                    var result = UserManager.RemoveFromRole(user.Id, item);
                }
            }

            UserManager.Delete(user);
        }

        public void Update(UserDTO user)
        {
            var dbUser = UserManager.FindById(user.ID);
            dbUser.Email = user.Email;
            dbUser.UserName = user.Email;

            UserManager.Update(dbUser);

            var rolesForUser = UserManager.GetRoles(user.ID);

            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser.ToList())
                {  
                    // item should be the name of the role
                    var result = UserManager.RemoveFromRole(user.ID, item);
                }
            }

            foreach (var roleName in user.Roles)
            {
                //Find Role Admin
                var role = RoleManager.FindByName(roleName);
                var result = UserManager.AddToRole(user.ID, role.Name);

                if (!result.Succeeded)
                {
                    throw new Exception("Error updating roles.");
                }
            }
            
        }

        public IEnumerable<string> GetAvailableRoles() {
            return RoleManager.Roles.Select(r => r.Name).ToList();
        }

        private UserDTO ConvertToDTO(ApplicationUser user)
        {
            var userDTO = new UserDTO();
            var roles = UserManager.GetRoles(user.Id);

            userDTO.ID = user.Id;
            userDTO.Email = user.Email;
            userDTO.Roles = roles;
            userDTO.AvailableRoles = RoleManager.Roles.Select(r => r.Name).ToList();

            return userDTO;
        }
    }
}