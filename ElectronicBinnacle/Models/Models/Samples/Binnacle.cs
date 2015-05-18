using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ElectronicBinnacle.Models.Context;
using ElectronicBinnacle.Models.Models.UserControl;

namespace ElectronicBinnacle.Models.Models.Samples
{
    [ComplexType]
    public class Binnacle
    {
        #region Constructor

        public Binnacle()
        {
            bitacora1 = new Binnacle1();
            bitacora2 = new Binnacle2();
        }

        #endregion
        #region Properties

        public int Id_OrdenMuestreo { get; set; }
        public Binnacle1 bitacora1 { get; set; }
        public Binnacle2 bitacora2 { get; set; }

        public long LastDate
        {
            get
            {
                using (var context = new MyContext())
                {
                    long date = 0;
                    if (context.SimpleSamplesCount(this.Id_OrdenMuestreo) > 0)
                    {
                        var samples = context.GetCleanSimpleSamples(this.Id_OrdenMuestreo);
                        if (samples != null)
                            date = samples.Max(s => s.datosGeneralesMuestreo.fechaFinal);
                    }
                    else
                    {
                        var samples = context.GetCleanComplexSamples(this.Id_OrdenMuestreo).ToList();
                        if (samples != null)
                            date = samples.Count() != 0 ? samples.Max(s => s.datosGeneralesMuestreo.fechaFinal) : -1;
                    }
                    return date;
                }
            }
        }

        public long FirstDate
        {
            get
            {
                using (var contex = new MyContext())
                {
                    //este metodo retorna la fecha de realizacion en que fue subida la primera order por el muestreador de la misma.
                    return contex.GetFirstSamplingDate(this.Id_OrdenMuestreo);
                }
            }
        }

        #endregion
    }

    [ComplexType]
    public class Binnacle1
    {
        #region Properties

        //public long fechaInicio { get; set; }
        //public long fechaTerminacion { get; set; }
        public string observaciones { get; set; }

        #endregion
    }
    [ComplexType]
    public class Binnacle2
    {
        #region Constructor

        public Binnacle2()
        {
            trasladoMuestraLab = new LabTransferSample();
        }

        #endregion
        #region Properties

        public string equipoUtilizado { get; set; }
        public string personalAuxiliar { get; set; }
        public int folio { get; set; }
        public string desarrolloMuestreo { get; set; }
        public LabTransferSample trasladoMuestraLab { get; set; }

        #endregion
    }
    [ComplexType]
    public class LabTransferSample
    {
        public string guiaNumero { get; set; }
        public string mensajeria { get; set; }
        public string tecnico { get; set; }
    }

}