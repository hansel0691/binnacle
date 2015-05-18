using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.Samples
{
    public enum SampleType
    {
        Simple,
        Compuesta
    }
    public enum WaterType
    {
        Residual,
        Salina,
        Potable,
        Subterránea
    }


    public class SimpleSample
    {
        #region Constructor

        public SimpleSample()
        {
            this.datosGeneralesMuestreo = new SamplingGeneralData();
            this.identificacionMuestraList = new List<SimpleSamplingIdentifier>();
        }

        #endregion
        #region Properties

        public int Id_OrdenMuestreo { get; set; }
        [Key, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SimpleSampleId { get; set; }
        public SampleType tipoMuestra { get; set; }
        public int idCroquis { get; set; }

        //[MaxLength]
        //public string mapa { get; set; }

        public string observaciones { get; set; }
        public SamplingGeneralData datosGeneralesMuestreo { get; set; }
        public List<SimpleSamplingIdentifier> identificacionMuestraList { get; set; }
        public ASWell pozoAS { get; set; }
        public SamplesQualityControl muestrasControlCalidad { get; set; }
        [Required]
        public List<ParamVerification> parametrosMuestraList { get; set; }


        [Key, ForeignKey("Sample"), Column(Order = 1)]
        public int SamplingId { get; set; }
        public Sample Sample { get; set; }

        #endregion
        #region Methods

        public dynamic ObjForJson()
        {
            return new
                   {
                       this.SimpleSampleId,
                       this.tipoMuestra,
                       this.idCroquis,
                       this.observaciones,
                       this.datosGeneralesMuestreo,
                       this.identificacionMuestraList,
                       this.pozoAS,
                       this.muestrasControlCalidad,
                       this.parametrosMuestraList
                   };
        }

        #endregion
    }

    public class ParamVerification
    {
        public int ParamVerificationId { get; set; }
        public int packageId { get; set; }
        public List<PackParamVerification> parameters { get; set; } 
    }

    public class PackParamVerification
    {
        public int PackParamVerificationId { get; set; }
        public int ParameterId { get; set; }
        public bool verificacion { get; set; }
    }

    [ComplexType]
    public class SamplingGeneralData
    {
        public long fechaFinal { get; set; }
        public long fechaInicial { get; set; }
        //public WaterType tipoAgua { get; set; }
    }

    [ComplexType]
    public class SampleMeasurement
    {
        public double valor0 { get; set; }
        public double valor1 { get; set; }
        public double valor2 { get; set; }
    }

    [ComplexType]
    public class ContainerNumber
    {
        public int O { get; set; }
        public int V { get; set; }
        public int P { get; set; }
        public int B { get; set; }
    }

    public class SimpleSamplingIdentifier
    {      
        public int SimpleSamplingIdentifierId { get; set; }
        public string muestraID { get; set; }
        public long hora { get; set; }
        public ContainerNumber numeroContenedores { get; set; }
        public int idCroquis { get; set; }  //es null cuando se hace referencia desde una muestra compueta.

        //[MaxLength]
        //public string mapa { get; set; }


        public double Cl2 { get; set; }
        public double O2 { get; set; }
        public bool? materiaFlotante { get; set; }
        public SampleMeasurement temperatura { get; set; }
        public SampleMeasurement pH { get; set; }
        public SampleMeasurement conductividadElectrica { get; set; }

        public double LabNo { get; set; }
        public double ReceivedAmount { get; set; }
       
    }

    [ComplexType]
    public class ASWell
    {
        public bool utiliza { get; set; }
        public double volumenTubo { get; set; }
        public double volumenFiltro { get; set; }
    }

    [ComplexType]
    public class SamplesQualityControl
    {
        public string IDMuestrasResguardo { get; set; }
        public string IDBcoDeViaje { get; set; }
        public string IDBcoDeEquipo { get; set; }
        public string IDBcoDeCampo { get; set; }
        public string muestrasDuplicadas { get; set; }
    }
    
}