using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.Samples
{
    public enum DirectedDownloadType
    {
        Drenaje,
        CuerpoRec,
        Riego
    }
    public enum ProtectionToolsType
    {
        Casco,
        Mascarilla,
        Lentes,
        ChalecoSalvavidas,
        Overall,
        Botas,
        GuantesCuero,
        Tyvex,
        GuantesLatex,
        GuantesNitrilo,
        Arnes
    }
    public enum SamplingPlaceType
    {
        Llave,
        Garrafon,
        Registro,
        Carcamo,
        Tubo,
        Noria,
        Lotico,
        Lentico,
        PozoMonitoreo,
        Estuario,
        LagunaCostera,
        Orilla,
        Costafuera,
        Otro,
    }
    public enum DownLoadFlow
    {
        Continuo,
        Intermitente
    }


    public class SamplingPlan
    {
        #region Constructor

        public SamplingPlan()
        {
            this.datosGenerales = new GeneralData();
        }

        #endregion
        #region Properties

        public int Id_OrdenMuestreo { get; set; }
        [ForeignKey("Sample")]
        public int SamplingPlanId { get; set; }
        public GeneralData datosGenerales { get; set; }
        public string estrategiaMuestreo { get; set; }
        public DirectedDownloadType descargaDirigida { get; set; }
        public int idCroquis { get; set; }

        //[MaxLength]
        //public string mapa { get; set; }

        public int horasDiaTiempoOperacionDescarga { get; set; }
        public int diasSemanaTiempoOperacionDescarga { get; set; }
        public List<SamplingPlaceKind> tipoSitioMuestreoList { get; set; }
        public DownLoadFlow flujoDescarga { get; set; }
        public List<ProtectionTool> equipoProteccionList { get; set; }

        public int SamplingId { get; set; }
        public Sample Sample { get; set; }

        #endregion

        public dynamic ObjForJson()
        {
            return new
                   {
                       this.datosGenerales,
                       this.estrategiaMuestreo,
                       this.descargaDirigida,
                       this.idCroquis,
                       this.horasDiaTiempoOperacionDescarga,
                       this.diasSemanaTiempoOperacionDescarga,
                       this.tipoSitioMuestreoList,
                       this.flujoDescarga,
                       this.equipoProteccionList,
                   };
        }
    }

    [ComplexType]
    public class GeneralData
    {
        #region Properties

        public long fecha { get; set; }

        #endregion
    }

    [ComplexType]
    public class PlaceDimensions
    {
        #region Properties

        public double ancho { get; set; }
        public double diametro { get; set; }
        public double profundidad { get; set; }
        public double largo { get; set; }

        #endregion
    }

    public class ProtectionTool
    {
        #region Properties

        public int ProtectionToolId { get; set; }
        public ProtectionToolsType tipo { get; set; }

        #endregion
    }

    public class SamplingPlaceKind
    {
        public int SamplingPlaceKindId { get; set; }
        public SamplingPlaceType tipoSitio { get; set; }
        public string otroSitio { get; set; }
    }
}