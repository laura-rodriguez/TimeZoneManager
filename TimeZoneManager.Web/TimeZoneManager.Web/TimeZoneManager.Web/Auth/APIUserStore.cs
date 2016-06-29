using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using TimeZoneManager.Web.Models;
using System.Security.Claims;

namespace TimeZoneManager.Web.Auth
{
    public class APIUserStore :
        //IUserStore<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>,
        IUserStore<ApplicationUser>,
        IUserStore<ApplicationUser, string>,
        IUserLockoutStore<ApplicationUser,object>,
        IUserPasswordStore<ApplicationUser>,
        IUserClaimStore<ApplicationUser>
        
    {
        public Task AddClaimAsync(ApplicationUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<ApplicationUser> FindByIdAsync(object userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var pepe = new ApplicationUser();
            pepe.Id = userId;

            return Task.FromResult<ApplicationUser>(pepe);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var pepe = new ApplicationUser();
            pepe.Email = userName;
            pepe.SecurityStamp = Guid.NewGuid().ToString();
            return Task.FromResult<ApplicationUser>(pepe);
        }

        public Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(ApplicationUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();

            var identityUser = ToIdentityUser(user);
            identityUser.PasswordHash = passwordHash;
            var task = userStore.SetPasswordHashAsync(identityUser, passwordHash);
            SetApplicationUser(user, identityUser);

            return task;
            
                        
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        private static void SetApplicationUser(ApplicationUser user, IdentityUser identityUser)
        {
            user.PasswordHash = identityUser.PasswordHash;
            user.SecurityStamp = identityUser.SecurityStamp;
            user.Id = identityUser.Id;
            user.UserName = identityUser.UserName;
        }


    private IdentityUser ToIdentityUser(ApplicationUser user)
        {
            return new IdentityUser
            {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                UserName = user.UserName
            };
        }

    }
}