using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicBinnacle.Models.Models.UserControl;

namespace ElectronicBinnacle.Models.Models.SamplingOrder
{
    public enum SamplingType
    {
        AgP,
        AgR,
        AgN,
        AgS,
        AgEst,
        AgMar
    }

    [ComplexType]
    public class SamplingData
    {
        #region Properties

        public string Identifier { get; set; } 
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public int Period { get; set; }
        public SamplingType SamplingKind { get; set; }

        public string RequirementsTechnical { get; set; }
        public string ObservationsForSafety { get; set; }
        public bool DuplicatesBlank { get; set; }


        #endregion
        #region Methods

        public void CopyProps(SamplingData samplingData)
        {
            this.Identifier = samplingData.Identifier;
            this.StartTime = samplingData.StartTime;
            this.EndTime = samplingData.EndTime;
            this.Period = samplingData.Period;
            this.SamplingKind = samplingData.SamplingKind;

            this.RequirementsTechnical = samplingData.RequirementsTechnical;
            this.ObservationsForSafety = samplingData.ObservationsForSafety;
            this.DuplicatesBlank = samplingData.DuplicatesBlank;
        }

        #endregion
    }
}