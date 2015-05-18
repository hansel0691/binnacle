using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ElectronicBinnacle.Models.Context;
using ElectronicBinnacle.Models.Models.SamplingOrder;
using WebMatrix.WebData;

namespace ElectronicBinnacle.Controllers
{
    public class PackagesController : Controller
    {
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Package")]
        public JsonResult GetPackage(int id = 0, string searchName = "", int page = 0)
        {
            using (var context = new MyContext())
            {
                if (id == 0)
                {
                    var packs = context.AllPackages(new Package{Identifier = searchName}).Where(p => p.Standard).OrderBy(p => p.PackageId).ToList();
                    if (page != 0)
                        return Json(new { packs = packs.Skip((page - 1) * 20).Take(20).ToList(), count = Math.Ceiling(packs.Count() / 20.0) }, JsonRequestBehavior.AllowGet);
                    return Json(packs, JsonRequestBehavior.AllowGet);
                }
                return Json(context.GetPackage(id), JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Package")]
        public JsonResult SavePackage([FromBody]Package package)
        {
            package.Standard = true;
            using (var context = new MyContext())
            {
                if (package.PackageId == 0)
                    return context.AddPackage(package) == null ? Json(new { success = false }) : Json(new { success = true });
                return !context.UpdatePackage(package) ? Json(new { success = false }) : Json(new { success = true });
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("RemovePackage")]
        public JsonResult DeletePackage(int id, int page = 1)
        {
            using (var context = new MyContext())
            {
                if (context.AllOrders().Any(o => o.WorkPackages.Any(w => w.Packages.Any(p => p.PackageId == id))))//**
                    return Json(new { success = true, notPass = true, error = "Existe una órden de trabajo que contiene a este grupo." }, JsonRequestBehavior.AllowGet);

                var packages = context.Packages.Where(p => p.Standard).OrderBy(p => p.PackageId).ToList();
                var next = packages.Skip(page * 20).Take(1).FirstOrDefault();
                var last = next != null && next.PackageId == packages.Last().PackageId;
                return !context.RemovePackage(id) ? Json(new { success = false }, JsonRequestBehavior.AllowGet) : Json(new { success = true, pack = next, last = last }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Parameter")]
        public JsonResult GetParameter(int id = 0, int page = 0, string searchName = "", string container = "", string preserver = "", int volume = -1, int tmpa = -1)
        {
            using (var context = new MyContext())
            {
                if (id == 0)
                {
                    var parameters = context.AllParameters(new Parameter(){Identifier = searchName, Container = container, Preserver =  preserver, Volume = volume, TMPA = tmpa}).OrderBy(p => p.ParameterId).ToList();
                    return page != 0 
                        ? Json( new { parameters = parameters.Skip((page - 1)*20).Take(20).ToList(), count = Math.Ceiling(parameters.Count()/20.0) }, JsonRequestBehavior.AllowGet) 
                        : Json(parameters.ToList(), JsonRequestBehavior.AllowGet);
                }
                return Json(context.GetParam(id), JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Parameter")]
        public JsonResult SaveParameter([FromBody] Parameter parameter)
        {
            using (var context = new MyContext())
            {
                if (parameter.ParameterId == 0)
                {
                    parameter.Creator = WebSecurity.CurrentUserId;
                    var saved = context.AddParam(parameter);
                    return saved == null ? Json(new { success = false }, JsonRequestBehavior.AllowGet) : Json(new { success = true, id = saved.ParameterId, overflow = context.Params.Count() % 20 == 1 }, JsonRequestBehavior.AllowGet);
                }
                return !context.UpdateParam(parameter) ? Json(new { success = false }, JsonRequestBehavior.AllowGet) : Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("RemoveParameter")]
        public JsonResult DeleteParameter(int id, int page = 1)
        {
            using (var context = new MyContext())
            {
                if (context.AllPackages().Any(p => p.Parameters.Any(pa => pa.ParameterId == id)))
                    return Json(new { success = true, notPass = true, error = "Este parámetro está actualmente en uso." }, JsonRequestBehavior.AllowGet);
                var parameter = context.Params.FirstOrDefault(p => p.ParameterId == id);
                if (parameter == null) return null;
                if (parameter.Creator != WebSecurity.CurrentUserId)
                    return Json(new { success = true, notPass = true, error = "Usted no es el creador de este parametro." }, JsonRequestBehavior.AllowGet);

                var parameters = context.Params.OrderBy(p => p.ParameterId).ToList();
                var next = parameters.Skip(page * 20).Take(1).FirstOrDefault();
                var last = next != null && next.ParameterId == parameters.Last().ParameterId;
                return !context.RemoveParam(id) ? Json(new { success = false }) : Json(new { success = true, param = next, last = last });
            }
        }
    }
}
