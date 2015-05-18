using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using ElectronicBinnacle.Models;
using ElectronicBinnacle.Models.Context;
using ElectronicBinnacle.Models.Models.Samples;
using ElectronicBinnacle.Models.Models.SamplingOrder;
using ElectronicBinnacle.Models.Models.UserControl;
using iTextSharp.text;
using WebMatrix.WebData;
using System.Data.Entity;

namespace ElectronicBinnacle.Controllers
{
    public class OrdersController : Controller
    {
        private static Dictionary<int, List<string>> PendingNots { get; set; }


        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Order")]
        public JsonResult AllOrders(int id = 0, string identifier = "", bool sended = true, bool unsended = true, bool evaluated = true, bool unevaluated = true, bool unfinished = true, bool uncomplete = true, 
            string socialReason = "", string place = "", string rfc = "",long startDate = 0, long endDate = 0, int page = 0)
        {
            var userId = WebSecurity.CurrentUserId;
            using (var context = new MyContext())
            {
                var authUser = context.GetOrdersUser(userId, id != 0);
                var breakTemp = false; 
                if (id == 0)
                {
                    var orders = FilterOrders(authUser.AllOrders(ref breakTemp), identifier, sended, unsended, evaluated, unevaluated, unfinished, uncomplete, socialReason, place, rfc, startDate, endDate)
                        .Select(o => o.ObjForJson("Index")).ToList();
                    if (page != 0)
                    {
                        var a = orders.Skip((page - 1)*20).Take(20).ToList();
                        return Json(new { orders = a, count = Math.Ceiling(orders.Count() / 20.0) }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(orders, JsonRequestBehavior.AllowGet);
                }
                
                var firstOrDefault = authUser.AllOrders(ref breakTemp).FirstOrDefault(o => o.Id == id);
                if (firstOrDefault != null)
                {
                    firstOrDefault.WorkPackages = new List<WorkPackage>(context.WorkPackages.Include(wp => wp.Packages.Select(p => p.Parameters)).Where(wp => wp.SamplingOrder.Id == firstOrDefault.Id));
                    return Json(firstOrDefault.ObjForJson("Orders"), JsonRequestBehavior.AllowGet);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        private IEnumerable<SamplingOrder> FilterOrders(IEnumerable<SamplingOrder> allOrders, string identifier, bool sended, bool unsended, bool evaluated, bool unevaluated, bool unfinished, bool uncomplete,
            string socialReason, string place, string rfc, long startDate, long endDate)
        {

            var orders = allOrders;
            if (!string.IsNullOrWhiteSpace(identifier))
                orders = orders.Where(o => o.SamplingData.Identifier.ToLower().Contains(identifier.ToLower()));
            
            if (!sended)
                orders = orders.Where(o => o.OrderState != OrderState.Sended);
            if (!unsended)
                orders = orders.Where(o => o.OrderState != OrderState.NotSended);
            if (!evaluated)
                orders = orders.Where(o => o.OrderState != OrderState.Evaluated);
            if (!unevaluated)
                orders = orders.Where(o => o.OrderState != OrderState.NotEvaluated);
            if (!unfinished)
                orders = orders.Where(o => o.OrderState != OrderState.NotFinished);
            if (!uncomplete)
                orders = orders.Where(o => o.OrderState != OrderState.Unredeemed);

            if (!string.IsNullOrWhiteSpace(socialReason))
                orders = orders.Where(o => o.ClientData.SocialReason.ToLower().Contains(socialReason.ToLower()));
            if (!string.IsNullOrWhiteSpace(place))
                orders = orders.Where(o => o.LocationData.Place.ToLower().Contains(place.ToLower()));

            if (!string.IsNullOrWhiteSpace(rfc))
                orders = orders.Where(o => (o.ClientData.RFC != null && o.ClientData.RFC.ToLower().Contains(rfc.ToLower())) || (o.BillerClient.RFC != null && o.BillerClient.RFC.ToLower().Contains(rfc.ToLower())));

            if (startDate > 0)
                orders = orders.Where(o => o.SamplingData.StartTime >= startDate);
            if (endDate > 0)
                orders = orders.Where(o => o.SamplingData.StartTime <= endDate + 24 * 60 * 60 * 1000);

            return orders;
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Order")]
        public JsonResult Save([FromBody]SamplingOrder order, int relatedCount = 1, int currentCount = 1)
        {
            var usedPacks = new List<WorkPackage>();
            if (order.WorkPackages != null)
            {
                usedPacks.AddRange(order.WorkPackages);
                order.WorkPackages.Clear();
            }
            else
                order.WorkPackages = new List<WorkPackage>();
            using (var context = new MyContext())
            {
                foreach (var usedPack in usedPacks)//**
                {
                    order.WorkPackages.Add(new WorkPackage() { Packages = new List<Package>(), Period = usedPack.Period, SamplesNumber = usedPack.SamplesNumber, Type = usedPack.Type });
                    if (usedPack.Packages != null)
                        foreach (var package in usedPack.Packages)
                        {
                            Package pack = package.PackageId != 0
                                ? context.GetPackage(package.PackageId)
                                : context.AddPackage(new Package() { Identifier = "", Parameters = new List<Parameter>(package.Parameters.Select(p => context.GetParam(p.ParameterId))), Standard = false });
                            order.WorkPackages[order.WorkPackages.Count - 1].Packages.Add(pack);
                        }
                }

                if (order.Sampler != null)
                    order.Sampler = context.GetEmployee(order.Sampler.EmployeeId);
                order.Creator = context.GetUser(order.Creator.UserId);
                if (order.Id == 0)
                {
                    order.SamplingData.Identifier = order.Creator.SamplingIdentifier + PrettyNumberString(order.Creator.CreatedOrders.Count);
                    return context.AddOrder(order) == null ? Json(new { success = false }) : Json(new { success = true });
                }
                return context.UpdateOrder(order) ? Json(new { success = true }) : Json(new { success = false });
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("GenerateExel")]
        public FileResult GenerateExel(int id = 0, int exportTo = 0, int exportFrom = 0)
        {
            //try
            //{
            using (var context = new MyContext())
            {
                var order = context.GetOrder(id);
                if (order == null) return null;

                var generatedPath = "";
                if (exportFrom != -1 && (order.OrderState == OrderState.Evaluated || order.OrderState == OrderState.Unredeemed || order.OrderState == OrderState.NotEvaluated))
                {
                    switch (exportFrom)
                    {
                        case 1:
                            generatedPath = SavePrintUtils.SavePlanMuestreo(context.GetSamplingInfo(id), exportTo == 0);
                            break;
                        case 2:
                            generatedPath = SavePrintUtils.SaveMuestras(context.GetSamplingInfo(id), exportTo == 0);
                            break;
                        case 3:
                            generatedPath = SavePrintUtils.SaveCadena(context.GetSamplingInfo(id), exportTo == 0);
                            break;
                        case 4:
                            generatedPath = SavePrintUtils.SaveBitacora(context.GetSamplingInfo(id), exportTo == 0);
                            break;
                        default:
                            generatedPath = SavePrintUtils.SaveRegistroCampo(context.GetSamplingInfo(id), exportTo == 0);
                            break;
                    }
                }
                else
                    generatedPath = SavePrintUtils.SaveOrdenTrabajo(order, exportTo == 0);
                
                var fileBytes = GetFile(generatedPath);
                string fileName = generatedPath.Substring(generatedPath.LastIndexOf('\\'));
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            
            //}
            //catch (Exception)
            //{
            //    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            //}
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            var data = new byte[fs.Length];
            var br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("SetSamplingState")]
        public JsonResult SetSamplingState(int id, int state)
        {
            try
            {
                using (var context = new MyContext())
                {
                    var order = context.GetOrder(id);
                    order.SamplingState = (SamplingState)state;
                    order.OrderState = OrderState.Evaluated;
                    context.SaveChanges();
                }
                return Json(new {success = true}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Notification")]
        public JsonResult GetNotification(int id = 0, string notificationText = "", int samplerId = 0, int type = 0, long date = 0, int page = 0)
        {
            var boosId = WebSecurity.CurrentUserId;
            using (var context = new MyContext())
            {
                var userBoos = context.Users.Include("Employee.Role").Include("Subordinates").FirstOrDefault(u => u.UserId == boosId);
                if (userBoos == null) return null;
                var subordinatesList = userBoos.AllSubordinates().ToList();
                var notifications = context.GetUserNotifications(boosId, notificationText, samplerId, type, date, id == 0 ? page : 0).ToList();

                foreach (var s in subordinatesList)
                {
                    var loadedS = context.Employees.Include("Role").FirstOrDefault(e => e.EmployeeId == s.EmployeeId);
                    if (loadedS.Role.RoleId != 5)
                    {
                        var otherNots = context.GetUserNotifications(loadedS.EmployeeId, notificationText, samplerId, type, date, id == 0 ? page : 0).ToList();
                        notifications.AddRange(otherNots);
                    }
                }


                if (id == 0)
                    return Json(notifications.ToList(), JsonRequestBehavior.AllowGet);
                return Json(notifications.FirstOrDefault(n => n.NotificationId == id), JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Sample")]
        public JsonResult GetSampleInformation(int id)
        {
            using (var context = new MyContext())
            {
                return Json(context.GetSamplingInfo(id).ObjForJson(), JsonRequestBehavior.AllowGet);
            }
        }
        

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("SamplingOrder")]
        public JsonResult SamplingOrder(int id)
        {
            using (var context = new MyContext())
                return Json(context.GetSampleOrder(id).JsonForSamplingOrder(), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("SamplingPlan")]
        public JsonResult GetSamplingPlan(int id)
        {
            using (var context = new MyContext())
            {
                var a = context.GetSamplingPlan(id).ObjForJson("SamplingPlan", context.SimpleSamplesCount(id), context.ComplexSamplesCount(id));
                return Json(a, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("SamplingString")]
        public JsonResult SamplingString(int id = 0)
        {
            using (var context = new MyContext())
            {
                var a = context.GetString(id).ObjForJson("SamplingString");
                return Json(a, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("SimpleSample")]
        public JsonResult GetSimpleSample(int id, int page)
        {
            if (page < 0) return Json(new {success = false}, JsonRequestBehavior.AllowGet);
            var simpleSamples = new List<SimpleSample>();
            using (var context = new MyContext())
            {
                var result = context.GetSimpleSamples(id);
                if (result != null)
                    simpleSamples.AddRange(result);
            }
            if (page >= simpleSamples.Count)
                return Json(new {success = false}, JsonRequestBehavior.AllowGet);
            return Json(simpleSamples[page].ObjForJson(), JsonRequestBehavior.AllowGet);
        }
        
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("ComplexSample")]
        public JsonResult GetComplexSample(int id, int page)
        {
            if (page < 0) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            var complexSamples = new List<ComplexSample>();
            using (var context = new MyContext())
            {
                var result = context.GetComplexSamples(id);
                if (result != null)
                    complexSamples.AddRange(result);
            }
            if (page >= complexSamples.Count)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            return Json(complexSamples[page].ObjForJson(), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Photos")]
        public JsonResult Photos(int id = 0, int photo = 0)
        {
            using (var context = new MyContext())
            {
                if (photo == 0)
                    return Json(context.Photos.Select(p => p.PhotoId).ToList(), JsonRequestBehavior.AllowGet);
                return Json(context.Photos.FirstOrDefault(p => p.PhotoId == photo && p.Id_OrdenMuestreo == id), JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Croquis")]
        public JsonResult Croquis(int sampleId = 0, int croquisId = 0)
        {
            using (var context = new MyContext())
            {
                var croquis = croquisId != -1 || sampleId == 0 ? context.GetCroquis(sampleId, croquisId) : null;
                return Json(croquis, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("SetReceivedAmount")]
        public JsonResult SetReceivedAmount(int id = 0, int samplingIdentifierId = 0, float amount = 0, SampleType samplingType = SampleType.Simple)
        {
            if (id == 0 || samplingIdentifierId == 0) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            using (var context = new MyContext())
            {
                SimpleSamplingIdentifier simpleSampleIdentifier = null;
                if (samplingType == SampleType.Simple)
                {
                    var sample = context.SimpleSamples.Include(ss => ss.identificacionMuestraList).FirstOrDefault(s => s.SimpleSampleId == id);
                    if (sample == null) return Json(new {success = false}, JsonRequestBehavior.AllowGet);
                    simpleSampleIdentifier = sample.identificacionMuestraList.FirstOrDefault(s => s.SimpleSamplingIdentifierId == samplingIdentifierId);
                }
                else
                {
                    var sample = context.ComplexSamples.Include(cs => cs.numeroMuestraList).FirstOrDefault(s => s.ComplexSampleId == id);
                    if (sample == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    simpleSampleIdentifier = sample.numeroMuestraList.FirstOrDefault(s => s.SimpleSamplingIdentifierId == samplingIdentifierId);
                }
                if (simpleSampleIdentifier == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                if (simpleSampleIdentifier.ReceivedAmount == amount) return Json(new { success = true, duplicate = true }, JsonRequestBehavior.AllowGet);            
                simpleSampleIdentifier.ReceivedAmount = amount;
                context.SaveChanges();
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);            
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("SetLabNo")]
        public JsonResult SetLabNo(int id = 0, int samplingIdentifierId = 0, float amount = 0, SampleType samplingType = SampleType.Simple)
        {
            if (id == 0 || samplingIdentifierId == 0) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            using (var context = new MyContext())
            {
                SimpleSamplingIdentifier simpleSampleIdentifier = null;
                if (samplingType == SampleType.Simple)
                {
                    var sample = context.SimpleSamples.Include(ss => ss.identificacionMuestraList).FirstOrDefault(s => s.SimpleSampleId == id);
                    if (sample == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    simpleSampleIdentifier = sample.identificacionMuestraList.FirstOrDefault(s => s.SimpleSamplingIdentifierId == samplingIdentifierId);
                }
                else
                {
                    var sample = context.ComplexSamples.Include(cs => cs.numeroMuestraList).FirstOrDefault(s => s.ComplexSampleId == id);
                    if (sample == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    simpleSampleIdentifier = sample.numeroMuestraList.FirstOrDefault(s => s.SimpleSamplingIdentifierId == samplingIdentifierId);
                }
                if (simpleSampleIdentifier == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                if (simpleSampleIdentifier.LabNo == amount) return Json(new { success = true, duplicate = true }, JsonRequestBehavior.AllowGet);
                simpleSampleIdentifier.LabNo = amount;
                context.SaveChanges();
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet); 
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("RemovePhoto")]
        public JsonResult RemovePhoto(int photoId = 0)
        {
            if (photoId == 0) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            try
            {
                using (var context = new MyContext())
                {
                    var photo = context.Photos.FirstOrDefault(p => p.PhotoId == photoId);
                    if (photo == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    context.Photos.Remove(photo);
                    context.SaveChanges();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("GPS")]
        public JsonResult GetGps(int id = 0)
        {
            using (var context = new MyContext())
            {
                var map = context.Maps.FirstOrDefault(m => m.id == id);
                return map == null ? Json(new { gps = ""}, JsonRequestBehavior.AllowGet) : Json(new { gps = map.mapa }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("NewNotifications")]
        public JsonResult NewNotifications(int id = 0)
        {
            if (PendingNots == null) PendingNots = new Dictionary<int, List<string>>();
            if (PendingNots.ContainsKey(id))
            {
                var result = new List<string>(PendingNots[id]);
                PendingNots.Remove(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("UserPosition")]
        public JsonResult GetUserPosition(int employeeId = 0)
        {
            using (var context = new MyContext())
            {
                if (employeeId == 0)
                {
                    var boosId = WebSecurity.CurrentUserId;
                    return Json(context.GetUsersPositions(boosId).Select(p => p.JsonForObj()).ToList(), JsonRequestBehavior.AllowGet);
                }
                return Json(context.GetUserPositions(employeeId).Select(p => p.JsonForObj()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("CurrentPosition")]
        public JsonResult GetCurrentPosition(int employeeId = 0)
        {
            if (employeeId != 0)
                using (var context = new MyContext())
                {
                    var currentPosition = context.GetUserPosition(employeeId);
                    if (currentPosition != null)
                        return Json(currentPosition.JsonForObj(), JsonRequestBehavior.AllowGet);
                    
                    var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                    return Json(new { name = employee.FullName, empty = true }, JsonRequestBehavior.AllowGet);
                }
            return null;
        }

        public static void AddPendingNot(int id, string notification)
        {
            if (PendingNots == null) PendingNots = new Dictionary<int, List<string>>();
            if (!PendingNots.ContainsKey(id))
//                lock(key)
                    PendingNots.Add(id, new List<string>{notification});
            else
//                lock(key)
                    PendingNots[id].Add(notification); 
        }
        private string PrettyNumberString(int number) {
            var stringNumber = number.ToString();
            for (var i = stringNumber.Length; i < 6; i++)
                stringNumber = "0" + stringNumber;
            return stringNumber;
        }
    }
}
