using _1FinalCapstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _1FinalCapstone.Controllers
{
    public class CustomerBuilderController : Controller
    {
        private Entities db = new Entities();
        // GET: CustomerBuilder
        //[Authorize(Roles = "Admin, Customer")]

      

        public ActionResult BuilderMain()
        {
            try
            {
                var build = db.BikeBuilder.ToList();

                foreach (var bikeBuilder in build)
                {
                    if (bikeBuilder.BImage1 != null)
                        bikeBuilder.Budimage1 = Convert.ToBase64String(bikeBuilder.BImage1);

                    if (bikeBuilder.BImage2 != null)
                        bikeBuilder.Budimage2 = Convert.ToBase64String(bikeBuilder.BImage2);

                    if (bikeBuilder.BImage3 != null)
                        bikeBuilder.Budimage3 = Convert.ToBase64String(bikeBuilder.BImage3);
                }

                var mergeModel = new Merge
                {
                    BikeBuilders = build
                };

                return View(mergeModel);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                return View("Error"); // Return an error view
            }
        }

        public ActionResult GetPartialView(int builderId)
        {
            // Define a list of Builder IDs for each case
            List<int> selectedBuilderIdsForPartial1 = new List<int> { 3, 4, };
            List<int> selectedBuilderIdsForPartial2 = new List<int> { 5, 6, };


            List<int> selectedBuilderIds;

            switch (builderId)
            {
                case 1:
                    selectedBuilderIds = selectedBuilderIdsForPartial1;
                    break;
                case 2:
                    selectedBuilderIds = selectedBuilderIdsForPartial2;
                    break;

                default:
                    return Content("Invalid builder selection");
            }

            // Fetch data for the selected Builder IDs
            var builderData = db.BikeBuilder.Where(b => selectedBuilderIds.Contains(b.BuilderId)).ToList();

            foreach (var bikeBuilder in builderData)
            {
                if (bikeBuilder.BImage1 != null)
                    bikeBuilder.Budimage1 = Convert.ToBase64String(bikeBuilder.BImage1);

                if (bikeBuilder.BImage2 != null)
                    bikeBuilder.Budimage2 = Convert.ToBase64String(bikeBuilder.BImage2);

                if (bikeBuilder.BImage3 != null)
                    bikeBuilder.Budimage3 = Convert.ToBase64String(bikeBuilder.BImage3);
            }

            // Create a model and return the appropriate partial view
            var mergeModel = new Merge
            {
                BikeBuilders = builderData
            };

            switch (builderId)
            {
                case 1:
                    return PartialView("_BuilderIds34", mergeModel);
                case 2:
                    return PartialView("_BuilderIds56", mergeModel);

                default:
                    return Content("Invalid builder selection");
            }
        }



        public ActionResult LoadPartialView(int builderId)
        {
            // Define a list of Builder IDs for each case
            List<int> selectedBuilderIdsForPartial1 = new List<int> { 7, 8 };
            List<int> selectedBuilderIdsForPartial2 = new List<int> { 9, 10 };

            List<int> selectedBuilderIds;

            switch (builderId)
            {

                case 3:
                    // For builderId 3, show Builder IDs 7 and 8 in the partial view
                    selectedBuilderIds = new List<int> { 7, 8 };
                    break;
                case 4:
                    // For builderId 3, show Builder IDs 7 and 8 in the partial view
                    selectedBuilderIds = new List<int> { 9, 10 };
                    break;
                case 5:
                    // For builderId 3, show Builder IDs 7 and 8 in the partial view
                    selectedBuilderIds = new List<int> { 11, 12 };
                    break;
                case 6:
                    // For builderId 3, show Builder IDs 7 and 8 in the partial view
                    selectedBuilderIds = new List<int> { 13, 14 };
                    break;


                default:
                    return Content("Invalid builder selection");
            }

            // Fetch data for the selected Builder IDs
            var builderData = db.BikeBuilder.Where(b => selectedBuilderIds.Contains(b.BuilderId)).ToList();

            foreach (var bikeBuilder in builderData)
            {
                if (bikeBuilder.BImage1 != null)
                    bikeBuilder.Budimage1 = Convert.ToBase64String(bikeBuilder.BImage1);

                if (bikeBuilder.BImage2 != null)
                    bikeBuilder.Budimage2 = Convert.ToBase64String(bikeBuilder.BImage2);

                if (bikeBuilder.BImage3 != null)
                    bikeBuilder.Budimage3 = Convert.ToBase64String(bikeBuilder.BImage3);
            }

            // Create a model and return the appropriate partial view
            var mergeModel = new Merge
            {
                BikeBuilders = builderData
            };

            switch (builderId)
            {

                case 3:
                    return PartialView("_BuilderIds78", mergeModel); // Display Builder IDs 7 and 8 for builderId 3
                case 4:
                    return PartialView("_BuilderIds910", mergeModel); // Display Builder IDs 7 and 8 for builderId 3
                case 5:
                    return PartialView("_BuilderIds1112", mergeModel); // Display Builder IDs 7 and 8 for builderId 3
                case 6:
                    return PartialView("_BuilderIds1314", mergeModel); // Display Builder IDs 7 and 8 for builderId 3

                default:
                    return Content("Invalid builder selection");
            }
        }




        public ActionResult LoadPartialView1(int builderId)
        {
            List<int> selectedBuilderIds;

            switch (builderId)
            {
                case 7:
                    // For builderId 7, show Builder IDs 15 and 16 in the partial view
                    selectedBuilderIds = new List<int> { 15, 16 };
                    break;
                case 8:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 17, 18 };
                    break;
                case 9:
                    // For builderId 7, show Builder IDs 15 and 16 in the partial view
                    selectedBuilderIds = new List<int> { 19, 20 };
                    break;
                case 10:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 21, 22 };
                    break;



                default:
                    return Content("Invalid builder selection");
            }

            // Fetch data for the selected Builder IDs
            var builderData = db.BikeBuilder.Where(b => selectedBuilderIds.Contains(b.BuilderId)).ToList();

            foreach (var bikeBuilder in builderData)
            {
                if (bikeBuilder.BImage1 != null)
                    bikeBuilder.Budimage1 = Convert.ToBase64String(bikeBuilder.BImage1);

                if (bikeBuilder.BImage2 != null)
                    bikeBuilder.Budimage2 = Convert.ToBase64String(bikeBuilder.BImage2);

                if (bikeBuilder.BImage3 != null)
                    bikeBuilder.Budimage3 = Convert.ToBase64String(bikeBuilder.BImage3);
            }

            // Create a model and return the appropriate partial view
            var mergeModel = new Merge
            {
                BikeBuilders = builderData
            };

            switch (builderId)
            {
                case 7:
                    return PartialView("_BuilderIds1516", mergeModel); // Display Builder IDs 15 and 16 for builderId 7
                case 8:
                    return PartialView("_BuilderIds1718", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 9:
                    return PartialView("_BuilderIds1920", mergeModel); // Display Builder IDs 15 and 16 for builderId 7
                case 10:
                    return PartialView("_BuilderIds2122", mergeModel); // Display Builder IDs 17 and 18 for builderId 8

                default:
                    return Content("Invalid builder selection");
            }
        }
        public ActionResult LoadPartialView2(int builderId)
        {
            List<int> selectedBuilderIds;

            switch (builderId)
            {
                case 11:
                    // For builderId 7, show Builder IDs 15 and 16 in the partial view
                    selectedBuilderIds = new List<int> { 23, 24 };
                    break;
                case 12:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 25, 26 };
                    break;
                case 13:
                    // For builderId 7, show Builder IDs 15 and 16 in the partial view
                    selectedBuilderIds = new List<int> { 27, 28 };
                    break;
                case 14:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 29, 30 };
                    break;



                default:
                    return Content("Invalid builder selection");
            }

            // Fetch data for the selected Builder IDs
            var builderData = db.BikeBuilder.Where(b => selectedBuilderIds.Contains(b.BuilderId)).ToList();

            foreach (var bikeBuilder in builderData)
            {
                if (bikeBuilder.BImage1 != null)
                    bikeBuilder.Budimage1 = Convert.ToBase64String(bikeBuilder.BImage1);

                if (bikeBuilder.BImage2 != null)
                    bikeBuilder.Budimage2 = Convert.ToBase64String(bikeBuilder.BImage2);

                if (bikeBuilder.BImage3 != null)
                    bikeBuilder.Budimage3 = Convert.ToBase64String(bikeBuilder.BImage3);
            }

            // Create a model and return the appropriate partial view
            var mergeModel = new Merge
            {
                BikeBuilders = builderData
            };

            switch (builderId)
            {
                case 11:
                    return PartialView("_BuilderIds2324", mergeModel); // Display Builder IDs 15 and 16 for builderId 7
                case 12:
                    return PartialView("_BuilderIds2526", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 13:
                    return PartialView("_BuilderIds2728", mergeModel); // Display Builder IDs 15 and 16 for builderId 7
                case 14:
                    return PartialView("_BuilderIds2930", mergeModel); // Display Builder IDs 17 and 18 for builderId 8

                default:
                    return Content("Invalid builder selection");
            }
        }



        public ActionResult LoadPartialView3(int? builderId)
        {
            List<int> selectedBuilderIds;

            switch (builderId)
            {
                case 15:
                    // For builderId 7, show Builder IDs 15 and 16 in the partial view
                    selectedBuilderIds = new List<int> { 31, 32 };
                    break;
                case 16:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 33, 34 };
                    break;
                case 17:
                    // For builderId 7, show Builder IDs 15 and 16 in the partial view
                    selectedBuilderIds = new List<int> { 35, 36 };
                    break;
                case 18:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 37, 38 };
                    break;

                case 19:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 39, 40 };
                    break;
                case 20:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 41, 42 };
                    break;

                case 21:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 43, 44 };
                    break;
                case 22:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 45, 46 };
                    break;
                case 23:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 47, 48 };
                    break;
                case 24:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 49, 50 };
                    break;
                case 25:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 51, 52 };
                    break;
                case 26:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 53, 54 };
                    break;
                case 27:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 55, 56 };
                    break;
                case 28:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 57, 58 };
                    break;
                case 29:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 59, 60 };
                    break;
                case 30:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 61, 62 };
                    break;
                case 31:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 15, 16 };
                    break;
                case 32:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 15, 16 };
                    break;
                case 33:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 15, 16 };
                    break;
                case 34:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 15, 16 };
                    break;
                case 35:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 17, 18 };
                    break;
                case 36:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 17, 18 };
                    break;
                case 37:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 17, 18 };
                    break;
                case 38:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 17, 18 };
                    break;
                case 39:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 19, 20 };
                    break;
                case 40:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 19, 20 };
                    break;
                case 41:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 19, 20 };
                    break;
                case 42:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 19, 20 };
                    break;
                case 43:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 21, 22 };
                    break;
                case 44:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 21, 22 };
                    break;
                case 45:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 21, 22 };
                    break;
                case 46:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 21, 22 };
                    break;
                case 47:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 23, 24 };
                    break;
                case 48:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 23, 24 };
                    break;
                case 49:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 23, 24 };
                    break;
                case 50:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 23, 24 };
                    break;
                case 51:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 25, 26 };
                    break;
                case 52:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 25, 26 };
                    break;
                case 53:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 25, 26 };
                    break;
                case 54:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 25, 26 };
                    break;
                case 55:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 27, 28 };
                    break;
                case 56:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 27, 28 };
                    break;
                case 57:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 27, 28 };
                    break;
                case 58:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 27, 28 };
                    break;
                case 59:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 29, 30 };
                    break;
                case 60:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 29, 30 };
                    break;
                case 61:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 29, 30 };
                    break;
                case 62:
                    // For builderId 8, show Builder IDs 17 and 18 in the partial view
                    selectedBuilderIds = new List<int> { 29, 30 };
                    break;


                default:
                    return Content("Invalid builder selection111");
            }

            // Fetch data for the selected Builder IDs
            var builderData = db.BikeBuilder.Where(b => selectedBuilderIds.Contains(b.BuilderId)).ToList();

            foreach (var bikeBuilder in builderData)
            {
                if (bikeBuilder.BImage1 != null)
                    bikeBuilder.Budimage1 = Convert.ToBase64String(bikeBuilder.BImage1);

                if (bikeBuilder.BImage2 != null)
                    bikeBuilder.Budimage2 = Convert.ToBase64String(bikeBuilder.BImage2);

                if (bikeBuilder.BImage3 != null)
                    bikeBuilder.Budimage3 = Convert.ToBase64String(bikeBuilder.BImage3);
            }

            // Create a model and return the appropriate partial view
            var mergeModel = new Merge
            {
                BikeBuilders = builderData
            };

            switch (builderId)
            {
                case 15:
                    return PartialView("_BuilderIds3132", mergeModel); // Display Builder IDs 15 and 16 for builderId 7
                case 16:
                    return PartialView("_BuilderIds3334", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 17:
                    return PartialView("_BuilderIds3536", mergeModel); // Display Builder IDs 15 and 16 for builderId 7
                case 18:
                    return PartialView("_BuilderIds3738", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 19:
                    return PartialView("_BuilderIds3940", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 20:
                    return PartialView("_BuilderIds4142", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 21:
                    return PartialView("_BuilderIds4344", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 22:
                    return PartialView("_BuilderIds4546", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 23:
                    return PartialView("_BuilderIds4748", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 24:
                    return PartialView("_BuilderIds4950", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 25:
                    return PartialView("_BuilderIds5152", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 26:
                    return PartialView("_BuilderIds5354", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 27:
                    return PartialView("_BuilderIds5556", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 28:
                    return PartialView("_BuilderIds5758", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 29:
                    return PartialView("_BuilderIds5960", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 30:
                    return PartialView("_BuilderIds6162", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 31:
                    return PartialView("_BuilderIds1516", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 32:
                    return PartialView("_BuilderIds1516", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 33:
                    return PartialView("_BuilderIds1516", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 34:
                    return PartialView("_BuilderIds1516", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 35:
                    return PartialView("_BuilderIds1718", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 36:
                    return PartialView("_BuilderIds1718", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 37:
                    return PartialView("_BuilderIds1718", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 38:
                    return PartialView("_BuilderIds1718", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 39:
                    return PartialView("_BuilderIds1920", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 40:
                    return PartialView("_BuilderIds1920", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 41:
                    return PartialView("_BuilderIds1920", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 42:
                    return PartialView("_BuilderIds1920", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 43:
                    return PartialView("_BuilderIds2122", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 44:
                    return PartialView("_BuilderIds2122", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 45:
                    return PartialView("_BuilderIds2122", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 46:
                    return PartialView("_BuilderIds2122", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 47:
                    return PartialView("_BuilderIds2324", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 48:
                    return PartialView("_BuilderIds2324", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 49:
                    return PartialView("_BuilderIds2324", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 50:
                    return PartialView("_BuilderIds2324", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 51:
                    return PartialView("_BuilderIds2526", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 52:
                    return PartialView("_BuilderIds2526", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 53:
                    return PartialView("_BuilderIds2526", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 54:
                    return PartialView("_BuilderIds2526", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 55:
                    return PartialView("_BuilderIds2728", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 56:
                    return PartialView("_BuilderIds2728", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 57:
                    return PartialView("_BuilderIds2728", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 58:
                    return PartialView("_BuilderIds2728", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 59:
                    return PartialView("_BuilderIds2930", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 60:
                    return PartialView("_BuilderIds2930", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 61:
                    return PartialView("_BuilderIds2930", mergeModel); // Display Builder IDs 17 and 18 for builderId 8
                case 62:
                    return PartialView("_BuilderIds2930", mergeModel); // Display Builder IDs 17 and 18 for builderId 8

                default:
                    return Content("Invalid builder selection");
            }
        }

        [HttpPost]
        public ActionResult FinalStep(int? builderSelection6)
        {
            // Map the selected builderSelection6 to the corresponding BuilderId
            int mappedBuilderId = builderSelection6.HasValue ? builderSelection6.Value + 32 : 0; // Default to 0 if builderSelection6 is null

            // Store the mapped BuilderId in session
            Session["SelectedBuilderId"] = mappedBuilderId;

            // Redirect to the Final action with the mapped BuilderId as a route parameter
            return RedirectToAction("Final", new { selectedBuilderId = mappedBuilderId });
        }

        public ActionResult Final(int selectedBuilderId)
        {
            // Define the mapping of builder IDs
            var builderIdMappings = new Dictionary<int, List<int>>
    {
        { 63, new List<int> { 15, 7, 3, 1 } }, // Map 63 to 31
        { 64, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 32
        { 65, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 35
        { 66, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 36
        { 67, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 37
        { 68, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 38
        { 69, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 39
        { 70, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 40
        { 71, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 41
        { 72, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 42
        { 73, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 43
        { 74, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 44
        { 75, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 45
        { 76, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 46
        { 77, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 47
        { 78, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 48
        { 79, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 49
        { 80, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 50
        { 81, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 51
        { 82, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 52
        { 83, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 53
        { 84, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 54
        { 85, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 55
        { 86, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 56
        { 87, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 57
        { 88, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 58
        { 89, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 59
        { 90, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 60
        { 91, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 61
        { 92, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 62
        { 93, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 61
        { 94, new List<int> { 15, 7, 3, 1 } }, // Map 64 to 62
        // Add other mappings here...
    };

            // Check if the selectedBuilderId is in the mapping
            if (builderIdMappings.ContainsKey(selectedBuilderId))
            {
                // Retrieve the list of builder IDs based on the mapping
                var additionalBuilderIds = builderIdMappings[selectedBuilderId];

                // Retrieve the selected builder and additional builders
                var selectedBuilder = db.BikeBuilder.FirstOrDefault(b => b.BuilderId == selectedBuilderId);
                var additionalBuilders = db.BikeBuilder
                    .Where(b => additionalBuilderIds.Contains(b.BuilderId))
                    .Select(b => b.BuilderTitle1)
                    .ToList();

                // Calculate the total Bprice1 by summing the Bprice1 values of selected builder and additional builders
                var totalBprice1 = selectedBuilder.Bprice1 + additionalBuilders.Sum(builderTitle =>
                {
                    var builder = db.BikeBuilder.FirstOrDefault(b => b.BuilderTitle1 == builderTitle);
                    return builder?.Bprice1 ?? 0; // Use 0 as default value if builder is not found
                });

                // Create a Merge model and set its properties
                var mergeModel = new Merge
                {
                    BikeBuilders = new List<BikeBuilder> { selectedBuilder },
                    BuilderDescriptions = additionalBuilders,
                    TotalBprice1 = totalBprice1 // Add the total Bprice1 to the Merge model
                };

                // Add the code snippet to convert images to base64 here (if needed)
                foreach (var bikeBuilder in mergeModel.BikeBuilders)
                {
                    if (bikeBuilder.BImage1 != null)
                        bikeBuilder.Budimage1 = Convert.ToBase64String(bikeBuilder.BImage1);

                    if (bikeBuilder.BImage2 != null)
                        bikeBuilder.Budimage2 = Convert.ToBase64String(bikeBuilder.BImage2);

                    if (bikeBuilder.BImage3 != null)
                        bikeBuilder.Budimage3 = Convert.ToBase64String(bikeBuilder.BImage3);
                }

                // Pass the mergeModel to the "Final" view
                return View("Final", mergeModel);
            }
            else
            {
                // If the selected builder is not found or not in the mapping, handle the error or redirect to an error view
                return View("Error"); // You can create an "Error" view for handling such cases
            }
        }














    }



}