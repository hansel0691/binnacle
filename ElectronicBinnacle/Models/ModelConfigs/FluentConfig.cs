using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using ElectronicBinnacle.Models.Models.Samples;
using ElectronicBinnacle.Models.Models.SamplingOrder;
using ElectronicBinnacle.Models.Models.UserControl;
using Microsoft.Ajax.Utilities;

namespace ElectronicBinnacle.Models.ModelConfigs
{
    public class PackageConfig : EntityTypeConfiguration<Package>
    {
        public PackageConfig()
        {
            Property(p => p.Identifier).IsRequired();
            
            HasMany(p => p.Parameters).WithMany();
        }
    }
    public class WorkPackageConfig : EntityTypeConfiguration<WorkPackage>
    {
        public WorkPackageConfig()
        {
            HasMany(wp => wp.Packages).WithMany();//**
        }
    }
    public class ParamConfig : EntityTypeConfiguration<Parameter>
    {
        public ParamConfig()
        {
            Property(p => p.Identifier).IsRequired();
        }
    }
    public class SamplingOrderConfig : EntityTypeConfiguration<SamplingOrder>
    {
        public SamplingOrderConfig()
        {
            Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Ignore(p => p.SimplesCount);
            Ignore(p => p.ComplexCount);
            Ignore(p => p.PackagesCount);

            HasMany(o => o.WorkPackages).WithRequired(o => o.SamplingOrder);
            HasOptional(o => o.Sampler).WithMany();
            HasRequired(o => o.Creator).WithMany(u => u.CreatedOrders);
            HasOptional(o => o.DataInformation).WithRequired(di => di.SamplingOrder);
        }
    }

    public class SamplingConfig : EntityTypeConfiguration<Sample>
    {
        public SamplingConfig()
        {
            HasMany(s => s.SimpleSamples).WithRequired(ss => ss.Sample).HasForeignKey(s => s.SamplingId);
            HasMany(s => s.ComplexSamples).WithRequired(cs => cs.Sample).HasForeignKey(s => s.SamplingId);
            HasMany(s => s.Croquises).WithRequired(c => c.Sample);
            HasMany(s => s.Photos).WithRequired(p => p.Sample);
            HasOptional(s => s.SamplingPlan).WithRequired(s => s.Sample);
            HasOptional(s => s.SampleString).WithRequired(s => s.Sample);
        }
    }

    public class ComplexSamplingConfig : EntityTypeConfiguration<ComplexSample>
    {
        public ComplexSamplingConfig()
        {
            HasMany(c => c.parametrosMuestraList).WithRequired();
            HasMany(c => c.numeroMuestraList).WithRequired();
            //HasRequired(c => c.secuenciaCalculoObtenerMuestraCompuesta).WithRequiredDependent();
        }
    }
    

    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            Property(p => p.Name).IsRequired();
            
            HasMany(p => p.Permissions).WithRequired();
        }
    }
    public class PermissionConfig : EntityTypeConfiguration<Permission>
    {
        public PermissionConfig()
        {
            Property(p => p.Identifier).IsRequired();
            Property(p => p.Value).IsRequired();
        }
    }
    public class EmployeeConfig : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfig()
        {
            Property(e => e.Name).IsRequired();
            Property(e => e.LastName).IsRequired();
            //Property(e => e.Signature).IsRequired();

            HasRequired(e => e.User).WithRequiredPrincipal(u => u.Employee);
            HasRequired(e => e.Role).WithMany();
        }
    }
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            //Property(e => e.Name).IsRequired();
            //Property(e => e.Password).IsRequired();
            
            HasMany(u => u.Subordinates).WithOptional();
            HasMany(u => u.Notifications).WithRequired();
        }
    }
}