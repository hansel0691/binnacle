using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.Samples
{
    [ComplexType]
    public class QualityControl
    {
        #region Constructor

        public QualityControl()
        {
            potencialREDOX = new Measurement();
            bufferPH10 = new Measurement();
            bufferPH4 = new Measurement();
            bufferPH7 = new Measurement();
            condElectricaDulce = new Measurement();
            condElectricaSalina = new Measurement();
        }

        #endregion
        #region Properties

        public int Id_OrdenMuestreo { get; set; }
        public string equipoUtilizado { get; set; }
        public float valorCorreccion { get; set; }
        public bool correccionTemp { get; set; }
        public Measurement potencialREDOX { get; set; }
        public Measurement bufferPH7 { get; set; }
        public Measurement bufferPH4 { get; set; }
        public Measurement condElectricaSalina { get; set; }
        public Measurement bufferPH10 { get; set; }
        public Measurement condElectricaDulce { get; set; }

        #endregion
    }

    [ComplexType]
    public class Measurement
    {
        #region Constructor

        public Measurement()
        {
            calibracionInicial = new ParameterMeasurement();
            calibracionFinal = new ParameterMeasurement();
        }

        #endregion
        #region Properties

        public ParameterMeasurement calibracionInicial { get; set; }
        public ParameterMeasurement calibracionFinal { get; set; }
        public string lote { get; set; }
        public string marca { get; set; }
        public string caducidad { get; set; }
        public bool isPresente { get; set; }

        #endregion
    }

    [ComplexType]
    public class ParameterMeasurement
    {
        public double VM { get; set; }
        public double VB { get; set; }
        public double temperatura { get; set; }
        public bool cumple { get; set; }
    }
}