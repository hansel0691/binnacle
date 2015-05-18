using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Antlr.Runtime;
using ElectronicBinnacle.Models.Context;
using ElectronicBinnacle.Models.Models.SamplingOrder;

namespace ElectronicBinnacle.Models.Models.UserControl
{
    [Table("UserProfile")]
    public class User
    {
        #region Constructor

        public User()
        {
            this.CalibrationKit = new CalibrationKit();
        }

        #endregion
        #region Properties

        public int UserId { get; set; }
        public string IMEI { get; set; }
        public string Name { get; set; }
        public string BinnacleIdentifier { get; set; }
        public string Job { get; set; }
        public string Category { get; set; }
        public string Subsidiary { get; set; }
        public string SamplingIdentifier { get; set; }
        public string Password { get; set; }

        [ForeignKey("UserId")]
        public Employee Employee { get; set; }
        public List<Employee> Subordinates { get; set; }
        public List<SamplingOrder.SamplingOrder> CreatedOrders { get; set; }
        public List<Notification> Notifications { get; set; }

        public CalibrationKit CalibrationKit { get; set; }
        public List<WatterType> WatterTypes { get; set; }

        #endregion
        #region Methods

        public void CopyProps(User user, bool fromEmployee = false)
        {
            this.UserId = user.UserId;
            this.IMEI = user.IMEI;
            this.Name = user.Name;
            this.BinnacleIdentifier = user.BinnacleIdentifier;
            this.Job = user.Job;
            this.Category = user.Category;
            this.Subsidiary = user.Subsidiary;
            this.SamplingIdentifier = user.SamplingIdentifier;
            this.Password = user.Password;
            if (this.CalibrationKit == null) this.CalibrationKit = new CalibrationKit();
            this.CalibrationKit.Name = user.CalibrationKit.Name;
            this.CalibrationKit.Series = user.CalibrationKit.Series;
            this.CalibrationKit.Model = user.CalibrationKit.Model;

            if (!fromEmployee && this.Subordinates != null && user.Subordinates != null)
            {
                this.Subordinates.Clear();
                this.Subordinates.AddRange(user.Subordinates);
            }
            if (this.WatterTypes != null && user.WatterTypes != null)
            {
                this.WatterTypes.Clear();
                this.WatterTypes.AddRange(user.WatterTypes);
            }
            //if (this.Notifications != null && user.Notifications != null)
            //{
            //    this.Notifications.Clear();
            //    this.Notifications.AddRange(user.Notifications);
            //}
        }
        public dynamic ObjForJson(string view = "")
        {
            
            if (view == "Routes")
                return new
                       {
                           this.UserId,
                           this.Notifications
                       };

            return new
            {
                this.UserId,
                this.IMEI,
                this.Name,
                this.BinnacleIdentifier,
                this.Job,
                this.Category,
                this.Subsidiary,
                this.SamplingIdentifier,
                this.Password,
                Subordinates = this.Subordinates == null ? null : this.Subordinates.Select(e => e.ObjForJson()).ToList(),
                Employee = this.Employee == null ? null : new
                {
                    this.Employee.EmployeeId,
                    this.Employee.Name,
                    this.Employee.LastName,
                    this.Employee.FullName,
                    this.Employee.PhoneNumber,
                    this.Employee.Email,
//                    this.Employee.Signature,
                    this.Employee.Speciality,
                    this.Employee.Degree,
                    this.Employee.DropDown,
                    this.Employee.RegisterDate,
                    this.Employee.Role
                },
                this.CalibrationKit,
                this.WatterTypes
            };
        }

        public dynamic JsonForAuth(bool withOrderCount = false)
        {
            var createrOrdersCount = 0;
            if (withOrderCount)
                using (var context = new MyContext())
                {
                    var user = context.Users.Find(this.UserId);
                    createrOrdersCount = context.Entry(user)
                        .Collection(u => u.CreatedOrders)
                        .Query()
                        .Count();
                }
            return new
            {
                this.UserId,
                this.Name,
                this.SamplingIdentifier,
                OrdersCount = createrOrdersCount,
                Employee = this.Employee == null ? null : new
                {
                    this.Employee.EmployeeId,
                    this.Employee.Name,
                    this.Employee.LastName,
                    this.Employee.FullName,
                    this.Employee.Role
                }
            };
        }

        public List<Employee> SamplersSubordinates()
        {
            if (this.Employee.Role.RoleId == 4 || this.Employee.Role.RoleId > 5)
                return this.Subordinates ?? new List<Employee>();
            var subordinates = new List<Employee>();
            foreach (var subordinate in this.Subordinates)
                using (var context = new MyContext())
                {
                    var s = context.Employees.Include("User.Subordinates").Include("Role.Permissions").FirstOrDefault(e => e.EmployeeId == subordinate.EmployeeId);
                    subordinates.AddRange(s.User.SamplersSubordinates());

                }
            return subordinates;
        }
        public IEnumerable<SamplingOrder.SamplingOrder> AllOrders(ref bool breakNow)
        {
            var orders = new List<SamplingOrder.SamplingOrder>();
            if (breakNow) return orders;

            var seeOrdersPermission = this.Employee.Role.Permissions.FirstOrDefault(p => p.Identifier == PermissionType.AllOrders);
            //var seeOrdersPermission = new Permission(){Value = PermissionValue.Full, Identifier = PermissionType.AllOrders};
            if (seeOrdersPermission == null)
                return new List<SamplingOrder.SamplingOrder>();
            if (seeOrdersPermission.Value == PermissionValue.Assign)
                orders.AddRange(this.CreatedOrders ?? new List<SamplingOrder.SamplingOrder>());
            else
                using (var context = new MyContext())
                {
                    orders.AddRange(context.AllOrders());
                    breakNow = true;
                    return orders.Distinct(new OrderComparer());
                }

            if (this.Subordinates != null)
                foreach (var subordinate in this.Subordinates)
                {
                    using (var context = new MyContext())
                    {
                        var userSub = context.Employees.Include("User.CreatedOrders").Include("Role.Permissions").FirstOrDefault(e => e.EmployeeId == subordinate.EmployeeId);
                        if (userSub.Role == null || userSub.Role.RoleId == 5)
                            continue;
                        orders.AddRange(userSub.User.AllOrders(ref breakNow));
                    }
                }
            return orders.Distinct(new OrderComparer());
        } 

        #endregion

        public IEnumerable<Employee> AllSubordinates()
        {
            if (this.Employee.Role.RoleId == 5)
                yield break;
            if (this.Employee.Role.RoleId == 2)
                using (var context = new MyContext())
                    foreach (var employee in context.AllEmployees())
                        if (employee.Role.RoleId > 2)
                            yield return employee;
            foreach (var subordinate in this.Subordinates)
            {
                yield return subordinate;
                using (var context = new MyContext())
                {
                    var loadedSubordinate = context.Employees.Include("Role").Include("User.Subordinates").FirstOrDefault(e => e.EmployeeId == subordinate.EmployeeId);
                    foreach (var s in loadedSubordinate.User.AllSubordinates())
                        yield return s;
                }
            }
        }
    }

    [ComplexType]
    public class CalibrationKit
    {
        public string Name { get; set; }
        public string Series { get; set; }
        public string Model { get; set; }
    }

    public class WatterType
    {
        public int Id { get; set; }
        public SamplingType SampleKind { get; set; }
    }

}

internal class OrderComparer : IEqualityComparer<SamplingOrder>
{
    public bool Equals(SamplingOrder x, SamplingOrder y)
    {
        return x.Id == y.Id;
    }

    public int GetHashCode(SamplingOrder obj)
    {
        return obj.Id;
    }
}
