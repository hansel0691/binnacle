using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.Samples
{
    [ComplexType]
    public class Header
    {
        #region Properties

        public int Id_OrdenMuestreo { get; set; }
        public long fechaRealizacion { get; set; }
        public bool cumplida { get; set; }
        public string motivoIncumplida { get; set; }
        public string observacionIncumplida { get; set; }

        #endregion
    }

}