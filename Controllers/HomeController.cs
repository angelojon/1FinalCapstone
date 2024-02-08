using _1FinalCapstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace _1FinalCapstone.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Products(string category = "", string sort = "", string search = "")
        {
            string userEmail = User.Identity.Name;
            var cart = db.Cart.Where(c => c.Email == userEmail).ToList();
            var cartBuilder = db.CartBuilder.Where(cb => cb.Email == userEmail).ToList();

            // Filter products based on the selected category
            var products = db.Product.ToList();

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.ComponentType == category).ToList();
            }

            // Apply price sorting
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "asc")
                {
                    products = products.OrderBy(p => p.ItemPrice).ToList();
                }
                else if (sort == "desc")
                {
                    products = products.OrderByDescending(p => p.ItemPrice).ToList();
                }
            }

            // Filter products based on the search query
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.ItemName.Contains(search)).ToList();
            }

            // Filter out products with "ArchiveStatus" equal to "Archive"
            products = products.Where(p => p.ArchiveStatus != "Archive").ToList();

            foreach (var product in products)
            {
                if (product.Image != null)
                    product.ImageProd = Convert.ToBase64String(product.Image);
            }

            var viewModel = new Merge
            {
                Cart = cart,
                CartBuilder = cartBuilder,
                Products = products  // Make sure you have a property named 'Products' in the 'Merge' class.
            };

            return View(viewModel);
        }





        // GET: BikeDetails
        Entities _context = new Entities();
        public ActionResult ProductsController()
        {
            var listofbikes = _context.Product.ToList();
            return View(listofbikes);
        }

        // GET: BikeDetails


        public ActionResult Index()
        {
            string userEmail = User.Identity.Name;
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

        public ActionResult About()
        {
            string userEmail = User.Identity.Name;
            var cart = db.Cart.Where(c => c.Email == userEmail).ToList();
            var cartBuilder = db.CartBuilder.Where(cb => cb.Email == userEmail).ToList();
            var products = db.Product.ToList();

            var viewModel = new Merge
            {
                Cart = cart,
                CartBuilder = cartBuilder,

            };

            return View(viewModel);
        }

        public ActionResult Contact()
        {
            string userEmail = User.Identity.Name;
            var cart = db.Cart.Where(c => c.Email == userEmail).ToList();
            var cartBuilder = db.CartBuilder.Where(cb => cb.Email == userEmail).ToList();
            var products = db.Product.ToList();

            var viewModel = new Merge
            {
                Cart = cart,
                CartBuilder = cartBuilder,

            };
            return View(viewModel);
        }
        public ActionResult Bikes()
        {

            string userEmail = User.Identity.Name;
            var cart = db.Cart.Where(c => c.Email == userEmail).ToList();
            var cartBuilder = db.CartBuilder.Where(cb => cb.Email == userEmail).ToList();
            var products = db.Product.ToList();

            var viewModel = new Merge
            {
                Cart = cart,
                CartBuilder = cartBuilder,

            };
            return View(viewModel);
        }
        public ActionResult Message()
        {

            return View();
        }


        [Authorize(Roles = "Admin,Owner")]

        public ActionResult AdminHome()
        {
            var users = _context.AspNetUsers.ToList();
            var products = _context.Product.ToList();
            var bikebuilder = _context.BikeBuilder.ToList();
            var totalQuantity = products.Sum(b => b.ItemQuantity);
            var currentDate = DateTime.Now;
            var startDate = currentDate.AddDays(-7);
            var newCustomers = _context.AspNetUsers.Where(u => u.CreateDate >= startDate && u.CreateDate <= currentDate).Take(3).ToList();
            var checkoutRecords = _context.CheckoutTable.ToList();
            var completeOrders = _context.CompletedOrders.ToList();
            var totalInvestment = _context.Product.ToList();

            decimal totalInvest = _context.BikeBuilder.Where(builder => builder.BuilderId >= 1 && builder.BuilderId <= 94).Sum(builder => builder.Bprice1) + (decimal)products.Sum(product => product.ItemQuantity * product.ItemCost);
            decimal totalRevenue = (decimal)completeOrders.Sum(record => record.TotalPrice);
            decimal totalIncome = Math.Max(totalRevenue - totalInvest,0);



            var viewModel = new DashboardViewModel
            {
                Users = users,
                Products = products,
                Builder = bikebuilder,
                TotalProductsQuantity = (int)totalQuantity,
                NewCustomer = newCustomers,
                CheckoutRecords = checkoutRecords,
                TotalPrice = totalRevenue,
                TotalInvestment = totalInvest,
                TotalIncome = totalIncome
            };

            var viewModelList = new List<DashboardViewModel>();
            viewModelList.Add(viewModel);
            return View(viewModelList);
        }

        [HttpPost]
        public JsonResult AddToCart(int productId)
        {
            Product product = db.Product.Find(productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            // Get the current user's email using ASP.NET Identity
            string userEmail = User.Identity.GetUserName();
            if (string.IsNullOrEmpty(userEmail))
            {
                return Json(new { success = false, message = "Please Create an Account or Login first to proceed to Add to cart" });
            }

            // Check if the cart already contains an item with the same ProductId for the current user
            Cart existingCartItem = db.Cart.FirstOrDefault(c => c.Email == userEmail && c.ProductId == productId);

            if (existingCartItem != null)
            {
                // If the item exists, update the quantity and subtotal
                existingCartItem.Quantity += 1;
                existingCartItem.Subtotal = existingCartItem.Quantity * existingCartItem.ItemPrice;
                db.Entry(existingCartItem).State = EntityState.Modified;


            }
            else
            {
                // If the item doesn't exist in the cart, add a new cart item
                db.Cart.Add(new Cart
                {
                    Email = userEmail,
                    ProductId = productId,
                    ItemName = product.ItemName,
                    ItemPrice = product.ItemPrice,
                    Quantity = 1,
                    Subtotal = product.ItemPrice, // Set the initial subtotal to the price of the product
                    CartImage = product.Image
                });
            }

            db.SaveChanges();

            // Calculate the updated cart count for the user and return it
            int cartCount = db.Cart.Count(c => c.Email == userEmail);
            return Json(new { success = true, cartCount = cartCount });
        }


        public async Task<ActionResult> Cart()
        {
            string userEmail = User.Identity.GetUserName();
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Account");
            }

            List<Cart> cartItems = await db.Cart.Where(c => c.Email == userEmail).ToListAsync();

            List<CartItem> cartItemList = new List<CartItem>();

      foreach (var item in cartItems)
{
    var product = db.Product.FirstOrDefault(p => p.ProductId == item.ProductId);
    if (product != null)
    {
        var cartItem = new CartItem
        {
            CartId = item.CartId,
            Email = item.Email,
            ProductId = item.ProductId,
            ItemName = item.ItemName,
            Quantity = item.Quantity,
            ItemPrice = item.ItemPrice,
            Subtotal = item.Quantity * item.ItemPrice,
            CartImage = new ProductsImage
            {
                ImageProd = item.CartImage != null ? Convert.ToBase64String(item.CartImage) : string.Empty
            },
            // Fetch ItemQuantity based on ProductId
            // Inside your loop in the controller action
            ItemQuantity = product.ItemQuantity

        };
        cartItemList.Add(cartItem);
    }
}


            return View(cartItemList);
        }


        [HttpPost]
        public ActionResult UpdateCartItemQuantity(int cartItemId, int productId, int change)
        {
            Cart cartItemToUpdate = db.Cart.Find(cartItemId);

            if (cartItemToUpdate != null)
            {
                // Fetch the current stock of the product
                Product product = db.Product.Find(productId);

                if (product != null)
                {
                    // Calculate the new quantity
                    int newQuantity = (cartItemToUpdate.Quantity ?? 0) + change;

                    // Ensure the updated quantity is within a valid range
                    if (newQuantity >= 1 && newQuantity <= product.ItemQuantity)
                    {
                        // Update the cart item quantity and subtotal
                        cartItemToUpdate.Quantity = newQuantity;
                        cartItemToUpdate.Subtotal = cartItemToUpdate.ItemPrice * newQuantity;
                        db.Entry(cartItemToUpdate).State = EntityState.Modified;
                        db.SaveChanges();

                        // Return the updated quantity in the response
                        return Json(new { success = true, newQuantity = newQuantity });
                    }
                }
            }

            // Return an appropriate HTTP status code (e.g., 404 Not Found) for errors
            return Json(new { success = false, message = "Failed to update quantity." });
        }








        [HttpPost]
        public ActionResult DeleteCartItem(int? cartItemId)
        {
            if (cartItemId == null)
            {
                // If the cartItemId is null or not provided, handle the error appropriately
                // You can display an error message or redirect back to the cart page
                return RedirectToAction("CartM", "ShoppingCart");
            }

            Cart cartItemToRemove = db.Cart.Find(cartItemId);

            if (cartItemToRemove != null)
            {
                db.Cart.Remove(cartItemToRemove);
                db.SaveChanges();
            }

            // Redirect back to the cart page after deletion
            return RedirectToAction("CartM", "ShoppingCart");
        }

        public class PaginatedList<T>
        {
            public List<T> Items { get; set; }
            public int PageIndex { get; private set; }
            public int TotalPages { get; private set; }
            public int TotalCount { get; private set; }
            public int PageSize { get; private set; } // Add PageSize property
            public bool HasPreviousPage => PageIndex > 1;
            public bool HasNextPage => PageIndex < TotalPages;

            public PaginatedList(List<T> items, int pageIndex, int pageSize, int totalCount)
            {
                PageIndex = pageIndex;
                PageSize = pageSize; // Set the PageSize property
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                TotalCount = totalCount;
                Items = items;
            }

            public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
            {
                var totalCount = source.Count();
                var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new PaginatedList<T>(items, pageIndex, pageSize, totalCount);
            }
        }


    }
}

