using System.Collections.Generic;

namespace ElectronicBinnacle.Models.Models.SamplingOrder
{
    public class Parameter
    {
        #region Properties

        public int ParameterId { get; set; }
        public string Identifier { get; set; }
        public string Container { get; set; }
        public double Volume { get; set; }
        public string Preserver { get; set; }
        public double TMPA { get; set; }
        
        public string Key { get; set; }
        public bool FieldMeasurement { get; set; }

        public int Creator { get; set; }
        
        //public List<Package> Packages { get; set; }

        #endregion
        #region Methods

        public void CopyProps(Parameter parameter)
        {
            this.Identifier = parameter.Identifier;
            this.Container = parameter.Container;
            this.Volume = parameter.Volume;
            this.Preserver = parameter.Preserver;
            this.TMPA = parameter.TMPA;
            this.Key = parameter.Key;
            this.FieldMeasurement = parameter.FieldMeasurement;
            this.Creator = parameter.Creator;
        }

        #endregion
    }
}