using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectronicBinnacle.Models.Context;
using ElectronicBinnacle.Models.Models.UserControl;
using WebMatrix.WebData;

namespace ElectronicBinnacle.Controllers
{
    [Authorize]
    public class PartialRouteController : Controller
    {
        public ActionResult EditEmployee()
        {
            return View();
        }
        public ActionResult EditOrder()
        {
            return View();
        }
        public ActionResult EditPackage()
        {
            return View();
        }

        public ActionResult Package()
        {
            return View();
        }
        public ActionResult EditRole()
        {
            return View();
        }
        public ActionResult EditUser()
        {
            return View();
        }
        public ActionResult Employees()
        {
            return View();
        }
        public ActionResult EoClient()
        {
            return View();
        }
        public ActionResult EoFinalPhase()
        {
            return View();
        }
        public ActionResult EoPackage()
        {
            return View();
        }
        public ActionResult EoSamplingData()
        {
            return View();
        }
        public ActionResult EoSamplingPlace()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Orders()
        {
            return View();
        }
        public ActionResult Packages()
        {
            return View();
        }
        public ActionResult Params()
        {
            return View();
        }
        public ActionResult Roles()
        {
            return View();
        }
        public ActionResult SamplesView()
        {
            return View();
        }
        public ActionResult SvBinnacle1()
        {
            return View();
        }
        public ActionResult SvBinnacle2()
        {
            return View();
        }
        public ActionResult SvComplexSample()
        {
            return View();
        }
        public ActionResult SvSamplePlan()
        {
            return View();
        }
        public ActionResult SvSimpleSamples()
        {
            return View();
        }
        public ActionResult SvString()
        {
            return View();
        }

        public ActionResult SvEvidence()
        {
            return View();
        }
        [System.Web.Mvc.Authorize]
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult Routes()
        {
            return View();
        }
        public ActionResult Stats()
        {
            return View();
        }
        public ActionResult EmployeeStats()
        {
            return View();
        }
        public ActionResult GeneralMap()
        {
            return View();
        }
    }
}
