using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;

namespace TimeZoneManager.API.Helpers
{
    public static class MessageHelper
    {
        public static string GetErrorsFromModelState(ModelStateDictionary modelState)
        {
            var sbErrors = new StringBuilder();
            foreach (var key in modelState.Keys)
            {
                var dotIndex = key.LastIndexOf('.');
                var fixedKey = (dotIndex > 0) ? key.Substring(dotIndex + 1, key.Length - dotIndex - 1) : key;

                // Only send the errors to the client.
                if (modelState[key].Errors.Count > 0)
                {
                    sbErrors.Append(fixedKey);
                    sbErrors.Append(":");
                    foreach (var error in modelState[key].Errors)
                    {
                        sbErrors.Append(error.ErrorMessage);
                    }
                    
                }
            }

            return sbErrors.ToString();
        }

        public static string GetErrors(IdentityResult result) {
            return string.Join(" ", result.Errors);
        }
    }
}