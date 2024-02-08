using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _1FinalCapstone.Models;

namespace _1FinalCapstone.Controllers
{
    public class ProductsController : Controller
    {
        private Entities db = new Entities();

        [Authorize(Roles = "Admin,Owner")]

        public ActionResult Index()
        {
            var parts = db.Product.Where(p => p.ArchiveStatus != "Archive").ToList();

            foreach (var product in parts)
            {
                if (product.Image != null)
                    product.ImageProd = Convert.ToBase64String(product.Image);
            }

            return View(parts);
        }

        // GET: Customers/Details/5


        // GET: Customers/Create
        [Authorize(Roles = "Admin,Owner")]

        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product bikeDetails, HttpPostedFileBase prodImage)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (prodImage != null && prodImage.ContentLength > 0)
                    {
                        var bikeImage = new byte[prodImage.ContentLength];
                        prodImage.InputStream.Read(bikeImage, 0, prodImage.ContentLength);

                        bikeDetails.Image = bikeImage;
                    }
                    bikeDetails.ArchiveStatus = "Unarchive";
                    db.Product.Add(bikeDetails);
                    db.SaveChanges();
                    transaction.Commit();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    // Handle or log the validation errors
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(validationResult => validationResult.ValidationErrors)
                        .Select(error => error.ErrorMessage);

                    // Log or handle the error messages as per your requirements
                    foreach (var errorMessage in errorMessages)
                    {
                        // You can log the errors or handle them in any other way (e.g., displaying them to the user)
                        // For example, to log the errors using the Trace class:
                        System.Diagnostics.Trace.WriteLine(errorMessage);
                    }

                    transaction.Rollback();
                    throw;
                }
            }
        }
        [Authorize(Roles = "Admin,Owner")]


        // GET: BikeParts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product bikeDetails = db.Product.Find(id);
            if (bikeDetails == null)
            {
                return HttpNotFound();
            }

            // Create a view model instance and populate properties
            var viewModel = new Product
            {
                ProductId = bikeDetails.ProductId,
                ItemName = bikeDetails.ItemName,
                ItemDesc = bikeDetails.ItemDesc,
                ItemQuantity = bikeDetails.ItemQuantity,
                ItemCost = bikeDetails.ItemCost,
                ItemPrice = bikeDetails.ItemPrice,
                ComponentType = bikeDetails.ComponentType,
                ExistingImageDataP = bikeDetails.Image, // Assuming the image property in Product is named "Image"
                ArchiveStatus = bikeDetails.ArchiveStatus
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product bikeDetails, HttpPostedFileBase prodImage)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = db.Product.Find(bikeDetails.ProductId);
                if (existingProduct == null)
                {
                    return HttpNotFound();
                }

                // Update other properties from the edited form
                existingProduct.ItemName = bikeDetails.ItemName;
                existingProduct.ItemDesc = bikeDetails.ItemDesc;
                existingProduct.ItemQuantity = bikeDetails.ItemQuantity;
                existingProduct.ItemCost = bikeDetails.ItemCost;
                existingProduct.ItemPrice = bikeDetails.ItemPrice;
                existingProduct.ComponentType = bikeDetails.ComponentType;
                existingProduct.ArchiveStatus = bikeDetails.ArchiveStatus;
                if (prodImage != null && prodImage.ContentLength > 0)
                {
                    var bikeImage = new byte[prodImage.ContentLength];
                    prodImage.InputStream.Read(bikeImage, 0, prodImage.ContentLength);
                    existingProduct.Image = bikeImage;
                }

                db.SaveChanges(); // Save changes to the database

                return RedirectToAction("Index"); // Redirect to the index action
            }

            return View(bikeDetails);
        }




        // GET: Customers/Delete/5
       


        public ActionResult ArchiveAction(int id)
        {
            var bikedetails = db.Product.Find(id); // Assuming you have a DbContext named "db"

            if (bikedetails != null)
            {
                if (bikedetails.ArchiveStatus == "Archive")
                {
                    bikedetails.ArchiveStatus = "Unarchive";
                }
                else if (bikedetails.ArchiveStatus == "Unarchive")
                {
                    bikedetails.ArchiveStatus = "Archive";
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index"); // Redirect back to the index page
        }
        [Authorize(Roles = "Admin,Owner")]

        public ActionResult ArchiveItems()
        {
            var parts = db.Product.Where(p => p.ArchiveStatus != "Unarchive").ToList();

            foreach (var product in parts)
            {
                if (product.Image != null)
                    product.ImageProd = Convert.ToBase64String(product.Image);
            }

            return View(parts);
        }
        public ActionResult UnArchiveAction(int id)
        {
            var bikedetails = db.Product.Find(id); // Assuming you have a DbContext named "db"

            if (bikedetails != null)
            {
                if (bikedetails.ArchiveStatus == "Archive")
                {
                    bikedetails.ArchiveStatus = "Unarchive";
                }
                else if (bikedetails.ArchiveStatus == "Unarchive")
                {
                    bikedetails.ArchiveStatus = "Archive";
                }

                db.SaveChanges();
            }

            return RedirectToAction("ArchiveItems"); // Redirect back to the index page
        }

    }
}

