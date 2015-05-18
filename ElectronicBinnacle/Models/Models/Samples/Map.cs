using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.Samples
{
    public class Map
    {
        [Key]
        public int MapId { get; set; }
        public int id { get; set; }
        public int Id_OrdenMuestreo { get; set; }
        [MaxLength]        
        public string mapa { get; set; }
    }
}