using _1FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1FinalCapstone.Controllers
{
    public class AuditLogsController : Controller
    {
        private Entities db = new Entities();

        [Authorize(Roles = "Admin,Owner")]

        public ActionResult AuditLogs()
        {
            return View(db.AuditLogs.ToList());
        }
    }
}