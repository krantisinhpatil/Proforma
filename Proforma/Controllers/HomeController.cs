using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;
using System.Data.Entity.Validation;
using System.Net;
using Proforma.ViewModel;
using Newtonsoft.Json;
using System.Data.Entity;
using Proforma.DAL;

namespace Proforma.Controllers
{
    public class HomeController : Controller
    {

        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

      
    }
}
