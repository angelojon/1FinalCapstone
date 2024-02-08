using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1FinalCapstone.Models
{
    public class DashboardViewModel
    {
        public List<_1FinalCapstone.Models.AspNetUsers> Users { get; set; }
        public List<_1FinalCapstone.Models.Product> Products { get; set; }
        public List<_1FinalCapstone.Models.BikeBuilder> Builder { get; set; }
        public int TotalProductsQuantity { get; set; }
        public List<_1FinalCapstone.Models.AspNetUsers> NewCustomer { get; set; }
        public List<_1FinalCapstone.Models.CheckoutTable> CheckoutRecords { get; set; }
        public List<_1FinalCapstone.Models.CompletedOrders> CompletedOrders { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OrderName { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalPrice { get; internal set; }
        public decimal TotalInvestment { get; internal set; }
        public decimal TotalIncome { get; internal set; }
    }
}