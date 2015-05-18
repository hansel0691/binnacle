using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ElectronicBinnacle.Models.Context;
using ElectronicBinnacle.Models.Models.Samples;
using ElectronicBinnacle.Models.Models.UserControl;
using System.Data.Entity;

namespace ElectronicBinnacle.Models.Models.SamplingOrder
{
    public enum OrderState
    {
        NotSended,
        Sended,
        NotEvaluated,
        Evaluated,
        NotFinished,
        Unredeemed 
    }
    public enum SamplingState
    {
        None,
        Correct,
        Incorrect,
    }

    public class SamplingOrder
    {
        #region Properties

        public int Id { get; set; }
        public ClientData ClientData { get; set; }
        public ClientData BillerClient { get; set; }
        public ClientData BinnacleData { get; set; }
        public LocationData LocationData { get; set; }
        public SamplingData SamplingData { get; set; }
        public OrderState OrderState { get; set; }
        public SamplingState SamplingState { get; set; }
        public  List<WorkPackage> WorkPackages { get; set; }
        public  Employee Sampler { get; set; }
        public  User Creator { get; set; }
        public Sample DataInformation { get; set; }

        //not Mapped
        public int SimplesCount
        {
            get
            {
                return this.WorkPackages == null ? 0 : this.WorkPackages.Where(package => package.Type == SampleKind.Simple).Sum(package => package.SamplesNumber);
            }
        }
        public int ComplexCount
        {
            get
            {
                return this.WorkPackages == null ? 0 : this.WorkPackages.Where(package => package.Type == SampleKind.Complex).Sum(package => package.SamplesNumber);
            }
        }
        public int PackagesCount { get { return this.WorkPackages == null ? 0 : this.WorkPackages.Count; } }

        #endregion
        #region Methods

        public dynamic ObjForJson(string view = "")
        {
            if (view == "Index") return JsonForIndex();
            if (view == "Sample") return JsonForSample();
            if (view == "Orders") return this.OrderState == OrderState.NotEvaluated || this.OrderState == OrderState.Evaluated || this.OrderState == OrderState.Unredeemed ? JsonForObject() : StandardJson();
            return StandardJson();
        }

        private dynamic JsonForIndex()
        {
            return new
            {
                this.Id,
                ClientData = new
                {
                    SocialReason = this.ClientData.SocialReason ?? "",
                    RFC = this.ClientData.RFC ?? "",
                    this.ClientData.BillReport
                },
                BillerClient = new
                {
                    SocialReason = this.BillerClient.SocialReason ?? "",
                    RFC = this.BillerClient.RFC ?? ""
                },
                LocationData = new
                {
                    Place = this.LocationData.Place ?? "",
                    DelMpio = this.LocationData.DelMpio ?? "",
                    Edo = this.LocationData.Edo ?? "",
                },
                this.SamplingData,
                this.OrderState,
                this.SamplingState,
                WorkPackages = this.WorkPackages != null ? this.WorkPackages.Select(wp => wp.ObjForJson()) : null,
                this.PackagesCount,
                this.SimplesCount,
                this.ComplexCount,
                this.BinnacleData,
                Sampler = new
                {
                    EmployeeId = this.Sampler != null ? this.Sampler.EmployeeId : 0,
                    Name = this.Sampler != null ? this.Sampler.Name : "",
                    LastName = this.Sampler != null ? this.Sampler.LastName : ""
                },
                Creator = new
                {
                    this.Creator.UserId,
                    this.Creator.Name
                }
            };
        }
        private dynamic JsonForSample()
        {
            return new
                   {
                       this.Id,
                       BinnacleId = this.Sampler.User.BinnacleIdentifier,
                       this.LocationData,
                       this.SamplingData,
                       WorkGroups = this.WorkPackages.Select(wp => new { wp.SamplesNumber, wp.Type, wp.Period, Group = new { wp.Packages } }),
                       Creator = new
                                 {
                                     this.Creator.Employee.Name,
                                     this.Creator.Employee.LastName,
                                     this.Creator.Employee.PhoneNumber
                                 },
                       Format = "Formato de Texto Fijo",
                       ResponsibleData = new
                                         {
                                             this.Sampler.Name,
                                             this.Sampler.User.Job,
                                             this.Sampler.User.Category,
                                             this.Sampler.User.Subsidiary,
                                             CalibrationName = this.Sampler.User.CalibrationKit.Name,
                                             CalibrationSeries = this.Sampler.User.CalibrationKit.Series,
                                             CalibrationModel = this.Sampler.User.CalibrationKit.Model,
                                         },
                   };
        }
        private dynamic JsonForObject()
        {
            Header header = null;
            using (var context = new MyContext())
                header = context.Orders.Include(o => o.DataInformation).First(o => o.Id == this.Id).DataInformation.Header;
            return new
            {
                this.Id,
                this.ClientData,
                this.BillerClient,
                this.LocationData,
                this.SamplingData,
                this.OrderState,
                this.SamplingState,
                WorkPackages = this.WorkPackages != null ? this.WorkPackages.Select(wp => wp.ObjForJson()) : null,
                this.PackagesCount,
                this.BinnacleData,
                this.SimplesCount,
                this.ComplexCount,
                DataInformation = new
                {
                    Header = new
                    {
                        fechaRealizacion = header.fechaRealizacion,
                        motivoIncumplida = header.motivoIncumplida,
                        observacionIncumplida = header.observacionIncumplida
                    }
                },
                Sampler = this.Sampler != null ? Sampler.ObjForJson() : null,
                Creator = this.Creator != null ? this.Creator.ObjForJson() : null
            };
        }
        private dynamic StandardJson()
        {
            return new
            {
                this.Id,
                this.ClientData,
                this.BillerClient,
                this.LocationData,
                this.SamplingData,
                this.OrderState,
                this.SamplingState,
                WorkPackages = this.WorkPackages != null ? this.WorkPackages.Select(wp => wp.ObjForJson()) : null,
                this.PackagesCount,
                this.BinnacleData,
                this.SimplesCount,
                this.ComplexCount,
                Sampler = this.Sampler != null ? Sampler.ObjForJson() : null,
                Creator = this.Creator != null ? this.Creator.ObjForJson() : null
            };
        }
        public dynamic JsonForSamplingOrder()
        {
            return new
            {
                this.Id,
                this.ClientData,
                this.BillerClient,
                this.LocationData,
                this.SamplingData,
                this.OrderState,
                this.SamplingState,
                WorkPackages = this.WorkPackages.Select(wp => new { Packages = wp.Packages.Select(p => new { p.PackageId, p.Identifier, p.Standard, Parameters = p.Parameters }) }),
                this.PackagesCount,
                this.BinnacleData,
                this.SimplesCount,
                this.ComplexCount,
                Sampler = this.Sampler != null ? Sampler.ObjForJson() : null,
                Creator = this.Creator != null ? this.Creator.ObjForJson() : null
            };
        }

        #endregion

        public dynamic JsonForStats()
        {
            long finished = 0;
            if (this.OrderState == OrderState.Evaluated || this.OrderState == OrderState.Unredeemed || this.OrderState == OrderState.NotEvaluated)
                using (var context = new MyContext())
                    finished = context.Samples.FirstOrDefault(s => s.SampleId == this.Id).Header.fechaRealizacion;
            return new
                   {
                       this.Id,
                       this.SamplingData.Identifier,
                       this.SamplingData.EndTime,
                       this.OrderState,
                       Finished = finished
                   };
        }
    }
}