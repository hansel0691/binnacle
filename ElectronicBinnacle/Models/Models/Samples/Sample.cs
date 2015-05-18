using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ElectronicBinnacle.Models.Models.Samples
{
    public class Sample
    {
        #region Constructor

        public Sample()
        {
            this.Header = new Header();
            this.QualityControl = new QualityControl();
            this.Binnacle = new Binnacle();
        }

        #endregion
        #region Properties

        public int SampleId { get; set; }
        public Header Header { get; set; }
        public QualityControl QualityControl { get; set; }
        public SamplingPlan SamplingPlan { get; set; }
        public List<SimpleSample> SimpleSamples { get; set; }
        public List<ComplexSample> ComplexSamples { get; set; }
        public SampleString SampleString { get; set; }
        public Binnacle Binnacle { get; set; }
        public List<Croquis> Croquises { get; set; }
        public List<Photo> Photos { get; set; }

        [ForeignKey("SampleId")]
        public SamplingOrder.SamplingOrder SamplingOrder { get; set; }


        [NotMapped]
        public int SimplesCount { get { return this.SimpleSamples == null ? 0 : this.SimpleSamples.Count; } }
        [NotMapped]
        public int ComplexCount { get { return this.ComplexSamples == null ? 0 : this.ComplexSamples.Count; } }
       

        #endregion
        #region Methods

        public dynamic ObjForJson(string view = "", int simpleCount = 0, int complexCount = 0)
        {
            if (view == "SamplingPlan")
                return new { this.SampleId, this.Croquises, this.QualityControl, SamplingPlan = this.SamplingPlan.ObjForJson(), this.Binnacle, SimpleCount = simpleCount, ComplexCount = complexCount };
            if (view == "SamplingString")
                return JsonForString();   
            return new
            {
                this.SampleId,
                this.Croquises,
                this.QualityControl,
                this.SamplingPlan,
                SimpleSample = this.SimpleSamples.FirstOrDefault(),
                ComplexSample = this.ComplexSamples.FirstOrDefault(),
                this.SampleString,
                this.Binnacle,
                this.SimplesCount,
                this.ComplexCount,
                SamplingOrder = this.SamplingOrder.ObjForJson()
            };
        }

        public dynamic JsonForString()
        {
            return
                new
                {
                    SampleString = this.SampleString == null ? new SampleString() : this.SampleString.ObjForJson(),
                    ComplexSamples = this.ComplexSamples.Select(cs => new { SampleId =  cs.ComplexSampleId ,SampleType = cs.tipoMuestra, Identifier = cs.muestraID ,GeneralData = cs.datosGeneralesMuestreo, SamplesIdentifier = cs.numeroMuestraList, ParamVerify = cs.parametrosMuestraList }),
                    SimpleSamples = this.SimpleSamples.Select(ss => new {SampleId = ss.SimpleSampleId, SampleType = ss.tipoMuestra,GeneralData = ss.datosGeneralesMuestreo, SamplesIdentifier = ss.identificacionMuestraList, ParamVerify = ss.parametrosMuestraList })
                };

        }

        #endregion
    }
}