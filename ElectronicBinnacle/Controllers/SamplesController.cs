using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Globalization;
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
using Newtonsoft.Json;
using WebMatrix.WebData;
using System.Data.Entity;

namespace ElectronicBinnacle.Controllers
{
    public class SamplesController : Controller
    {
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Orders")]
        public JsonResult AllOrders(string imei)
        {
            using (var context = new MyContext())
            {
                try
                {
                    var todayAndTomorrowOrders = context.AllOrders().Where(order => order.OrderState != OrderState.NotFinished && order.Sampler.User.IMEI == imei && order.OrderState == OrderState.NotSended && BeforeSe7enDay(order)).ToList();

                    var a = todayAndTomorrowOrders.Select(o => o.ObjForJson("Sample")).ToList();
                    return Json(a, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Source + e.Message + "." + e.InnerException + "." + e.InnerException + "." + e.InnerException }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        private bool BeforeSe7enDay(SamplingOrder order)
        {
            var today = DateTime.Now.GetUnixEpoch();
            var tomorrow = today + 7 * (24 * 60 * 60 * 1000);
            var a = order.SamplingData.StartTime < tomorrow;
            return a;
        }
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Received")]
        public JsonResult Received([FromBody] MyJson data)
        {
            try
            {
//                var list = (List<int>)data.json;
                var list = JsonConvert.DeserializeObject<List<int>>(data.json);
                using (var context = new MyContext())
                {
                    foreach (var orderId in list)
                    {
                        var order = context.Orders.Include(o => o.Creator).Include(o => o.Sampler.User).First(o => o.Id == orderId);
                        if (order.Sampler.User.IMEI != data.imei) return Json(new { success = false, error = "Muestreador Incorrecto." }, JsonRequestBehavior.AllowGet);

                        order.OrderState = OrderState.Sended;
                        var creator = context.Users.Include(u => u.Notifications).First(u => u.UserId == order.Creator.UserId);
                        var msg = string.Format("La orden de trabajo {0} ha sido enviada satisfactoriamente.", order.SamplingData.Identifier);
                        creator.Notifications.Add(new Notification() { SamplerName = order.Sampler.FullName, DATETIME = DateTime.UtcNow.GetUnixEpoch(), NOTIFICATION_TYPE = NotificationType.SendedRecievedOrder, NOTIFICATION_CATEGORY = 0, NOTIFICATION_MSG = msg });
                    }
                    context.SaveChanges();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, error = "Error no Manejado." }, JsonRequestBehavior.AllowGet);
            }
        }

        private JsonResult CheckSamplePost(MyContext context, string imei, int orderId, out Sample sample)
        {
            sample = null;
            try
            {
                var sampler = context.Users.FirstOrDefault(u => u.Employee.Role.RoleId == 5 && u.IMEI == imei);
                if (sampler == null) return Json(new { success = false, error = "Muestreador no registrado." }, JsonRequestBehavior.AllowGet);

                var samplingData = context.GetCleanSamplingInfo(orderId);
                if (samplingData == null) return Json(new { success = false, error = "Sampling Data es Null" }, JsonRequestBehavior.AllowGet);
                if (samplingData.SamplingOrder.Sampler.User.IMEI != imei) return Json(new { success = false, error = "El Muestreador incorrecto está enviando los datos." }, JsonRequestBehavior.AllowGet);
                sample = samplingData;
                return null;
            }
            catch (Exception)
            {
                return Json(new { success = false, error = "Error no Manejado." }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Header")]
        public JsonResult StartSamplingData([FromBody] MyJson data)
        {
            try
            {
//                var header = (Header)data.json;
                Header header;
                try
                {
                    header = JsonConvert.DeserializeObject<Header>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }

                using (var context = new MyContext())
                {
                    var sampler = context.Users.FirstOrDefault(u => u.Employee.Role.RoleId == 5 && u.IMEI == data.imei);
                    if (sampler == null)
                        return Json(new { success = false, error = "Muestreador no registrado." },
                            JsonRequestBehavior.AllowGet);

                    var order = context.GetOrder(header.Id_OrdenMuestreo);
                    if (order.Sampler.User.IMEI != data.imei) return Json(new { success = false, error = "El Muestreador incorrecto está enviando los datos." }, JsonRequestBehavior.AllowGet);
                    if (context.Samples.Any(sd => sd.SampleId == header.Id_OrdenMuestreo))
                        context.RemoveSamplingInfo(header.Id_OrdenMuestreo);

                    order.DataInformation = new Sample { Header = header };
                    context.SaveChanges();
                    
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Croquis")]
        public JsonResult AddCroquis([FromBody] MyJson data)
        {
            try
            {
//                var croquis = (Croquis)data.json;
                Croquis croquis;
                try
                {
                    croquis = JsonConvert.DeserializeObject<Croquis>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    Sample samplingData;
                    var error = CheckSamplePost(context, data.imei, croquis.Id_OrdenMuestreo, out samplingData);
                    if (error != null)
                        return error;

                    context.AddCroquis(samplingData, croquis);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, error = "Error no Manejado." }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("mapa")]
        public JsonResult AddMap([FromBody] MyJson data)
        {
            try
            {
                //var map = (Croquis)data.json;
                Map map;
                try
                {
                    map = JsonConvert.DeserializeObject<Map>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    Sample samplingData;
                    var error = CheckSamplePost(context, data.imei, map.Id_OrdenMuestreo, out samplingData);
                    if (error != null)
                        return error;
                    context.AddMap(map);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, error = "Error no Manejado." }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("QualityControl")]
        public JsonResult SetQualityControl([FromBody] MyJson data)
        {
            try
            {
//                var qualityControl = (QualityControl)data.json;
                QualityControl qualityControl;
                try
                {
                    qualityControl = JsonConvert.DeserializeObject<QualityControl>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    Sample samplingData;
                    var error = CheckSamplePost(context, data.imei, qualityControl.Id_OrdenMuestreo, out samplingData);
                    if (error != null)
                        return error;

                    context.SetQualityControl(samplingData.SampleId, qualityControl);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, error = "Error no Manejado." }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("SamplingPlan")]
        public JsonResult SetSamplingPlan([FromBody] MyJson data)
        {
            try
            {
//                var samplingPlan = (SamplingPlan)data.json;
                SamplingPlan samplingPlan;
                try
                {
                    samplingPlan = JsonConvert.DeserializeObject<SamplingPlan>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    Sample samplingData;
                    var error = CheckSamplePost(context, data.imei, samplingPlan.Id_OrdenMuestreo, out samplingData);
                    if (error != null)
                        return error;

                    context.SetSamplingPlan(samplingPlan, samplingData);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, error = "Error no Manejado." }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("SimpleSample")]
        public JsonResult AddSimpleSample([FromBody] MyJson data)
        {
            try
            {
//                var simpleSample = (SimpleSample)data.json;
                SimpleSample simpleSample;
                try
                {
                    simpleSample = JsonConvert.DeserializeObject<SimpleSample>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    Sample samplingData;
                    var error = CheckSamplePost(context, data.imei, simpleSample.Id_OrdenMuestreo, out samplingData);
                    if (error != null)
                        return error;

                    context.AddSimpleSample(samplingData, simpleSample);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, error = "Error no Manejado." }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("ComplexSample")]
        public JsonResult AddComplexSample([FromBody] MyJson data)
        {
            try
            {
//                var complexSample = (ComplexSample)data.json;
                ComplexSample complexSample;
                try
                {
                    complexSample = JsonConvert.DeserializeObject<ComplexSample>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    Sample samplingData;
                    var error = CheckSamplePost(context, data.imei, complexSample.Id_OrdenMuestreo, out samplingData);
                    if (error != null)
                        return error;

                    context.AddComplexSample(samplingData, complexSample);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, error = "Error no Manejado." }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("SampleString")]
        public JsonResult SetSampleString([FromBody] MyJson data)
        {
            try
            {
//                var sampleString = (SampleString)data.json;
                SampleString sampleString;
                try
                {
                    sampleString = JsonConvert.DeserializeObject<SampleString>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    Sample samplingData;
                    var error = CheckSamplePost(context, data.imei, sampleString.Id_OrdenMuestreo, out samplingData);
                    if (error != null)
                        return error;

                    context.SetSampleString(samplingData, sampleString);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, error = "Error no Manejado." }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Binnacle")]
        public JsonResult SetBinnacle([FromBody] MyJson data)
        {
            try
            {
//                var binnacle = (Binnacle)data.json;
                Binnacle binnacle;
                try
                {
                    binnacle = JsonConvert.DeserializeObject<Binnacle>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    Sample samplingData;
                    var error = CheckSamplePost(context, data.imei, binnacle.Id_OrdenMuestreo, out samplingData);
                    if (error != null)
                        return error;

                    context.SetBinnacle(samplingData.SampleId, binnacle);
                    
                    var header = samplingData.Header;
                    var order = context.Orders.Find(samplingData.SampleId);
                    order.OrderState = !header.cumplida ? OrderState.Unredeemed : OrderState.NotEvaluated;
                    OrdersController.AddPendingNot(order.Creator.UserId, string.Format("Se ha recibido información de una orden de trabajo {0}.", !header.cumplida ? "incumplida" : "realizada"));
                    
                    var creator = context.Users.Include(u => u.Notifications).First(u => u.UserId == order.Creator.UserId);
                    var msg = string.Format("La orden de trabajo {0} se recibió {1}.", order.SamplingData.Identifier, !header.cumplida ? "cancelada" : "realizada");
                    creator.Notifications.Add(new Notification() { SamplerName = order.Sampler.FullName, DATETIME = DateTime.Now.GetUnixEpoch(), NOTIFICATION_TYPE = NotificationType.SendedRecievedOrder, NOTIFICATION_CATEGORY = 2, NOTIFICATION_MSG = msg });
                    context.SaveChanges();
                    
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Photo")]
        public JsonResult Photo([FromBody] MyJson data)
        {
            try
            {
//                var photo = (Photo)data.json;
                Photo photo;
                try
                {
                    photo = JsonConvert.DeserializeObject<Photo>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    Sample samplingData;
                    var error = CheckSamplePost(context, data.imei, photo.Id_OrdenMuestreo, out samplingData);
                    if (error != null)
                        return error;

                    context.AddPhoto(samplingData, photo);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (DbEntityValidationException e)
            {
                var a = e.EntityValidationErrors.ToList();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }



        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Notification")]
        public JsonResult Notification([FromBody] MyJson data)
        {
            try
            {
//                var notify = (Notification)data.json;
                Notification notify;
                try
                {
                    notify = JsonConvert.DeserializeObject<Notification>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    var sampler = context.Users.Include("Employee").FirstOrDefault(u => !u.Employee.DropDown && u.Employee.Role.RoleId == 5 && u.IMEI == data.imei);
                    if (notify.NOTIFICATION_MSG.Contains("Ha instalado una actualización de la aplicación de MasterSampler. Número de la versión:"))
                    {
                        var msg = notify.NOTIFICATION_MSG;
                        var firstIndex = msg.LastIndexOf(": ") + 2;
                        sampler.Employee.AppVertion = msg.Substring(firstIndex, msg.Length - firstIndex - 1);
                    }

                    if (sampler == null) return Json(new { success = false, error = "Muestreador no registrado." }, JsonRequestBehavior.AllowGet);

                    var boss = context.Users.Include(u => u.Subordinates).Include(u => u.Notifications).FirstOrDefault(u => u.Subordinates.Any(s => s.User.IMEI == sampler.IMEI));
                    if (boss == null) return Json(new { success = false, error = "Muestreador sin jefe." }, JsonRequestBehavior.AllowGet);
                    notify.SamplerName = sampler.Employee.FullName;
                    if (boss.Notifications == null) boss.Notifications = new List<Notification>();
                    boss.Notifications.Add(notify);
                    OrdersController.AddPendingNot(boss.UserId, "Ha recibido una nueva notificación de " + notify.SamplerName);
                    context.SaveChanges();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("UserPosition")]
        public JsonResult UpdatePosition([FromBody] MyJson data)
        {
            try
            {
//                var position = (Position)data.json;
                Position position;
                try
                {
                    position = JsonConvert.DeserializeObject<Position>(data.json);
                }
                catch (Exception)
                {
                    return Json(new { success = false, error = "Error de parseo" }, JsonRequestBehavior.AllowGet);
                }
                using (var context = new MyContext())
                {
                    var sampler = context.Employees.Include(e => e.User).FirstOrDefault(e => !e.DropDown && e.Role.RoleId == 5 && e.User.IMEI == data.imei);
                    if (sampler == null) return Json(new { success = false, error = "Muestreador no registrado." }, JsonRequestBehavior.AllowGet);

                    position.Employee = sampler;
                    context.Positions.Add(position);
                    context.SaveChanges();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("MockMap")]
        public void Map([FromBody] MyJson data)
        {
            using (var context = new MyContext())
            {
                var a = context.Positions.ToList();
            }
            //var a = new MyJson()
            //        {
            //            imei = "013287002652897",
            //            json =
            //                "{\"DATETIME\":1421344265053,\"NOTIFICATION_TYPE\":6,\"NOTIFICATION_CATEGORY\":1,\"NOTIFICATION_MSG\":\"Ha instalado una actualización de la aplicación de MasterSampler. Número de la versión: 2.\"}"
            //        };

            //this.Notification(a);
        }


        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("SendMockNotify")]
        public void SendMockNotify()
        {
            var not1 = "{ DATETIME : 1424371800000, NOTIFICATION_CATEGORY : 0, NOTIFICATION_MSG : \"Notification #1 For 1\", NOTIFICATION_TYPE : 5 }";
            var not2 = "{ DATETIME : 1424371800000, NOTIFICATION_CATEGORY : 1, NOTIFICATION_MSG : \"Notification #2  For 1\", NOTIFICATION_TYPE : 6 }";
            var not3 = "{ DATETIME : 1424371800000, NOTIFICATION_CATEGORY : 2, NOTIFICATION_MSG : \"Notification #3 For 1\", NOTIFICATION_TYPE : 7 }";
            var not4 = "{ DATETIME : 1424371800000, NOTIFICATION_CATEGORY : 0, NOTIFICATION_MSG : \"Notification #4 For 1\", NOTIFICATION_TYPE : 8 }";
            var not5 = "{ DATETIME : 1424371800000, NOTIFICATION_CATEGORY : 1, NOTIFICATION_MSG : \"Notification #5  For 1\", NOTIFICATION_TYPE : 1 }";

            this.Notification(new MyJson() { imei = "3", json = not1 });
            this.Notification(new MyJson() { imei = "3", json = not2 });
            this.Notification(new MyJson() { imei = "3", json = not3 });
            this.Notification(new MyJson() { imei = "3", json = not4 });
            this.Notification(new MyJson() { imei = "3", json = not5 });
            this.Notification(new MyJson() { imei = "3", json = not3 });
        }
    }

    public class MyJson
    {
//        public object json { get; set; }
        public string json { get; set; }
        public string imei { get; set; }
    }
}
