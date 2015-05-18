using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ElectronicBinnacle.Models.Models.SamplingOrder;

namespace ElectronicBinnacle.Models.Models.Samples
{
    public class SampleString
    {
        #region Constructor

        public SampleString()
        {
            entrega1 = new DeliveryData();
            recibe1 = new DeliveryData();
        }

        #endregion
        #region Properties

        public int Id_OrdenMuestreo { get; set; }

        [ForeignKey("Sample")]
        public int SampleStringId { get; set; }

        //borrar cuando se cambie el modelo. Esta ropiedad no existe mas.
        public string observacionesIngresoMuestra { get; set; }
        public string ordenMuestreoEditable { get; set; }
        public DeliveryData entrega1 { get; set; }
        public DeliveryData recibe1 { get; set; }
        [MaxLength]
        public string firma { get; set; }

        public string nombre { get; set; }
        [MaxLength]
        public string firma2 { get; set; }
        public string nombre2 { get; set; }
        [MaxLength]
        public string firma3 { get; set; }
        public string nombre3 { get; set; }


        public Sample Sample { get; set; }

        #endregion

        public dynamic ObjForJson()
        {
            return new
                   {
                       this.observacionesIngresoMuestra,
                       this.ordenMuestreoEditable,
                       this.entrega1,
                       this.recibe1,
                       this.firma,
                       this.firma2,
                       this.firma3,
                       this.nombre,
                       this.nombre2,
                       this.nombre3
                   };
        }
    }
    
    [ComplexType]
    public class DeliveryData
    {
        public long fechaHora { get; set; }
        public string nombre { get; set; }
        [MaxLength]
        public string firma { get; set; }
    }
    
    [ComplexType]
    public class ResultClientReport
    {
        public string atencion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
    }
}