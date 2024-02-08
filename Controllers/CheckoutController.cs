using _1FinalCapstone.Extensions;
using _1FinalCapstone.Models;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.Data.Entity;
using static _1FinalCapstone.Controllers.HomeController;


namespace _1FinalCapstone.Controllers
{
    public class CheckoutController : Controller
    {
        private Entities db = new Entities();

        
        public ActionResult CustomerPlacedOrder()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Handle the case where the user is not authenticated (e.g., redirect to login)
                return RedirectToAction("Login", "Account"); // Adjust the action and controller names as needed
            }

            // Get the email of the logged-in user
            string userEmail = User.Identity.Name; // Assuming the email is stored in the Name property

            // Query the database to retrieve orders with status "Order Placed" for the logged-in user
            var orders = db.CheckoutTable
                .Where(order => order.OrderStatus == "Order Placed" && order.Email == userEmail)
                .ToList();

            // Your code to retrieve additional data
            var cart = db.Cart.Where(c => c.Email == userEmail).ToList();
            var cartBuilder = db.CartBuilder.Where(cb => cb.Email == userEmail).ToList();

            var viewModel = new Merge
            {
                Cart = cart,
                CartBuilder = cartBuilder,
                CheckoutTable = orders // Use the filtered "orders" list here
            };

            return View(viewModel);
        }

        public ActionResult CustomerAcceptedOrder()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Handle the case where the user is not authenticated (e.g., redirect to login)
                return RedirectToAction("Login", "Account"); // Adjust the action and controller names as needed
            }

            // Get the email of the logged-in user
            string userEmail = User.Identity.Name; // Assuming the email is stored in the Name property

            // Query the database to retrieve orders with status "Order Placed" for the logged-in user
            var orders = db.CheckoutTable
                .Where(order => order.OrderStatus == "Order Accepted" && order.Email == userEmail)
                .ToList();

            // Your code to retrieve additional data
            var cart = db.Cart.Where(c => c.Email == userEmail).ToList();
            var cartBuilder = db.CartBuilder.Where(cb => cb.Email == userEmail).ToList();

            var viewModel = new Merge
            {
                Cart = cart,
                CartBuilder = cartBuilder,
                CheckoutTable = orders // Use the filtered "orders" list here
            };

            return View(viewModel);
        }
        public ActionResult PrintOrderDetails(string orderName, int orderQuantity, decimal orderPrice, decimal totalPrice, string orderStatus, string paymentMode, string referenceNumber, string firstname, string lastname, string email)
        {
            // Pass data to the view
            ViewData["OrderName"] = orderName;
            ViewData["OrderQuantity"] = orderQuantity;
            ViewData["OrderPrice"] = orderPrice;
            ViewData["TotalPrice"] = totalPrice;
            ViewData["OrderStatus"] = orderStatus;
            ViewData["PaymentMode"] = paymentMode;
            ViewData["ReferenceNumber"] = referenceNumber;
            ViewData["FirstName"] = firstname;
            ViewData["LastName"] = lastname;
            ViewData["Email"] = email;

            return View();
        }






        public ActionResult CustomerHistory()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Handle the case where the user is not authenticated (e.g., redirect to login)
                return RedirectToAction("Login", "Account"); // Adjust the action and controller names as needed
            }

            // Get the email of the logged-in user
            string userEmail = User.Identity.Name; // Assuming the email is stored in the Name property

            // Query the database to retrieve orders with status "Order Placed" for the logged-in user
            var orders = db.CompletedOrders
                .Where(order => order.OrderStatus == "Order Completed" && order.Email == userEmail)
                .ToList();

            // Your code to retrieve additional data
            var cart = db.Cart.Where(c => c.Email == userEmail).ToList();
            var cartBuilder = db.CartBuilder.Where(cb => cb.Email == userEmail).ToList();

            var viewModel = new Merge
            {
                Cart = cart,
                CartBuilder = cartBuilder,
                CompletedOrders = orders // Use the filtered "orders" list here

            };

            return View(viewModel);
        }

        public ActionResult Checkout(int productId, int cartId, string itemName, int quantity, int price, int subtotal, string userId)
        {
            string firstName = GetUserFirstName(userId);
            string lastName = GetUserLastName(userId);
            string email = GetUserEmail(userId);
            string contactNumber = GetUserContactNumber(userId);
            string address = GetUserAddress(userId);
            var shopaccounts = db.ShopAccounts.ToList();


            var model = new _1FinalCapstone.Models.Merge
            {
                ProductId = productId,
                CartId = cartId,
                OrderName = itemName,
                OrderQuantity = quantity,
                OrderPrice = price,
                TotalPrice = subtotal,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Address = address,
                ContactNumber = contactNumber,
                ShopAccounts = shopaccounts,

            };

            return View(model);
        }


        public ActionResult Checkout2(string selectedCartIds)
        {
            // Check if selectedCartIds is null or empty
            if (string.IsNullOrEmpty(selectedCartIds))
            {
                // Handle the case where selectedCartIds is null or empty
                // You can return an error view or perform other actions as needed
                return RedirectToAction("Error");
            }

            // Split the selectedCartIds string into an array
            string[] cartIdsArray = selectedCartIds.Split(',');

            // Check if cartIdsArray is null or empty
            if (cartIdsArray == null || cartIdsArray.Length == 0)
            {
                // Handle the case where cartIdsArray is null or empty
                // You can return an error view or perform other actions as needed
                return RedirectToAction("Error");
            }

            // Fetch the data for the selected cart items based on cartIdsArray
            // Implement logic to retrieve the actual cart item data from your data source
            var selectedCartItems = db.Cart.Where(item => cartIdsArray.Contains(item.CartId.ToString()))
         .Select(cart => new CheckoutViewModel
         {
             CartId = cart.CartId,
             ProductId = cart.ProductId,
             Email = cart.Email,
             OrderName = cart.ItemName,
             OrderQuantity = cart.Quantity,
             OrderPrice = cart.ItemPrice,  // Price per item
             TotalPrice = cart.Quantity * cart.ItemPrice,  // Total price for the item quantity
             // Map other properties as needed
         })
         .ToList();


            // Check if selectedCartItems is null or empty
            if (selectedCartItems == null || !selectedCartItems.Any())
            {
                // Handle the case where selectedCartItems is null or empty
                // You can return an error view or perform other actions as needed
                return RedirectToAction("Error");
            }

            // Retrieve user information based on the first selected cart item's UserId
            int? firstCartItemUserId = selectedCartItems.FirstOrDefault()?.UserId;

            string firstName = "";
            string lastName = "";
            string address = "";
            string contactNumber = "";

            // Check if firstCartItemUserId has a value and retrieve user information
            if (firstCartItemUserId.HasValue)
            {
                firstName = GetUserFirstName(firstCartItemUserId.Value.ToString());
                lastName = GetUserLastName(firstCartItemUserId.Value.ToString());
                address = GetUserAddress(firstCartItemUserId.Value.ToString());
                contactNumber = GetUserContactNumber(firstCartItemUserId.Value.ToString());
            }

            // Retrieve ShopAccount data from your data source
            var shopaccounts = db.ShopAccounts.ToList();

            // Create a new CheckoutViewModel
            var model = new CheckoutViewModel
            {
                SelectedCartItems = selectedCartItems,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                ContactNumber = contactNumber,
                ShopAccounts = shopaccounts
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Checkout2(CheckoutViewModel model, int[] CartId, int[] ProductId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fullAddress = model.Address;
                    if (!string.IsNullOrEmpty(model.AdditionalNote))
                    {
                        fullAddress += " (" + model.AdditionalNote + ")";
                    }

                    // Create a new Checkout object from the data in the ViewModel
                    var checkoutData = new CheckoutTable
                    {
                        // Map the properties from the ViewModel to the Checkout model
                        // You should map these according to your database schema
                        CheckoutId = model.CheckoutId,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = fullAddress,
                        ContactNumber = model.ContactNumber,
                        OrderName = model.OrderName,
                        OrderQuantity = model.OrderQuantity,
                        OrderPrice = model.OrderPrice,
                        TotalPrice = model.TotalPrice,
                        PaymentMode = model.PaymentMode,
                        CustomerAccountNumber = model.CustomerAccountNumber,
                        OrderStatus = "Order Placed",
                        CheckoutDate = DateTime.Now
                        // Map other properties as needed
                    };

                    // Insert the checkout data into the database
                    db.CheckoutTable.Add(checkoutData);
                    db.SaveChanges();

                    // Now, you can loop through the CartId array and delete items from the CartTable
                    if (CartId != null && CartId.Length >= 0)
                    {
                        foreach (int cartId in CartId)
                        {
                            // Delete the item with the specified CartId from the CartTable
                            var cartItem = db.Cart.FirstOrDefault(item => item.CartId == cartId);
                            if (cartItem != null)
                            {
                                db.Cart.Remove(cartItem);

                                if (model.SelectedCartItems != null || model.SelectedCartItems == null)
                                {
                                    // Now, let's try to subtract OrderQuantity from ItemQuantity for the corresponding product
                                    var product = db.Product.FirstOrDefault(p => p.ProductId == cartItem.ProductId);
                                    if (product != null || product == null)
                                    {
                                        // Use model.OrderQuantity instead of cartItem.OrderQuantity
                                        product.ItemQuantity -= cartItem.Quantity;
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        // Handle the case where the product doesn't exist
                                        ModelState.AddModelError(string.Empty, "Product not found in the database.");
                                        return View(model);
                                    }
                                }
                            }
                        }

                        // Save changes after processing all items
                        db.SaveChanges();
                    }

                    // Send an order placed email
                    string senderEmail = ConfigurationManager.AppSettings["Email"];
                    string senderPassword = ConfigurationManager.AppSettings["Password"];
                    string emailSubject = "Your Order Has Been Placed!";
                    string emailBody = GenerateOrderPlacedEmailBody(checkoutData); // Implement this method

                    SendOrderPlacedEmail(checkoutData.Email, emailSubject, emailBody, senderEmail, senderPassword);

                    // Redirect to a success page or perform other actions as needed
                    return RedirectToAction("CustomerPlacedOrder");
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the checkout process
                    ModelState.AddModelError(string.Empty, "An error occurred during the checkout process.");
                }
            }

            // If ModelState is not valid or an error occurred, return to the form for correction
            return View(model);
        }





        private int CalculateTotalAmount(IEnumerable<CartItem> cartItems)
        {
            // Calculate the total amount based on the selected cart items
            int totalAmount = cartItems.Sum(item => item.Subtotal.GetValueOrDefault()); // Use GetValueOrDefault() to get the integer value or 0 if null

            return totalAmount;
        }


       

        public ActionResult Checkout3(int builderId, string stepTitle, string builderTitle1, string ordername, int orderprice, string userId)
        {
            // Retrieve user's first name, last name, email, contact number, and address based on the user's ID
            string firstName = GetUserFirstName(userId); // Replace this with your actual method to fetch user's first name
            string lastName = GetUserLastName(userId); // Replace this with your actual method to fetch user's last name
            string email = GetUserEmail(userId); // Replace with your logic to fetch email
            string contactNumber = GetUserContactNumber(userId); // Replace with your logic to fetch contact number
            string address = GetUserAddress(userId); // Replace with your logic to fetch address

            // Fetch the shop accounts data, assuming that db.ShopAccounts is a valid collection
            var shopAccounts = db.ShopAccounts.ToList();

            // Create an instance of CheckoutViewModel and populate its properties
            var viewModel = new CheckoutViewModel
            {
                // Other properties
                BuilderId = builderId,
                StepTitle = stepTitle,
                BuilderTitle1 = builderTitle1,
                OrderName = ordername,
                OrderPrice = orderprice,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                ContactNumber = contactNumber,
                Address = address,
                ShopAccounts = shopAccounts // Set the ShopAccounts property
            };

            // Return the Checkout3 view with the viewModel
            return View("Checkout3", viewModel);
        }

        [HttpPost]
        public ActionResult ProcessCheckout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fullAddress = model.Address;
                    if (!string.IsNullOrEmpty(model.AdditionalNote))
                    {
                        fullAddress += " (" + model.AdditionalNote + ")";
                    }
                    // Create a new Checkout object and populate its properties
                    var checkout = new CheckoutTable
                    {
                        OrderName = model.OrderName,
                        OrderQuantity = 1, // Assuming a quantity of 1 for simplicity
                        OrderPrice = model.OrderPrice,
                        TotalPrice = model.OrderPrice,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Address = fullAddress,
                        ContactNumber = model.ContactNumber,
                        PaymentMode = model.PaymentMode,
                        CustomerAccountNumber = model.CustomerAccountNumber,
                        OrderStatus = "Order Placed",
                        CheckoutDate = DateTime.Now
                    };

                    // Add the checkout object to the database and save changes
                    db.CheckoutTable.Add(checkout);
                    db.SaveChanges();

                    // Send an order placed email
                    string senderEmail = ConfigurationManager.AppSettings["Email"];
                    string senderPassword = ConfigurationManager.AppSettings["Password"];
                    string emailSubject = "Your Order Has Been Placed!";
                    string emailBody = GenerateOrderPlacedEmailBody(checkout); // Implement this method

                    SendOrderPlacedEmail(checkout.Email, emailSubject, emailBody, senderEmail, senderPassword);

                    // Redirect to a thank-you page or display a success message
                    return RedirectToAction("CustomerPlacedOrder");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                }
            }

            // If ModelState is not valid or an exception occurred, return to the form with validation errors
            return View("BuilderMain", model);
        }






















        // Replace this method with your actual logic to fetch user's first name from the ApplicationUser list
        private string GetUserFirstName(string userId)
        {
            // Fetch the user's first name based on userId from the ApplicationUser list and return it
            // Example:
            // var user = YourUserService.GetUserById(userId);
            // return user.FirstName;
            return User.Identity.GetFirstName(); // Replace with actual user's first name
        }

        private string GetUserLastName(string userId1)
        {
            // Fetch the user's first name based on userId from the ApplicationUser list and return it
            // Example:
            // var user = YourUserService.GetUserById(userId);
            // return user.FirstName;
            return User.Identity.GetLastName(); // Replace with actual user's first name
        }

        private string GetUserEmail(string userId1)
        {
            // Fetch the user's first name based on userId from the ApplicationUser list and return it
            // Example:
            // var user = YourUserService.GetUserById(userId);
            // return user.FirstName;
            return User.Identity.GetEmail(); // Replace with actual user's first name
        }

        private string GetUserContactNumber(string userId1)
        {
            // Fetch the user's first name based on userId from the ApplicationUser list and return it
            // Example:
            // var user = YourUserService.GetUserById(userId);
            // return user.FirstName;
            return User.Identity.GetContactNumber(); // Replace with actual user's first name
        }

        private string GetUserAddress(string userId1)
        {
            // Fetch the user's first name based on userId from the ApplicationUser list and return it
            // Example:
            // var user = YourUserService.GetUserById(userId);
            // return user.FirstName;
            return User.Identity.GetAddress(); // Replace with actual user's first name
        }
        // Add this method for sending an order placed email
        private void SendOrderPlacedEmail(string toEmail, string subject, string body, string senderEmail, string senderPassword)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail);
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true; // Set this to true if you're using HTML in the email body

                smtpClient.Send(mailMessage);
            }
        }

        




        // Implement this method to generate the email body for an order placed email
        private string GenerateOrderPlacedEmailBody(CheckoutTable order)
        {
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine($"<p>Dear {order.FirstName},</p>");
            emailBody.AppendLine("<p>We're excited to let you know that your order has been successfully placed! 🛍️</p>");
            emailBody.AppendLine("<p>Here are the details of your order:</p>");
            emailBody.AppendLine($"<ul>");
            emailBody.AppendLine($"<li>Order Name: {order.OrderName}</li>");
            emailBody.AppendLine($"<li>Order Quantity: {order.OrderQuantity}</li>");
            emailBody.AppendLine($"<li>Total Price: {order.TotalPrice}</li>");
            emailBody.AppendLine($"<li>Payment Mode: {order.PaymentMode}</li>");
            emailBody.AppendLine($"<li>Customer Account Number: {order.CustomerAccountNumber}</li>");
            emailBody.AppendLine("</ul>");
            emailBody.AppendLine("<p>Thank you for choosing us for your shopping needs. We'll be working diligently to fulfill your order and deliver it to you as soon as possible.</p>");
            emailBody.AppendLine("<p>If you have any questions or need assistance, feel free to contact our support team.</p>");
            emailBody.AppendLine("<p>Best regards,<br>The M.A.M Team</p>");

            return emailBody.ToString();
        }


        public ActionResult ConfirmationPage(Merge orderDetails)
        {
            return View(orderDetails);
        }

        [Authorize(Roles = "Admin,Owner")]

        public ActionResult ListofOrders(int page = 1, int pageSize = 5)
        {
            IQueryable<CheckoutTable> query = db.CheckoutTable
                .Where(order => order.OrderStatus == "Order Placed")
                .OrderBy(order => order.CheckoutId); // Change OrderId to the appropriate property for sorting

            var paginatedOrders = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalItems = query.Count();

            var model = new PaginatedList<CheckoutTable>(paginatedOrders, page, pageSize, totalItems);

            return View(model);
        }
        [Authorize(Roles = "Admin,Owner")]

        public ActionResult AcceptedOrders(int page = 1, int pageSize = 5)
        {
            IQueryable<CheckoutTable> query = db.CheckoutTable
                .Where(order => order.OrderStatus == "Order Accepted")
                .OrderBy(order => order.CheckoutId); // Change OrderId to the appropriate property for sorting

            var paginatedOrders = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalItems = query.Count();

            var model = new PaginatedList<CheckoutTable>(paginatedOrders, page, pageSize, totalItems);

            return View(model);
        }



        public ActionResult AcceptOrder(int checkoutId)
        {
            var order = db.CheckoutTable.FirstOrDefault(o => o.CheckoutId == checkoutId);
            if (order != null)
            {
                order.OrderStatus = "Order Accepted";
                order.AcceptedDate = DateTime.Now; // Update the OrderDate to the current date and time
                db.SaveChanges();

                string senderEmail = ConfigurationManager.AppSettings["Email"];
                string senderPassword = ConfigurationManager.AppSettings["Password"];

                // Send an email notification
                string emailSubject = "Your Order Has Been Accepted!";
                string emailBody = GenerateAcceptanceEmailBody(order);

                SendEmailNotification(order.Email, emailSubject, emailBody, senderEmail, senderPassword);
            }

            return RedirectToAction("ListofOrders", "Checkout"); // Redirect back to the list of orders
        }
        [HttpPost]
        public ActionResult AcceptSelectedOrders(List<int> checkoutIds)
        {
            foreach (var checkoutId in checkoutIds)
            {
                var order = db.CheckoutTable.Find(checkoutId);
                if (order != null)
                {
                    order.OrderStatus = "Order Accepted";
                    order.AcceptedDate = DateTime.Now;

                    // Get the customer's email address
                    string recipientEmail = order.Email;

                    // Send an email notification to the customer
                    string senderEmail = ConfigurationManager.AppSettings["Email"];
                    string senderPassword = ConfigurationManager.AppSettings["Password"];
                    string emailSubject = "Your Order Has Been Accepted!";
                    string emailBody = GenerateAcceptanceEmailBody(order);

                    SendEmailNotification(recipientEmail, emailSubject, emailBody, senderEmail, senderPassword);
                }
            }

            db.SaveChanges(); // Save the changes to the database

            // Return a response to the client (e.g., success message)
            return Json(new { message = "Selected orders have been accepted." });
        }


        private string GenerateAcceptanceEmailBody(CheckoutTable order)
        {
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine($"Dear {order.FirstName},");
            emailBody.AppendLine();
            emailBody.AppendLine("We're thrilled to inform you that your order has been accepted and is now in progress! 🎉");
            emailBody.AppendLine();
            emailBody.AppendLine("Your excitement for our products is truly appreciated, and we can't wait to bring your selected items to life. Our dedicated team is already working diligently to ensure that your order is handled with the utmost care and attention.");
            emailBody.AppendLine();
            emailBody.AppendLine("Here are the details of your order:");
            emailBody.AppendLine($"- Order Name: {order.OrderName}");
            emailBody.AppendLine($"- Order Quantity: {order.OrderQuantity}");
            emailBody.AppendLine($"- Total Price: {order.TotalPrice}");
            emailBody.AppendLine($"- Payment Mode: {order.PaymentMode}");
            emailBody.AppendLine($"- Customer Account Number: {order.CustomerAccountNumber}");
            emailBody.AppendLine();
            emailBody.AppendLine("You can expect the delivery of your items within the estimated timeframe. We'll keep you updated every step of the way, so you're always in the loop.");
            emailBody.AppendLine();
            emailBody.AppendLine("If you have any questions or special requests, please don't hesitate to get in touch with our support team. We're here to make sure your shopping experience is exceptional.");
            emailBody.AppendLine();
            emailBody.AppendLine("Thank you for choosing us for your shopping needs. Your satisfaction is our top priority, and we can't wait to delight you with your order.");
            emailBody.AppendLine();
            emailBody.AppendLine("Best regards,");
            emailBody.AppendLine("The M.A.M Team");

            return emailBody.ToString();
        }

        private void SendEmailNotification(string toEmail, string subject, string body, string senderEmail, string senderPassword)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail);
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                smtpClient.Send(mailMessage);
            }
        }


        [HttpPost]
        public ActionResult CompleteOrder(int checkoutId)
        {
            // Retrieve the order by checkoutId from the AcceptedOrders table
            var acceptedOrder = db.CheckoutTable.Find(checkoutId);

            if (acceptedOrder != null)
            {
                // Create a new CompletedOrders object and populate it with the data from the accepted order
                var completedOrder = new CompletedOrders
                {
                    OrderName = acceptedOrder.OrderName,
                    OrderQuantity = acceptedOrder.OrderQuantity,
                    OrderPrice = acceptedOrder.OrderPrice,
                    TotalPrice = acceptedOrder.TotalPrice,
                    FirstName = acceptedOrder.FirstName,
                    LastName = acceptedOrder.LastName,
                    Email = acceptedOrder.Email,
                    Address = acceptedOrder.Address,
                    ContactNumber = acceptedOrder.ContactNumber,
                    PaymentMode = acceptedOrder.PaymentMode,
                    CustomerAccountNumber = acceptedOrder.CustomerAccountNumber,
                    OrderStatus = "Order Completed", // Set the status to "Completed" or any other appropriate status
                    CheckoutDate = acceptedOrder.CheckoutDate,
                    AcceptedDate = acceptedOrder.AcceptedDate,
                    CompletedDate = DateTime.Now // Set the completion date to the current date and time
                };

                // Add the completed order to the CompletedOrders table
                db.CompletedOrders.Add(completedOrder);

                // Remove the accepted order from the AcceptedOrders table
                db.CheckoutTable.Remove(acceptedOrder);

                // Save changes to the database
                db.SaveChanges();
            }

            // Redirect to the list of completed orders or any other appropriate page
            return RedirectToAction("CustomerHistory");
        }

        // Your other controller actions and methods go here...

        [Authorize(Roles = "Admin,Owner")]

        public ActionResult CancelledOrders()
        {
            return View(db.CancelledOrders.ToList());
        }

        public ActionResult CancelSelectedOrders(List<int> orderIds)
        {
            foreach (int orderId in orderIds)
            {
                var order = db.CheckoutTable.Find(orderId); // Assuming "db" is your database context

                if (order != null)
                {
                    // Create a CancelledOrder object and populate it with the order data
                    var cancelledOrder = new CancelledOrders
                    {
                        OrderName = order.OrderName,
                        OrderQuantity = order.OrderQuantity,
                        OrderPrice = order.OrderPrice,
                        TotalPrice = order.TotalPrice,
                        FirstName = order.FirstName,
                        LastName = order.LastName,
                        Email = order.Email,
                        ContactNumber = order.ContactNumber,
                        PaymentMode = order.PaymentMode,
                        CustomerAccountNumber = order.CustomerAccountNumber
                    };

                    // Add the cancelled order to the CancelledOrders table
                    db.CancelledOrders.Add(cancelledOrder);

                    // Remove the selected order from the CheckoutTable
                    db.CheckoutTable.Remove(order);

                    // Send an email notification for order cancellation
                    string senderEmail = ConfigurationManager.AppSettings["Email"];
                    string senderPassword = ConfigurationManager.AppSettings["Password"];
                    string emailSubject = "Your Order Has Been Canceled";
                    string emailBody = GenerateCancellationEmailBody(order);

                    SendEmailNotification(order.Email, emailSubject, emailBody, senderEmail, senderPassword);
                }
            }

            // Save changes to the database
            db.SaveChanges();

            // Return a response indicating success or failure
            return Json(new { message = "Selected orders have been canceled." });
        }

        private string GenerateCancellationEmailBody(CheckoutTable order)
        {
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine($"Dear {order.FirstName},");
            emailBody.AppendLine();
            emailBody.AppendLine("We regret to inform you that your order has been canceled.");
            emailBody.AppendLine("If you have any questions or concerns regarding this cancellation, please don't hesitate to contact our support team.");
            emailBody.AppendLine();
            emailBody.AppendLine("Here are the details of the canceled order:");
            emailBody.AppendLine($"- Order Name: {order.OrderName}");
            emailBody.AppendLine($"- Order Quantity: {order.OrderQuantity}");
            emailBody.AppendLine($"- Total Price: {order.TotalPrice}");
            emailBody.AppendLine($"- Payment Mode: {order.PaymentMode}");
            emailBody.AppendLine($"- Customer Account Number: {order.CustomerAccountNumber}");
            emailBody.AppendLine();
            emailBody.AppendLine("We apologize for any inconvenience this may have caused.");
            emailBody.AppendLine();
            emailBody.AppendLine("Best regards,");
            emailBody.AppendLine("The M.A.M Team");

            return emailBody.ToString();
        }




    }
}