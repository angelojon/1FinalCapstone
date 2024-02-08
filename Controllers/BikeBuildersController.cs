using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _1FinalCapstone.Models;

namespace _1FinalCapstone.Controllers
{
    public class BikeBuildersController : Controller
    {

        private Entities db = new Entities();

        [Authorize(Roles = "Admin,Owner")]

        public ActionResult Index()
        {
            try
            {
                var build = db.BikeBuilder
                              .Where(bikeBuilder => bikeBuilder.ArchiveStatus != "Archive")
                              .ToList();

                // Filter the data to include only rows with StepTitle starting with 'F' or 'f'
                build = build.Where(bikeBuilder => bikeBuilder.StepTitle != null && bikeBuilder.StepTitle.StartsWith("S", StringComparison.OrdinalIgnoreCase)).ToList();

                foreach (var bikeBuilder in build)
                {
                    if (bikeBuilder.BImage1 != null)
                        bikeBuilder.Budimage1 = Convert.ToBase64String(bikeBuilder.BImage1);

                    if (bikeBuilder.BImage2 != null)
                        bikeBuilder.Budimage2 = Convert.ToBase64String(bikeBuilder.BImage2);

                    if (bikeBuilder.BImage3 != null)
                        bikeBuilder.Budimage3 = Convert.ToBase64String(bikeBuilder.BImage3);
                }

                return View("Index", build);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                return View("Error"); // Return an error view
            }
        }






        [Authorize(Roles = "Admin,Owner")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeBuilder bikeBuilder = db.BikeBuilder.Find(id);
            if (bikeBuilder == null)
            {
                return HttpNotFound();
            }

            var viewModel = new BikeBuilder
            {
                BuilderId = bikeBuilder.BuilderId,
                StepTitle = bikeBuilder.StepTitle,
                BuilderTitle1 = bikeBuilder.BuilderTitle1,
                BuilderDescription1 = bikeBuilder.BuilderDescription1,
                Bprice1 = bikeBuilder.Bprice1,
                BuilderTitle2 = bikeBuilder.BuilderTitle2,
                BuilderDescription2 = bikeBuilder.BuilderDescription2,
                Bprice2 = bikeBuilder.Bprice2,
                BuilderTitle3 = bikeBuilder.BuilderTitle3,
                BuilderDescription3 = bikeBuilder.BuilderDescription3,
                Bprice3 = bikeBuilder.Bprice3,
                ArchiveStatus = bikeBuilder.ArchiveStatus
            };

            // Check if an existing image exists and populate the view model
            if (bikeBuilder.BImage1 != null)
            {
                viewModel.BImage1 = bikeBuilder.BImage1;
            }

            if (bikeBuilder.BImage2 != null)
            {
                viewModel.BImage2 = bikeBuilder.BImage2;
            }

            if (bikeBuilder.BImage3 != null)
            {
                viewModel.BImage3 = bikeBuilder.BImage3;
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BikeBuilder bikeBuilder, HttpPostedFileBase BudImage1, HttpPostedFileBase BudImage2, HttpPostedFileBase BudImage3)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var existingBikeBuilder = db.BikeBuilder.Find(bikeBuilder.BuilderId);
                        if (existingBikeBuilder == null)
                        {
                            return HttpNotFound();
                        }

                        existingBikeBuilder.StepTitle = bikeBuilder.StepTitle;
                        existingBikeBuilder.BuilderTitle1 = bikeBuilder.BuilderTitle1;
                        existingBikeBuilder.BuilderDescription1 = bikeBuilder.BuilderDescription1;
                        existingBikeBuilder.Bprice1 = bikeBuilder.Bprice1;
                        existingBikeBuilder.BuilderTitle2 = bikeBuilder.BuilderTitle2;
                        existingBikeBuilder.BuilderDescription2 = bikeBuilder.BuilderDescription2;
                        existingBikeBuilder.Bprice2 = bikeBuilder.Bprice2;
                        existingBikeBuilder.BuilderTitle3 = bikeBuilder.BuilderTitle3;
                        existingBikeBuilder.BuilderDescription3 = bikeBuilder.BuilderDescription3;
                        existingBikeBuilder.Bprice3 = bikeBuilder.Bprice3;
                        existingBikeBuilder.ArchiveStatus = bikeBuilder.ArchiveStatus;

                        // Only update the image properties if new images are provided
                        if (BudImage1 != null && BudImage1.ContentLength > 0)
                        {
                            var image1Data = new byte[BudImage1.ContentLength];
                            BudImage1.InputStream.Read(image1Data, 0, BudImage1.ContentLength);
                            existingBikeBuilder.BImage1 = image1Data;
                        }

                        if (BudImage2 != null && BudImage2.ContentLength > 0)
                        {
                            var image2Data = new byte[BudImage2.ContentLength];
                            BudImage2.InputStream.Read(image2Data, 0, BudImage2.ContentLength);
                            existingBikeBuilder.BImage2 = image2Data;
                        }

                        if (BudImage3 != null && BudImage3.ContentLength > 0)
                        {
                            var image3Data = new byte[BudImage3.ContentLength];
                            BudImage3.InputStream.Read(image3Data, 0, BudImage3.ContentLength);
                            existingBikeBuilder.BImage3 = image3Data;
                        }

                        db.SaveChanges();
                        transaction.Commit();

                        return RedirectToAction("Index"); // Assuming you have an Index action
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Handle or log the validation errors
                    }
                    catch (Exception)
                    {
                        // Handle or log the exception
                    }
                }
            }

            // If the ModelState is invalid or an exception occurred, return the view with the provided viewModel
            return View(bikeBuilder);
        }





        // GET: BikeBuilders/Delete/5









        [Authorize(Roles = "Admin,Owner")]

        public ActionResult Index2()
        {
            try
            {
                var build = db.BikeBuilder
                              .Where(bikeBuilder => bikeBuilder.ArchiveStatus != "Archive")
                              .ToList();

                // Filter the data to include only rows with StepTitle starting with 'F' or 'f'
                build = build.Where(bikeBuilder => bikeBuilder.StepTitle != null && bikeBuilder.StepTitle.StartsWith("F", StringComparison.OrdinalIgnoreCase)).ToList();

                foreach (var bikeBuilder in build)
                {
                    if (bikeBuilder.BImage1 != null)
                        bikeBuilder.Budimage1 = Convert.ToBase64String(bikeBuilder.BImage1);

                    if (bikeBuilder.BImage2 != null)
                        bikeBuilder.Budimage2 = Convert.ToBase64String(bikeBuilder.BImage2);

                    if (bikeBuilder.BImage3 != null)
                        bikeBuilder.Budimage3 = Convert.ToBase64String(bikeBuilder.BImage3);
                }

                return View("Index2", build);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                return View("Error"); // Return an error view
            }
        }


        [Authorize(Roles = "Admin,Owner")]

        public ActionResult Edit2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeBuilder bikeBuilder = db.BikeBuilder.Find(id);
            if (bikeBuilder == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BikeBuilder
            {
                BuilderId = bikeBuilder.BuilderId,
                StepTitle = bikeBuilder.StepTitle,
                BuilderTitle1 = bikeBuilder.BuilderTitle1,
                BuilderDescription1 = bikeBuilder.BuilderDescription1,
                Bprice1 = bikeBuilder.Bprice1,
                BImage1 = bikeBuilder.BImage1,
                ArchiveStatus = bikeBuilder.ArchiveStatus,
                ExistingImageData1 = bikeBuilder.BImage1,
                ExistingImageData2 = bikeBuilder.BImage2,
                ExistingImageData3 = bikeBuilder.BImage3,

            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2(BikeBuilder bikeBuilder, HttpPostedFileBase BudImage1, HttpPostedFileBase BudImage2, HttpPostedFileBase BudImage3)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var existingBikeBuilder = db.BikeBuilder.Find(bikeBuilder.BuilderId);
                        if (existingBikeBuilder == null)
                        {
                            return HttpNotFound();
                        }

                        // Update properties
                        existingBikeBuilder.StepTitle = bikeBuilder.StepTitle;
                        existingBikeBuilder.BuilderTitle1 = bikeBuilder.BuilderTitle1;
                        existingBikeBuilder.BuilderDescription1 = bikeBuilder.BuilderDescription1;
                        existingBikeBuilder.Bprice1 = bikeBuilder.Bprice1;
                        existingBikeBuilder.ArchiveStatus = bikeBuilder.ArchiveStatus;

                        // Update image data only if new images are provided
                        if (BudImage1 != null && BudImage1.ContentLength > 0)
                        {
                            var image1Data = new byte[BudImage1.ContentLength];
                            BudImage1.InputStream.Read(image1Data, 0, BudImage1.ContentLength);
                            existingBikeBuilder.BImage1 = image1Data;
                        }

                        if (BudImage2 != null && BudImage2.ContentLength > 0)
                        {
                            var image2Data = new byte[BudImage2.ContentLength];
                            BudImage2.InputStream.Read(image2Data, 0, BudImage2.ContentLength);
                            existingBikeBuilder.BImage2 = image2Data;
                        }

                        if (BudImage3 != null && BudImage3.ContentLength > 0)
                        {
                            var image3Data = new byte[BudImage3.ContentLength];
                            BudImage3.InputStream.Read(image3Data, 0, BudImage3.ContentLength);
                            existingBikeBuilder.BImage3 = image3Data;
                        }
                        else
                        {
                            // If no new image was provided, retain the existing image data
                            if (existingBikeBuilder.BImage1 != null)
                            {
                                existingBikeBuilder.BImage1 = existingBikeBuilder.BImage1;
                            }

                            if (existingBikeBuilder.BImage2 != null)
                            {
                                existingBikeBuilder.BImage2 = existingBikeBuilder.BImage2;
                            }

                            if (existingBikeBuilder.BImage3 != null)
                            {
                                existingBikeBuilder.BImage3 = existingBikeBuilder.BImage3;
                            }
                        }

                        db.SaveChanges();
                        transaction.Commit();

                        return RedirectToAction("Index2"); // Assuming you have an Index2 action
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Handle or log the validation errors
                    }
                    catch (Exception)
                    {
                        // Handle or log the exception
                    }
                }
            }

            // If the ModelState is invalid or an exception occurred, return the view with the provided viewModel
            return View(bikeBuilder);
        }
 
       

    }
}
