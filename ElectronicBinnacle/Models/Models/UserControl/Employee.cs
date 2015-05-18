using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;


namespace ElectronicBinnacle.Models.Models.UserControl
{
    public enum DegreeType
    {
        None,
        Lic,
        Ing,
        Tec
    }

    public class Employee
    {
        #region Properties

        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [MaxLength]
        public string Signature { get; set; }
        public string Speciality { get; set; }
        public DegreeType Degree { get; set; }
        public bool DropDown { get; set; }
        public long RegisterDate { get; set; }
        public string AppVertion { get; set; }



        public  User User { get; set; }
        public  Role Role { get; set; }

        public List<Position> Positions { get; set; }


        [NotMapped]
        public string FullName {get { return this.Name + " " + this.LastName; }}


        #endregion
        #region Methods

        public void CopyProps(Employee employee)
        {
            this.Name = employee.Name;
            this.LastName = employee.LastName;
            this.PhoneNumber = employee.PhoneNumber;
            this.Email = employee.Email;
            this.Signature = employee.Signature;
            this.Speciality = employee.Speciality;
            this.Degree = employee.Degree;
            this.DropDown = employee.DropDown;
            this.RegisterDate = employee.RegisterDate;
        }
        public dynamic ObjForJson(string view = "")
        {
            return new
                   {
                       this.EmployeeId,
                       this.Name,
                       this.LastName,
                       this.PhoneNumber,
                       this.Email,
//                       this.Signature,
                       this.Speciality,
                       this.Degree,
                       this.DropDown,
                       this.RegisterDate,
                       User = this.User == null ? null : this.User.ObjForJson(),
                       this.Role,
                       this.FullName
                   };
        }

        #endregion

        public dynamic JsonForStats()
        {
            return new
                   {
                       this.EmployeeId,
                       this.Name,
                       this.LastName,
//                       Role = new
//                              {
//                                  this.Role.Name
//                              },
//                       User = new
//                              {
//                                  this.User.Name
//                              }
                   };
        }
    }
}