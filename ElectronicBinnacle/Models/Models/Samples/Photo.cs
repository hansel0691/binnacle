using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.Samples
{
    public class Photo
    {
        public int Id_OrdenMuestreo { get; set; }
        public int PhotoId { get; set; }
        [MaxLength]
        public string foto { get; set; }
        public string titulo { get; set; }

        
        public Sample Sample { get; set; }

        public dynamic ObjForJson()
        {
            return new
                   {
                       this.foto,
                       this.titulo,
                       this.PhotoId
                   };
        }
    }
}