using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using ElectronicBinnacle.Models;
using ElectronicBinnacle.Models.Context;
using ElectronicBinnacle.Models.Models.SamplingOrder;
using ElectronicBinnacle.Models.Models.UserControl;
using WebMatrix.WebData;

namespace ElectronicBinnacle.Controllers
{
    public class EmployeesController : Controller
    {
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Employee")]
        public JsonResult GetEmployee(int id = 0, int page = 0, bool active = true, string searchName = "")
        {
            using (var context = new MyContext())
            {
                if (id == 0)
                {
                    var employees = context.AllEmployees(new Employee{DropDown = !active, Name = searchName}).Where(e => e.EmployeeId != 1);
                    if (page != 0)
                        return Json(new { employees = employees.Skip((page - 1) * 20).Take(20).Select(e => e.ObjForJson()).ToList(), count = Math.Ceiling(employees.Count() / 20.0) }, JsonRequestBehavior.AllowGet);
                    return Json(context.AllEmployees().Where(e => e.Role.RoleId != 1).Select(e => e.ObjForJson()).ToList(), JsonRequestBehavior.AllowGet);
                }
                return Json(context.GetEmployee(id).ObjForJson("EditView"), JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Employee")]
        public JsonResult SaveEmployee([FromBody]Employee employee)
        {
            using (var context = new MyContext())
            {
                var duplicate = context.Users.FirstOrDefault(u => !u.Employee.DropDown && u.Name == employee.User.Name && u.UserId != employee.User.UserId);
                if (duplicate != null)
                    return Json(new {success = false, error = "Escoja otro nombre de usuario."}); 
                
                if (employee.EmployeeId == 0)
                {; 
                    employee.RegisterDate = DateTime.Now.GetUnixEpoch();
                    return context.AddEmployee(employee) == null ? Json(new { success = false }) : Json(new { success = true });
                }
                return !context.UpdateEmployee(employee) ? Json(new { success = false }) : Json(new { success = true });
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Sampler")]
        public JsonResult GetSampler(int id = 0, int watterMatch = -1)
        {
            using (var context = new MyContext())
            {
                var authUser = context.GetUser(WebSecurity.CurrentUserId);
                if (id == 0 || authUser.Employee.Role.Permissions.Any(p => p.Identifier == PermissionType.NewOrder && p.Value == PermissionValue.Full))
                    return Json(context.AllEmployees().Where(e => !e.DropDown && e.Role.RoleId == 5).Select(e => e.ObjForJson()).ToList(), JsonRequestBehavior.AllowGet);
                if (id == -1)
                {
                    if (authUser.Employee.Role.RoleId == 2)
                    {
                        var allEmployees = context.Employees.Where(e => e.Role.RoleId == 5).ToList();
                        return Json(allEmployees.Select(e => e.ObjForJson()), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var a = authUser.SamplersSubordinates().ToList();
                        var b = a.Where(e => !e.DropDown && (watterMatch == -1 || e.User.WatterTypes.Any(w => w.SampleKind == (SamplingType)watterMatch))).ToList();
                        return Json(b.Select(e => e.ObjForJson()), JsonRequestBehavior.AllowGet);
                    }
                }
            }
                
            return null;
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Coordinator")]
        public JsonResult GetCoordinator(int id = 0)
        {
            using (var context = new MyContext())
            {
                if (id == 0)
                    return Json(context.AllEmployees().Where(e => !e.DropDown && e.Role.RoleId == 4).Select(e => e.ObjForJson()).ToList(), JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("ToogleActiveEmployee")]
        public JsonResult ToogleActiveEmployee(int id, int page = 1, bool active = true, string searchName = "")
        {
            using (var context = new MyContext())
            {
                var employee = context.GetCleanEmployee(id);
                var employees = context.AllEmployees(new Employee { DropDown = !active, Name = searchName }).Where(e => e.EmployeeId != 1).Skip(page * 20).ToList();
                var next = employees.FirstOrDefault();
                var last = next != null && next.EmployeeId == employees.Last().EmployeeId;

                employee.DropDown = !employee.DropDown;
                employee.User.IMEI = "";
                context.SaveChanges();
                return Json(new { employee = next != null ? next.ObjForJson("IndexView") : null, last }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("User")]
        public JsonResult AllUsers(int id = 0, int page = 0, string searchName = "")
        {
            using (var context = new MyContext())
            {
                if (id == 0)
                {
                    var users = context.AllUsers(new User { Name = searchName }).Where(u => !u.Employee.DropDown && u.Employee.Role.RoleId != 5).Select(u => u.ObjForJson("IndexUsers")).ToList();
                    if (page != 0)
                        return Json(new {users = users.Skip((page - 1)*20).Take(20), count = Math.Ceiling(users.Count() / 20.0) }, JsonRequestBehavior.AllowGet);
                    return Json(users, JsonRequestBehavior.AllowGet);
                }
                return Json(context.GetUser(id).ObjForJson(), JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("User")]
        public JsonResult SaveUser([FromBody]User user)
        {
            using (var context = new MyContext())
            {
                var duplicate = context.Users.FirstOrDefault(u => !u.Employee.DropDown && u.Name == user.Name && u.UserId != user.UserId);
                if (duplicate != null)
                    return Json(new { success = false, error = "Escoja otro nombre de usuario." }); 

                var subordinates = new List<Employee>(user.Subordinates ?? new List<Employee>());
                if (user.Subordinates != null)
                user.Subordinates.Clear();
                foreach (var s in subordinates)
                    user.Subordinates.Add(context.GetEmployee(s.EmployeeId));
                return !context.UpdateUser(user) ? Json(new { success = false, error = "Ha ocurrido un error al crear el usuario" }) : Json(new { success = true });
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Role")]
        public JsonResult GetRole(int id = 0, int page = 0, bool active = true, string searchName = "")
        {
            using (var context = new MyContext())
            {
                if (id == 0)
                {
                    var roles = context.AllRoles(new Role{Active = active, Name = searchName }).ToList();
                    if (page != 0)
                        return Json(new { roles = roles.Skip((page - 1) * 20).Take(20), count = Math.Ceiling(roles.Count() / 20.0) }, JsonRequestBehavior.AllowGet);
                    return Json( roles, JsonRequestBehavior.AllowGet);
                }
                return Json(context.GetRole(id), JsonRequestBehavior.AllowGet); ;
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("ActiveRoles")]
        public JsonResult GetActiveRoles()
        {
            using (var context = new MyContext())
            {
                var a = context.AllRoles().Where(r => r.Active && r.RoleId != 1).ToList();
                return Json(a, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Role")]
        public JsonResult SaveRole([FromBody]Role role)
        {
            using (var context = new MyContext())
            {
                var permissionList = new List<Permission>();
                if (role.Permissions != null)
                {
                    permissionList.AddRange(
                        role.Permissions.Where(permission => permission.Value != PermissionValue.None));
                    if (permissionList.Count != role.Permissions.Count())
                    {
                        role.Permissions.Clear();
                        role.Permissions.AddRange(permissionList);
                    }
                }
                if (role.RoleId == 0)
                    return context.AddRole(role) == null ? Json(new { success = false }) : Json(new { success = true });
                return !context.UpdateRole(role) ? Json(new { success = false }) : Json(new { success = true });
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("ToogleActiveRole")]
        public JsonResult ToogleActive(int id, int page = 1, bool active = true, string searchName = "")
        {
            using (var context = new MyContext())
            {
                var role = context.GetCleanRole(id);
                var roles = context.AllRoles(new Role { Active = active, Name = searchName }).Skip(page * 20).ToList();
                var next = roles.FirstOrDefault();
                var last = next != null && next.RoleId == roles.Last().RoleId;

                role.Active = !role.Active;
                context.SaveChanges();
                return Json(new { role = next , last}, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("UploadSign")]
        public JsonResult UploadSign(int id = 0, HttpPostedFileBase file = null)
        {
            if (file == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            var r = new Random();
            var fullPath = @ConfigurationManager.AppSettings["FileUploadDirectory"] + "Images\\" + r.Next(10000) + file.FileName;

            file.SaveAs(fullPath);
            var image = ImageToBase64(new Bitmap(fullPath), ImageFormat.Png);

            return Json(new { success = true, image }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DownloadSign(int employeeId = 0)
        {
            if (employeeId == 0)
                return Json(new { success = false}, JsonRequestBehavior.AllowGet);
            using (var context = new MyContext())
            {
                var employee = context.GetEmployee(employeeId);
                return Json(new { success = true, employee.Signature }, JsonRequestBehavior.AllowGet);
            }

        }

        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Subordinates")]
        public JsonResult GetSubordinates(int page = 0, string searchName = "")
        {
            using (var context = new MyContext())
            {
                var authUser = context.GetSubordinatesUser(WebSecurity.CurrentUserId);
                var subordinates = authUser.SamplersSubordinates().Where(s => !s.DropDown).ToList();
                if (page != 0)
                    return Json(new { subordinates = subordinates.Skip((page - 1) * 20).Take(20).Select(e => e.JsonForStats()).ToList(), count = Math.Ceiling(subordinates.Count() / 20.0) }, JsonRequestBehavior.AllowGet);
                return Json(new {subordinates = new List<Employee>()}, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Orders")]
        public JsonResult GetOrders(int employeeId)
        {
            using (var context = new MyContext())
            {
                var orders = context.AllOrders().Where(o => o.Sampler != null && o.Sampler.EmployeeId == employeeId).Select(o => o.JsonForStats()).ToList();
                return Json(new {orders}, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("BoosAndVertion")]
        public JsonResult BoosAndVertion(int employeeId)
        {
            using (var context = new MyContext())
            {
                var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                var boos = context.Users.Include("Employee").Include("Subordinates").FirstOrDefault(u => u.Subordinates.Any(e => e.EmployeeId == employeeId));
                return Json(new { boos = boos.Employee.FullName, employee.AppVertion }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("Path")]
        public JsonResult Path(int employeeId = 0 , long date = 0)
        {
            using (var context = new MyContext())
            {
                var employee = context.Employees.Include("Positions").FirstOrDefault(e => e.EmployeeId == employeeId);
                var path = employee.Positions.Where(p => date <= p.DateTime && p.DateTime <= date + (24 * 60 * 60 * 1000)).Select(p => p.JsonForObj());
                return Json(new { path, employeeId }, JsonRequestBehavior.AllowGet);
            }
        }
//        LastPositions
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("LastPositions")]
        public JsonResult LastPositions(int employeeId = 0, int lastId = 0, long date = 0)
        {
            if (lastId == 0) return null;
            using (var context = new MyContext())
            {
                var employee = context.Employees.Include("Positions").FirstOrDefault(e => e.EmployeeId == employeeId);
                var path = employee.Positions.Where(p => date <= p.DateTime && p.DateTime <= date + (24 * 60 * 60 * 1000) && p.PositionId > lastId).Select(p => p.JsonForObj());
                return Json(new { path}, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult Permissions()
        {
            return Json(Permission.AllPermissions().Select(Permission.ToSpanish), JsonRequestBehavior.AllowGet);
        }


        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("CheckImei")]
        public JsonResult CheckImei(string imei = "0", int id = 0)
        {
            using (var context = new MyContext())
            {
                var count = context.Users.Where(u => !u.Employee.DropDown && u.UserId != id).Select(u => u.IMEI).Count(i => i == imei);
                return count > 0 ? null : Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
