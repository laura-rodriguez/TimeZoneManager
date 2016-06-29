//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using Microsoft.AspNet.Identity.Owin;
//using TimeZoneManager.Web.Models;

//namespace TimeZoneManager.Web.Auth
//{
//    public class APISignInManager : SignInManager<ApplicationUser, string>
//    {
//        public APISignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
//            : base(userManager, authenticationManager)
//        {
//        }

//        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
//        {
//            return base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
//        }
//    }
//}