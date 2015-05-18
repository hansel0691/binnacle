using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicBinnacle.Models.Models.SamplingOrder
{
    [ComplexType]
    public class LocationData
    {
        #region Properties

        public string Place { get; set; }
        public string StreetNo { get; set; }
        public string Colony { get; set; }
        public string DelMpio { get; set; }
        public string Edo { get; set; }
        public string CP { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public string Address {
            get
            {
                return string.Format("Calle {0}, colonia {1}, {2}, {3}, C.P. {4}", this.StreetNo, this.Colony,
                    this.DelMpio, this.Edo, this.CP);
            }
        }

        #endregion

        public void CopyProps(LocationData locationData)
        {
            this.Place = locationData.Place;
            this.StreetNo = locationData.Place;
            this.Colony = locationData.Colony;
            this.DelMpio = locationData.DelMpio;
            this.Edo = locationData.Edo;
            this.CP = locationData.CP;
            this.Contact = locationData.Contact;
            this.Phone = locationData.Phone;
            this.Cellphone = locationData.Cellphone;
            this.Email = locationData.Email;

        }
    }
}