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
    public class ShopAccountsController : Controller
    {
        private Entities db = new Entities();

        // GET: ShopAccounts
        [Authorize(Roles = "Admin,Owner")]

        public ActionResult Index()
        {
            return View(db.ShopAccounts.ToList());
        }
        [Authorize(Roles = "Admin,Owner")]

        // GET: ShopAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopAccounts shopAccounts = db.ShopAccounts.Find(id);
            if (shopAccounts == null)
            {
                return HttpNotFound();
            }
            return View(shopAccounts);
        }

        // POST: ShopAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountId,GcashAccountName,GcashAccountNumber,PaymayaAccountName,PaymayaAccountNumber")] ShopAccounts shopAccounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopAccounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shopAccounts);
        }
    }
}
