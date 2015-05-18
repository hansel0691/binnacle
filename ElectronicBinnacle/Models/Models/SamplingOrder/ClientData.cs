using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicBinnacle.Models.Models.SamplingOrder
{
    [ComplexType]
    public class ClientData
    {
        #region Properties

        public string SocialReason { get; set; } 
        public string StreetNo { get; set; }
        public string Colony { get; set; }
        public string DelMpio { get; set; }
        public string Edo { get; set; }
        public string CP { get; set; }
        public string RFC { get; set; }
        public bool BillReport { get; set; } //si se marca que no poner quien lo factura en BillerClientData.

        [NotMapped]
        public string Address
        {
            get
            {
                return string.Format("Calle {0}, colonia {1}, {2}, {3}, C.P. {4}", this.StreetNo, this.Colony,
                    this.DelMpio, this.Edo, this.CP);
            }
        }

        #endregion

        public void CopyProps(ClientData clientData)
        {
            this.SocialReason = clientData.SocialReason;
            this.StreetNo = clientData.StreetNo;
            this.Colony = clientData.Colony;
            this.DelMpio = clientData.DelMpio;
            this.Edo = clientData.Edo;
            this.CP = clientData.CP;
            this.RFC = clientData.RFC;
            this.BillReport = clientData.BillReport;
        }
    }
}