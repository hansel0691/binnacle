using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using ElectronicBinnacle.Models.ModelConfigs;
using ElectronicBinnacle.Models.Models;
using ElectronicBinnacle.Models.Models.Samples;
using ElectronicBinnacle.Models.Models.SamplingOrder;
using ElectronicBinnacle.Models.Models.UserControl;
using WebMatrix.WebData;
using User = ElectronicBinnacle.Models.Models.UserControl.User;

namespace ElectronicBinnacle.Models.Context
{
    public class MyContext : DbContext
    {
        #region Constructor 

        public MyContext()
            : base("DefaultConnection")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MyDbContextInitializer());
//            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");


            modelBuilder.Configurations.Add(new PackageConfig());
            modelBuilder.Configurations.Add(new WorkPackageConfig());
            modelBuilder.Configurations.Add(new ParamConfig());
            modelBuilder.Configurations.Add(new SamplingOrderConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new PermissionConfig());
            modelBuilder.Configurations.Add(new EmployeeConfig());
            modelBuilder.Configurations.Add(new UserConfig());
        }

        #endregion
        #region Properties

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Parameter> Params { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<SamplingOrder> Orders { get; set; }
        public DbSet<WorkPackage> WorkPackages { get; set; }
        
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Croquis> Croquiss { get; set; }
        public DbSet<SamplingPlan> SamplingPlans { get; set; }
        public DbSet<SimpleSample> SimpleSamples { get; set; }
        public DbSet<ComplexSample> ComplexSamples { get; set; }
        public DbSet<SampleString> SampleStrings { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Position> Positions { get; set; }

        public DbSet<Map> Maps { get; set; }

        #endregion
        #region Methods

        public IEnumerable<Employee> AllEmployees(Employee search = null)
        {
            var employees = this.Employees
                .Include(e => e.User.WatterTypes)
                .Include(e => e.Role.Permissions);
            if (search != null)
            {
                var activeEmployees = employees.Where(e => e.DropDown == search.DropDown);
                return !string.IsNullOrEmpty(search.Name) ? activeEmployees.Where(e => e.Name.Contains(search.Name) || e.LastName.Contains(search.Name)) : activeEmployees;
            }
            return employees;
        }
        public Employee GetEmployee(int employeeId)
        {
            return this.AllEmployees().FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public Employee GetCleanEmployee(int employeeId)
        {
            return this.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);            
        }
        public Employee AddEmployee(Employee employee)
        {
            employee.Role = this.GetCleanRole(employee.Role.RoleId);
            var added = this.Employees.Add(employee);
            if (added != null)
                this.SaveChanges();
            return added;
        }
        public bool RemoveEmployee(int employeeId)
        {
            var removed = Employees.Remove(Employees.Find(employeeId));
            var result = removed != null;
            if (result)
                SaveChanges();
            return result;
        }
        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                if (employee.EmployeeId == 0) return false;
                var emp = this.GetEmployee(employee.EmployeeId);
                emp.CopyProps(employee);
                emp.Role = this.GetCleanRole(employee.Role.RoleId);
                this.UpdateUser(employee.User, true);
                this.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public IEnumerable<Role> AllRoles()
        {
            return this.Roles.Include(r => r.Permissions);
        }
        public IEnumerable<Role> AllRoles(Role search)
        {
            var roles = this.Roles
                .Include(r => r.Permissions)
                .Where(r => r.Active == search.Active);
            return string.IsNullOrEmpty(search.Name) ? roles : roles.Where(r => r.Name.Contains(search.Name));
        }
        public Role GetRole(int roleId)
        {
            return this.AllRoles().FirstOrDefault(r => r.RoleId == roleId);
        }
        public Role GetCleanRole(int roleId)
        {
            return this.Roles.FirstOrDefault(r => r.RoleId == roleId);
        }
        public Role AddRole(Role role)
        {
            try
            {
                var added = this.Roles.Add(role);
                if (added != null)
                    this.SaveChanges();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool RemoveRole(int roleId)
        {
            try
            {
                var toDelete = Roles.Find(roleId);
                var removed = Roles.Remove(toDelete);
                var result = removed != null;
                if (result)
                    SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateRole(Role role)
        {
            try
            {
                if (role.RoleId == 0) return false;
                var r = this.GetRole(role.RoleId);
                //remove all the permissions before the copy.
                var permissionToDelete = r.Permissions.Select(p => p.PermissionId).ToList();
                foreach (var id in permissionToDelete)
                    this.Permissions.Remove(Permissions.Find(id));

                r.CopyProps(role);
                this.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<User> AllUsers(User seach = null)
        {
            var users = this.Users
                .Include(u => u.Subordinates.Select(e => e.User.WatterTypes))
                .Include(u => u.CreatedOrders)
//                .Include(u => u.CreatedOrders.Select(o => o.WorkPackages.Select(wp => wp.Packages.Select(p => p.Parameters))))//**
                .Include(u => u.Employee.Role.Permissions);
            return seach != null && !string.IsNullOrEmpty(seach.Name) ? users.Where(u => u.Name.Contains(seach.Name)) : users;
        }
        public User GetUser(int userId)
        {
            return this.AllUsers().FirstOrDefault(u => u.UserId == userId);
        }
        public User GetSubordinatesUser(int userId)
        {
            return this.Users
                .Include(u => u.Subordinates)
                .Include(u => u.Employee.Role.Permissions)
                .FirstOrDefault(u => u.UserId == userId);
        }
        public User GetEmployeeUser(int userId)
        {
            return this.Users
                .Include(u => u.Employee.Role.Permissions)
                .FirstOrDefault(u => u.UserId == userId);
        }
        public User GetOrdersUser(int userId, bool withWorkPacks)
        {
            var user = this.Users
                .Include(u => u.Subordinates)
                .Include(u => u.Employee.Role.Permissions)
                .First(u => u.UserId == userId);
            
            user.CreatedOrders = new List<SamplingOrder>(this.Orders.Where(o => o.Creator.UserId == userId));
            return user;
        }
        public User GetActiveUser(string userName)
        {
            return this.AllUsers().FirstOrDefault(u => !u.Employee.DropDown && u.Name == userName);
        }
        public User AddUser(User user)
        {
            var added = this.Users.Add(user);
            if (added != null)
                this.SaveChanges();
            return added;
        }
        public bool RemoveUser(int userId)
        {
            var removed = Users.Remove(Users.Find(userId));
            var result = removed != null;
            if (result)
                SaveChanges();
            return result;
        }
        public bool UpdateUser(User user, bool fromEmployee = false)
        {
            try
            {
                if (user.UserId == 0) return false;
                var u = this.GetUser(user.UserId);
                u.CopyProps(user, fromEmployee);
                this.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Parameter> AllParameters(Parameter search = null)
        {
            var parameters = this.Params.AsEnumerable();
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.Identifier))
                    parameters = parameters.Where(p => p.Identifier.ToLower().Contains(search.Identifier.ToLower()));
                if (!string.IsNullOrEmpty(search.Preserver))
                    parameters = parameters.Where(p => p.Preserver.ToLower().Contains(search.Preserver.ToLower()));
                if (!string.IsNullOrEmpty(search.Container))
                    parameters = parameters.Where(p => p.Container.ToLower().Contains(search.Container.ToLower()));
                if (search.Volume != -1)
                    parameters = parameters.Where(p => p.Volume == search.Volume);
                if (search.TMPA != -1)
                    parameters = parameters.Where(p => p.TMPA == search.TMPA);
            }
            return parameters;
        }
        public Parameter GetParam(int parameterId)
        {
            return this.Params.FirstOrDefault(p => p.ParameterId == parameterId);
        }
        public Parameter AddParam(Parameter param)
        {
            try
            {
                var added = this.Params.Add(param);
                if (added != null)
                    this.SaveChanges();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool RemoveParam(int paramId)
        {
            try
            {
                var removed = Params.Remove(Params.Find(paramId));
                var result = removed != null;
                if (result)
                    SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateParam(Parameter parameter)
        {
            try
            {
                if (parameter.ParameterId == 0) return false;
                var param = this.GetParam(parameter.ParameterId);
                param.CopyProps(parameter);
                this.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Package> AllPackages(Package pack = null)
        {
            var packages = this.Packages.Include(p => p.Parameters);
            if (pack != null && !string.IsNullOrEmpty(pack.Identifier))
                packages = packages.Where(p => p.Identifier.ToLower().Contains(pack.Identifier.ToLower()));
            return packages;
        }
        public Package GetPackage(int packageId)
        {
            return this.AllPackages().FirstOrDefault(p => p.PackageId == packageId);

        }
        public Package AddPackage(Package package)
        {
            try
            {
                var toAdd = new Package();
                this.CopyPack(toAdd, package);
                var added = this.Packages.Add(toAdd);
                if (added != null)
                    this.SaveChanges();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool RemovePackage(int packageId)
        {
            try
            {
                var toRemove = this.GetPackage(packageId);
                if (toRemove.Parameters != null)
                    toRemove.Parameters.Clear();
                var removed = Packages.Remove(toRemove);
                var result = removed != null;
                if (result)
                    SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdatePackage(Package package)
        {
            try
            {
                if (package.PackageId == 0) return false;
                var pack = this.GetPackage(package.PackageId);
                this.CopyPack(pack, package);
                this.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<SamplingOrder> AllOrders()
        {
            var a = this.Orders
                .Include(o => o.Creator.Employee)
                .Include(o => o.Sampler.User)
                .Include(o => o.WorkPackages.Select(wp => wp.Packages.Select(p => p.Parameters))).ToList();//**
            return a;
        }
        public SamplingOrder GetOrder(int orderId)
        {
            return this.AllOrders().FirstOrDefault(o => o.Id == orderId);
        }
        public SamplingOrder AddOrder(SamplingOrder order)
        {
            if (order.Id == 0)
            {
                var maxId = !this.Orders.Any() ? 0 : this.Orders.Max(o => o.Id);
                order.Id = maxId + 1;
            }
            var added = this.Orders.Add(order);
            if (added != null)
                this.SaveChanges();
            return added;
        }
        public bool RemoveOrder(int orderId)
        {
            try
            {
                if (this.Samples.Any(s => s.SampleId == orderId))
                    RemoveSamplingInfo(orderId);
                var removed = Orders.Remove(Orders.Find(orderId));
                var result = removed != null;
                if (result)
                    SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateOrder(SamplingOrder order)
        {
            try
            {
                this.RemoveOrder(order.Id);
                var result = this.AddOrder(order) != null;
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Sample GetSamplingInfo(int orderId)
        {
            return this.Samples
                .Include(sd => sd.SampleString)
                .Include(sd => sd.SamplingOrder.Creator.Employee).Include(sd => sd.SamplingOrder.Sampler.User)
                .Include(sd => sd.SamplingPlan.equipoProteccionList)
                .Include(sd => sd.SamplingPlan.tipoSitioMuestreoList)
                .Include(sd => sd.SimpleSamples.Select(ss => ss.identificacionMuestraList))
                .Include(sd => sd.SimpleSamples.Select(ss => ss.parametrosMuestraList.Select(pm => pm.parameters)))
                .Include(sd => sd.ComplexSamples.Select(cs => cs.numeroMuestraList))
                .Include(sd => sd.ComplexSamples.Select(cs => cs.parametrosMuestraList.Select(pm => pm.parameters)))
                .Include(sd => sd.ComplexSamples.Select(cs => cs.secuenciaCalculoObtenerMuestraCompuesta.variablesIndividualesList))
                .FirstOrDefault(sd => sd.SampleId == orderId);

        }
        public Sample GetCleanSamplingInfo(int orderId)
        {
            return this.Samples
                //.Include(sd => sd.SamplingOrder.Creator.Employee).Include(sd => sd.SamplingOrder.Sampler.User)
                .Include(sd => sd.SamplingOrder.Sampler.User)
                .FirstOrDefault(sd => sd.SampleId == orderId);
        }
        public SamplingOrder GetSampleOrder(int id)
        {
            return this.Orders
                .Include(r => r.WorkPackages.Select(wp => wp.Packages.Select(p => p.Parameters)))
                .Include(r => r.Sampler.User)
                .Include(r => r.Creator.Employee)
                .FirstOrDefault(r => r.Id == id);
        }
        public Sample GetSamplingPlan(int id)
        {
            var sample = this.Samples
                //.Include(sd => sd.SamplingOrder.Sampler.User)
                .Include(sd => sd.SamplingPlan.equipoProteccionList)
                .Include(sd => sd.SamplingPlan.tipoSitioMuestreoList)
                .FirstOrDefault(s => s.SampleId == id);
            return sample;
        }
        public Sample GetString(int id)
        {
            var sample = this.Samples
                .Include(s => s.SimpleSamples.Select(ss => ss.identificacionMuestraList))
                .Include(s => s.SimpleSamples.Select(ss => ss.parametrosMuestraList.Select(pm => pm.parameters)))
                .Include(c => c.ComplexSamples.Select(cs => cs.numeroMuestraList))
                .Include(c => c.ComplexSamples.Select(cs => cs.secuenciaCalculoObtenerMuestraCompuesta.variablesIndividualesList))
                .Include(c => c.ComplexSamples.Select(cs => cs.parametrosMuestraList.Select(pm => pm.parameters)))
                .Include(sd => sd.SampleString)
                .FirstOrDefault(s => s.SampleId == id);
            return sample ?? null;
        }
        public List<SimpleSample> GetSimpleSamples(int orderId)
        {
            var sample = this.Samples
                .Include(s => s.SimpleSamples.Select(ss => ss.identificacionMuestraList))
                .Include(s => s.SimpleSamples.Select(ss => ss.parametrosMuestraList.Select(pm => pm.parameters)))
                .FirstOrDefault(s => s.SampleId == orderId);
            return sample != null ? sample.SimpleSamples : null;
        }
        public List<ComplexSample> GetComplexSamples(int orderId)
        {
            var samples = this.ComplexSamples
                .Include(c => c.numeroMuestraList)
                .Include(c => c.secuenciaCalculoObtenerMuestraCompuesta.variablesIndividualesList)
                .Include(c => c.parametrosMuestraList.Select(pm => pm.parameters))
                .Where(s => s.SamplingId == orderId).ToList();
            return samples;
        }
        public bool RemoveSamplingInfo(int id)
        {
            var samplePlan = this.GetSamplingPlan(id);
            if (samplePlan != null && samplePlan.SamplingPlan != null)
                this.SamplingPlans.Remove(samplePlan.SamplingPlan);

            var sampleString = this.GetString(id);
            if (sampleString != null && sampleString.SampleString != null)
                this.SampleStrings.Remove(sampleString.SampleString);

            var simples = this.GetSimpleSamples(id).ToList();
            foreach (var simpleSample in simples)
                this.SimpleSamples.Remove(simpleSample);

            var complexs = this.GetComplexSamples(id).ToList();
            foreach (var complexSample in complexs)
                this.ComplexSamples.Remove(complexSample);

            var photos = this.GetPhotos(id).ToList();
            foreach (var photo in photos)
                this.Photos.Remove(photo);

            var croquiss = this.Croquiss.Where(c => c.Id_OrdenMuestreo == id).ToList();
            foreach (var croquis in croquiss)
                this.Croquiss.Remove(croquis);

            var maps = this.Maps.Where(m => m.Id_OrdenMuestreo == id).ToList();
            foreach (var map in maps)
                this.Maps.Remove(map);

            var removed  = this.Samples.Remove(this.Samples.FirstOrDefault(s => s.SampleId == id));
            var result = removed != null;
            if (result)
                SaveChanges();
            return result;
        }
        public int SimpleSamplesCount(int samplingId)
        {
            if (samplingId == 0) return 0;
            return this.Samples.Include(s => s.SimpleSamples).First(s => s.SampleId == samplingId).SimpleSamples.Count;
        }
        public int ComplexSamplesCount(int samplingId)
        {
            return this.Samples.Include(s => s.ComplexSamples).First(s => s.SampleId == samplingId).ComplexSamples.Count;
        }


        public void AddCroquis(Sample samplingData, Croquis croquis)
        {
            croquis.Sample = samplingData;
            
            //de ahora en adelante asumo que si se cae la conxion el dato no se guarda en la bd. CHECK!!!!???
            //var croquiss = this.Croquiss.Where(c => c.Id_OrdenMuestreo == croquis.Id_OrdenMuestreo).ToList();
            //foreach (var c in croquiss)
            //    this.Croquiss.Remove(c);

            this.Croquiss.Add(croquis);
            this.SaveChanges();
        }
        public void AddMap(Map map)
        {
            this.Maps.Add(map);
            foreach (var map1 in this.Maps)
            {
                
            }
            this.SaveChanges();
        }

        public void SetQualityControl(int sampleId, QualityControl qualityControl)
        {
            //ver si ya existe alguno con este id borrarlo

            var sample = this.Samples
                .Include(s => s.Croquises)
                .Include(s => s.SamplingPlan)
                .Include(s => s.SimpleSamples)
                .Include(s => s.ComplexSamples)
                .Include(s => s.SampleString)
                .Include(s => s.Photos)
                .FirstOrDefault(s => s.SampleId == sampleId);
            sample.QualityControl = qualityControl;
            this.SaveChanges();
        }

        public void SetSamplingPlan(SamplingPlan samplingPlan, Sample samplingData)
        {
            samplingPlan.Sample = samplingData;
            
            var samplePlan = this.GetSamplingPlan(samplingPlan.Id_OrdenMuestreo);
            if (samplePlan != null && samplePlan.SamplingPlan != null)
                this.SamplingPlans.Remove(samplePlan.SamplingPlan);

            this.SamplingPlans.Add(samplingPlan);
            this.SaveChanges();
        }

        public void AddSimpleSample(Sample samplingData, SimpleSample simpleSample)
        {
            simpleSample.Sample = samplingData;

            //var simples = this.GetSimpleSamples(simpleSample.Id_OrdenMuestreo).ToList();
            //foreach (var ss in simples)
            //    this.SimpleSamples.Remove(ss);

            simpleSample.SamplingId = samplingData.SampleId;
            this.SimpleSamples.Add(simpleSample);
            this.SaveChanges();
        }

        public void AddComplexSample(Sample samplingData, ComplexSample complexSample)
        {
            complexSample.Sample = samplingData;

            //var complexs = this.GetComplexSamples(complexSample.Id_OrdenMuestreo).ToList();
            //foreach (var cs in complexs)
            //    this.ComplexSamples.Remove(cs);

            complexSample.SamplingId = samplingData.SampleId;
            this.ComplexSamples.Add(complexSample);
            this.SaveChanges();
        }

        public void SetSampleString(Sample samplingData, SampleString sampleString)
        {
            sampleString.Sample = samplingData;

            //var sString = this.GetString(sampleString.Id_OrdenMuestreo);
            //if (sString != null && sString.SampleString != null)
            //    this.SampleStrings.Remove(sString.SampleString);

            this.SampleStrings.Add(sampleString);
            this.SaveChanges();
        }

        public void SetBinnacle(int sampleId, Binnacle binnacle)
        {
            //ver si esta borarlo

            var sample = this.Samples
                .Include(s => s.Croquises)
                .Include(s => s.SamplingPlan)
                .Include(s => s.SimpleSamples)
                .Include(s => s.ComplexSamples)
                .Include(s => s.SampleString)
                .Include(s => s.Photos)
                .FirstOrDefault(s => s.SampleId == sampleId);
            
            sample.Binnacle = binnacle;
            var order = GetOrder(sampleId);
            if (order.OrderState == OrderState.Sended || order.OrderState == OrderState.NotSended || order.OrderState == OrderState.Evaluated)
            {
                order.OrderState = OrderState.NotEvaluated;
                order.SamplingState = SamplingState.None;
            }
            this.SaveChanges();
        }

        public void AddPhoto(Sample samplingData, Photo photo)
        {
            photo.Sample = samplingData;

            //var photos = this.GetPhotos(photo.Id_OrdenMuestreo).ToList();
            //foreach (var ph in photos)
            //    this.Photos.Remove(ph);

            this.Photos.Add(photo);
            this.SaveChanges();
        }

        public IEnumerable<Photo> GetPhotos(int id)
        {
            return this.Photos.Where(p => p.Id_OrdenMuestreo == id);
        }

        public Croquis GetCroquis(int sampleId, int croquisId)
        {
            if (sampleId == 0) return new Croquis();
            var croquis = this.Croquiss.FirstOrDefault(c => c.Id_OrdenMuestreo == sampleId && c.id == croquisId);
            if (croquis != null && String.IsNullOrEmpty(croquis.croquis))
            {
                var firstOrDefault = this.Croquiss.FirstOrDefault(c => c.Id_OrdenMuestreo == sampleId && c.idImagen == croquis.idImagen);
                if (firstOrDefault != null)
                    croquis.croquis = firstOrDefault.croquis;
            }
            return croquis;
        }

        public IEnumerable<SimpleSample> GetCleanSimpleSamples(int sampleId)
        {
            if (sampleId == 0) return new List<SimpleSample>();
            return this.Samples.Include(s => s.SimpleSamples).First(s => s.SampleId == sampleId).SimpleSamples;
        }
        public IEnumerable<ComplexSample> GetCleanComplexSamples(int sampleId)
        {
            if (sampleId == 0) return new List<ComplexSample>();
            return this.Samples.Include(s => s.ComplexSamples).First(s => s.SampleId == sampleId).ComplexSamples;
        }

        public long GetFirstSamplingDate(int orderId)
        {
            var order = this.Orders.Include(o => o.Sampler).FirstOrDefault(o => o.Id == orderId);
            if (order == null) return 0;
            var samplerId = order.Sampler.EmployeeId;
            return this.Samples.Include(s => s.SamplingOrder).Where(s => s.SamplingOrder.Sampler.EmployeeId == samplerId).Min(s => s.Header.fechaRealizacion);

        }

        public IEnumerable<Notification> GetUserNotifications(int userId, string notificationText, int samplerId, int type, long date, int page = 0)
        {
            var user = this.Users.Include(u => u.Notifications).FirstOrDefault(u => u.UserId == userId);

            IEnumerable<Notification> notifications = user != null ? user.Notifications.Select(n => n.Clone()).ToList() : new List<Notification>();
            if (!string.IsNullOrWhiteSpace(notificationText))
            {
                var words = notificationText.Split(' ', '.', ',', ':');
                notifications = words.Aggregate(notifications, (current, word) => current.Where(n => n.NOTIFICATION_MSG.ToLower().Contains(word.ToLower())));
            }
            if (samplerId != 0)
                notifications = notifications.Where(n => n.SamplerName == this.Employees.First(e => e.EmployeeId == samplerId).FullName);
            if (type != 0)
                notifications = notifications.Where(n => n.NOTIFICATION_TYPE == (NotificationType) type);
            if (date != -1 && date != 0)
                notifications = notifications.Where(n => n.DATETIME >= date  && n.DATETIME <= date + 24*60*60*1000);
            notifications = notifications.Select(n => n.Clone()).ToList();
            if (page != 0)
                notifications = notifications.OrderBy(n => n.NotificationId * -1).Skip((page - 1) * 30).Take(30);

            foreach (var notification in notifications.Where(n => !n.Old))
                user.Notifications.Find(n => n.NotificationId == notification.NotificationId).Old = true; //para mejorar la rapidez today < n.DATETIME && n.DATETIME < tomorrow
            this.SaveChanges();
            return notifications;
        }
        private void CopyPack(Package pack1, Package pack2)
        {
            pack1.CopyProps(pack2);
            if (pack1.Parameters == null)
                pack1.Parameters = new List<Parameter>();
            else
                pack1.Parameters.Clear();

            if (pack2.Parameters != null)
                pack1.Parameters.AddRange(pack2.Parameters.Select(p => this.GetParam(p.ParameterId)));
        }

        #endregion

        public IEnumerable<Position> GetUsersPositions(int boosId)
        {
            var boos = this.Users.Include(u => u.Subordinates.Select(s => s.Positions)).FirstOrDefault(u => u.UserId == boosId);
            foreach (var subordinate in boos.Subordinates)
            {
                var unixToday = DateTime.Today.GetUnixEpoch();
                var todayPositions = subordinate.Positions.Where(p => p.DateTime > unixToday && p.DateTime <= unixToday + 24 * 60 * 60 * 1000).OrderBy(p => p.PositionId).ToList();
                if (todayPositions.Any())
                {
                    var lastPosition = todayPositions.Last();
                    yield return lastPosition;
                }
            }
            yield break;
        }
        public IEnumerable<Position> GetUserPositions(int employeeId)
        {
            var unixToday = DateTime.Today.GetUnixEpoch();
            var employeePositions = this.Positions.Where(p => p.Employee.EmployeeId == employeeId && p.DateTime > unixToday && p.DateTime <= unixToday + 24 * 60 * 60 * 1000);
            return employeePositions;
        }
        public Position GetUserPosition(int employeeId)
        {
            var unixToday = DateTime.Today.GetUnixEpoch();
            var employeePositions = this.Positions.Where(p => p.Employee.EmployeeId == employeeId && p.DateTime > unixToday && p.DateTime <= unixToday + 24 * 60 * 60 * 1000);
            Position currentPosition = null;
            if (employeePositions.Any())
                currentPosition = employeePositions.OrderBy(p => p.PositionId).LastOrDefault();
            return currentPosition;
        }
    }

    public class MyDbContextInitializer : DropCreateDatabaseIfModelChanges <MyContext>
    {
        protected override void Seed(MyContext context)
        {
            var admin = new Role {Active = true, Name = "Admin", Permissions = new List<Permission>()};
            for (int i = 0; i < 15; i++)
                admin.Permissions.Add(new Permission{Identifier = (PermissionType)i, Value = PermissionValue.Full});
            var sampler = new Role { Active = true, Name = "Muestreador", Permissions = new List<Permission>() };
            var coordinator = new Role {Active = true, Name = "Coordinador", Permissions = new List<Permission>()};
            for (int i = 15; i < 34; i++)
                coordinator.Permissions.Add(new Permission{Identifier = (PermissionType)i, Value = PermissionValue.Assign});
            var coordinatorBoss = new Role { Active = true, Name = "Jefe de Coordinador", Permissions = new List<Permission>() };
            for (int i = 15; i < 34; i++)
                coordinatorBoss.Permissions.Add(new Permission{Identifier = (PermissionType)i, Value = PermissionValue.Assign});
            var manager = new Role { Active = true, Name = "Gerente", Permissions = new List<Permission>() };
            for (int i = 15; i < 34; i++)
                if (i % 5 == 0 || i % 5 == 1 || (i >= 30 && i != 31))
                    manager.Permissions.Add(new Permission{Identifier = (PermissionType)i, Value = PermissionValue.Full});


            var adminUser = new User() { Name = "admin", Password = "pass" };
            var adminEmployee = new Employee { Name = "", LastName = "", Role = admin, User = adminUser, RegisterDate = DateTime.Now.GetUnixEpoch()};


            context.AddRole(admin);
            context.AddEmployee(adminEmployee);

            context.AddRole(manager);
            context.AddRole(coordinatorBoss);
            context.AddRole(coordinator);
            context.AddRole(sampler);

            base.Seed(context);
        }
    }
}