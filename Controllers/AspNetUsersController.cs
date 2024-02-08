using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _1FinalCapstone.Models;

namespace _1FinalCapstone.Controllers
{
    public class AspNetUsersController : Controller
    {
        private Entities db = new Entities();

        // GET: AspNetUsers
        [Authorize(Roles = "Owner")]
        public ActionResult Index()
        {
            // Get all users from the database
            var allUsers = db.AspNetUsers.ToList();

            // Filter the users with AccountStatus "Disable"
            var filteredUsers = allUsers.Where(user => user.AccountStatus != "Disable").ToList();

            return View(filteredUsers);
        }

        [Authorize(Roles = "Owner")]
        public ActionResult DisableAccounts()
        {
            // Get all users from the database
            var allUsers = db.AspNetUsers.ToList();

            // Filter the users with AccountStatus "Disable"
            var disabledUsers = allUsers.Where(user => user.AccountStatus == "Disable").ToList();

            return View(disabledUsers);
        }

        public ActionResult ManageAccount()
        {
            string userEmail = User.Identity.Name;

            var user = db.AspNetUsers.FirstOrDefault(u => u.Email == userEmail);

            if (user != null)
            {
                // Retrieve the user's cart and cart builder information
                var cart = db.Cart.Where(c => c.Email == userEmail).ToList();
                var cartBuilder = db.CartBuilder.Where(cb => cb.Email == userEmail).ToList();
                var products = db.Product.ToList();

                // Create a view model to combine the user and cart information
                var viewModel = new Merge
                {
                    Cart = cart,
                    CartBuilder = cartBuilder,
                    // You can add more properties to the viewModel as needed
                };

                var userCollection = new List<AspNetUsers> { user };
                ViewBag.Products = products; // You can use ViewBag to pass data to the view

                return View(userCollection);
            }
            else
            {
                // Handle the case where the user's data is not found
                return View("NotFoundView");
            }
        }


        // GET: AspNetUsers/Details/5
     

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,LockoutEnabled,FirstName,LastName,CreateDate,Address,ContactNumber,UserName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult EditAccount(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,LockoutEnabled,FirstName,LastName,CreateDate,Address,ContactNumber,UserName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing user from the database
                var existingUser = db.AspNetUsers.Find(aspNetUsers.Id);
                if (existingUser == null)
                {
                    return HttpNotFound();
                }

                // Capture the old values
                var oldFirstName = existingUser.FirstName;
                var oldLastName = existingUser.LastName;
                var oldAddress = existingUser.Address;
                var oldContactNumber = existingUser.ContactNumber;

                // Update the properties you want to change
                existingUser.FirstName = aspNetUsers.FirstName;
                existingUser.LastName = aspNetUsers.LastName;
                existingUser.Address = aspNetUsers.Address;
                existingUser.ContactNumber = aspNetUsers.ContactNumber;

                // Retrieve the existing AuditLogs record for the user
                var auditLog = db.AuditLogs.FirstOrDefault(a => a.Email == existingUser.Email);

                if (auditLog != null)
                {
                    // Update the properties of the existing AuditLogs record
                    auditLog.FirstName = existingUser.FirstName;
                    auditLog.LastName = existingUser.LastName;
                    auditLog.ContactNumber = existingUser.ContactNumber;
                    auditLog.Address = existingUser.Address;
                    auditLog.ChangeBasicInfoDate = DateTime.Now; // Set the ChangeBasicInfoDate to the current date and time

                    // Set the old values
                    auditLog.OldFirstName = oldFirstName;
                    auditLog.OldLastName = oldLastName;
                    auditLog.OldContactNumber = oldContactNumber;
                    auditLog.OldAddress = oldAddress;

                    // Mark the entity as modified and save changes
                    db.Entry(existingUser).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("ManageAccount");
            }
            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Delete/5
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpPost]
        public ActionResult DisableUser(string userEmail)
        {
            // Find the user by their email address
            var user = db.AspNetUsers.FirstOrDefault(u => u.Email == userEmail);

            if (user != null)
            {
                // Update the AccountStatus property to "Disable"
                user.AccountStatus = "Disable";

                // Save changes to the database
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }

            // You can redirect the user back to the page they came from
            // or any other appropriate action, such as returning to the user list.
            return RedirectToAction("Index"); // Replace "Index" with your actual action name
        }
        [HttpPost]
        public ActionResult UnDisableUser(string userEmail)
        {
            // Find the user by their email address
            var user = db.AspNetUsers.FirstOrDefault(u => u.Email == userEmail);

            if (user != null)
            {
                // Update the AccountStatus property to "Disable"
                user.AccountStatus = "UnDisable";

                // Save changes to the database
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }

            // You can redirect the user back to the page they came from
            // or any other appropriate action, such as returning to the user list.
            return RedirectToAction("DisableAccounts"); // Replace "Index" with your actual action name
        }



    }

}
