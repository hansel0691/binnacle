using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace ElectronicBinnacle.Models.Models.UserControl
{
    public class Role
    {
        #region Properties

        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        
        public  List<Permission> Permissions { get; set; }

        #endregion

        public void CopyProps(Role role)
        {
            this.RoleId = role.RoleId;
            this.Name = role.Name;
            this.Active = role.Active;
            if (this.Permissions != null)
            {
                this.Permissions.Clear();
                if (role.Permissions != null)
                    this.Permissions.AddRange(role.Permissions);
            }
            
        }
    }
}