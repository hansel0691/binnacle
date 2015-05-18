using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.Samples
{
    public enum MeasurementFlow
    {
        SecciónVelocidad,
        VolumenTiempo,
        VertedorTriangular,
        VertedorRectangular,
        NoMedidorFlujo
    }

    public class ComplexSample
    {
        #region Constructor

        public ComplexSample()
        {
            datosGeneralesMuestreo = new SamplingGeneralData();
            muestrasControlCalidad = new SamplesQualityControl();
        }

        #endregion
        #region Properties

        public int Id_OrdenMuestreo { get; set; }
        [Key, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplexSampleId { get; set; }
        public SampleType tipoMuestra { get; set; }
        public int idCroquis { get; set; }

        //[MaxLength]
        //public string mapa { get; set; }


        public bool hayMedicionFlujo { get; set; }
        public string observaciones { get; set; }
        public MeasurementFlow tipoMetodoMedicionFlujo { get; set; }
        public string muestraID { get; set; }
        public SamplingGeneralData datosGeneralesMuestreo { get; set; }
        public ComplexSampleComputationSequence secuenciaCalculoObtenerMuestraCompuesta { get; set; }
        public List<SimpleSamplingIdentifier> numeroMuestraList { get; set; }
        public SamplesQualityControl muestrasControlCalidad { get; set; }
        public List<ParamVerification> parametrosMuestraList { get; set; }

        [Key, ForeignKey("Sample"), Column(Order = 1)]
        public int SamplingId { get; set; }
        public Sample Sample { get; set; }

        public int cuentaMedidorFlujo { get; set; }
        public string causaNoMedirFlujo { get; set; }


        #endregion
        #region Methods

        public dynamic ObjForJson()
        {
            return new
            {
                this.ComplexSampleId,
                this.tipoMuestra,
                this.idCroquis,
                this.observaciones,
                this.tipoMetodoMedicionFlujo,
                this.muestraID,
                this.datosGeneralesMuestreo,
                this.secuenciaCalculoObtenerMuestraCompuesta,
                this.numeroMuestraList,
                this.muestrasControlCalidad,
                this.parametrosMuestraList,
                this.hayMedicionFlujo,
                this.cuentaMedidorFlujo,
                this.causaNoMedirFlujo,
            };
        }

        #endregion
    }
    public class ComplexSampleComputationSequence
    {
        #region Properties

        public int ComplexSampleComputationSequenceId { get; set; }
        public double volumenTotalRequerido { get; set; }
        public List<IndividualVariable> variablesIndividualesList { get; set; }

        #endregion
    }
    public class IndividualVariable
    {
        #region Properties

        public int IndividualVariableId { get; set; }
        public double gasto { get; set; }
        public long hora { get; set; }


        #endregion
    }
}