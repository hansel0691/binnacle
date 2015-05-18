using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ElectronicBinnacle.Models.Models.UserControl
{
    public class Position
    {
        public int PositionId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public float Accuracy { get; set; }
        public float Speed { get; set; }
        public long DateTime { get; set; }

        public Employee Employee { get; set; }

        public dynamic JsonForObj()
        {
            return new
                   {
                       this.PositionId,
                       this.Longitude,
                       this.Latitude,
                       this.Accuracy,
                       this.Speed,
                       this.DateTime,
                       Employee = new
                                  {
                                      this.Employee.EmployeeId,
                                      this.Employee.FullName,
                                  }
                   };
        }
    }
}