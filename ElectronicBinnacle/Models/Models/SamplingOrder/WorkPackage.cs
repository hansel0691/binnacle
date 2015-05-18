using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.SamplingOrder
{
    public enum SampleKind
    {
        Simple,
        Complex
    }

    public class WorkPackage
    {
        #region Properties

        public int Id { get; set; }
        public List<Package> Packages { get; set; }//**
        public int SamplesNumber { get; set; }
        public SampleKind Type { get; set; }
        public double Period { get; set; }
        public SamplingOrder SamplingOrder { get; set; }

        #endregion
        #region Methods

        

        #endregion

        public dynamic ObjForJson()
        {
            return new
                   {
                       this.Id,
                       this.Packages,
                       this.SamplesNumber,
                       this.Type,
                       this.Period
                   };
        }
    }
}