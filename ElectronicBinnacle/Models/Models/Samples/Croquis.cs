using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.Samples
{
    public class Croquis
    {
        #region Properties

        [Key]
        public int CroquisId { get; set; }
        public int Id_OrdenMuestreo { get; set; }
        public int id { get; set; }
        public int idImagen { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public float velocidad { get; set; }
        public float precision { get; set; }
        public long fecha { get; set; }
        [MaxLength]
        public string croquis { get; set; }

        public bool usoDispositivoAuxiliar { get; set; }
        [MaxLength]
        public string fotoDispositivoAuxiliar { get; set; }

        public Sample Sample { get; set; }

        #endregion
    }
}