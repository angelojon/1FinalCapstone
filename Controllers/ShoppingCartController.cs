// ShoppingCartController.cs
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using _1FinalCapstone.Models;
using System;

namespace _1FinalCapstone.Controllers
{
    public class ShoppingCartController : Controller
    {
        private Entities db = new Entities();

        public ActionResult CartM()
        {
            // Check if the user is authenticated (logged in)
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if the user is not logged in
                return RedirectToAction("Login", "Account"); // Adjust the login route as per your application
            }

            // Get the email of the logged-in user
                string userEmail = User.Identity.Name;

                // Fetch data based on the email of the logged-in user
                var cart = db.Cart.Where(c => c.Email == userEmail).ToList();
                var cartBuilder = db.CartBuilder.Where(cb => cb.Email == userEmail).ToList();
                var products = db.Product.ToList();

                var viewModel = new Merge
                {
                    Cart = cart,
                    CartBuilder = cartBuilder,

                };
                
                return View(viewModel); // Pass a single instance of Merge to the view
            }

    

    }



    }




