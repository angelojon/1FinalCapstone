using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace _1FinalCapstone.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetFirstName(this IIdentity identity)
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(identity.GetUserId());

            if (user != null)
            {
                return user.FirstName; // Assuming 'FirstTable' is the column name for the first name
            }

            return null;
        }
        public static string GetLastName(this IIdentity identity)
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(identity.GetUserId());

            if (user != null)
            {
                return user.LastName; // Assuming 'FirstTable' is the column name for the first name
            }

            return null;
        }

        public static string GetEmail(this IIdentity identity)
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(identity.GetUserId());

            if (user != null)
            {
                return user.Email; // Assuming 'FirstTable' is the column name for the first name
            }

            return null;
        }


        public static string GetContactNumber(this IIdentity identity)
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(identity.GetUserId());

            if (user != null)
            {
                return user.ContactNumber; // Assuming 'FirstTable' is the column name for the first name
            }

            return null;
        }


        public static string GetAddress(this IIdentity identity)
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(identity.GetUserId());

            if (user != null)
            {
                return user.Address; // Assuming 'FirstTable' is the column name for the first name
            }

            return null;
        }

     
    }
}