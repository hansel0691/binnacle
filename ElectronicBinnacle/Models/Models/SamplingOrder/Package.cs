using System.Collections.Generic;

namespace ElectronicBinnacle.Models.Models.SamplingOrder
{
    public class Package
    {
        #region Properties

        public int PackageId { get; set; }
        public string Identifier { get; set; }
        public bool Standard { get; set; }

        public  List<Parameter> Parameters { get; set; }

        #endregion
        #region Methods

        public void CopyProps(Package package)
        {
            this.Identifier = package.Identifier;
            this.Standard = package.Standard;
            //if (this.Parameters != null)
            //    this.Parameters.Clear();
            //if (package.Parameters != null)
            //    this.Parameters.AddRange(package.Parameters);
        }

        #endregion
    }
}