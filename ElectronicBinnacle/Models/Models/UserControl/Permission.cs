using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.UserControl
{
    public enum PermissionType
    {
        AllUsers,
        ViewUser,
        NewUser,
        EditUser,
        RemoveUser,

        AllRoles,
        ViewRole,
        NewRole,
        EditRole,
        RemoveRole,

        AllEmployees,
        ViewEmployee,
        NewEmployee,
        EditEmployee,
        RemoveEmployee,

        AllParams,
        ViewParam,
        NewParam,
        EditParam,
        RemoveParam,

        AllPackages,
        ViewPackage,
        NewPackage,
        EditPackage,
        RemovePackage,

        AllOrders,
        ViewOrder,
        NewOrder,
        EditOrder,
        RemoveOrder,

        ViewSample,
        EditSample,

        ViewStats,
        ViewRoutes

    }
    public enum PermissionValue
    {
        None,
        Assign,
        Full
    }

    public class Permission
    {
        #region Properties

        public int PermissionId { get; set; }
        public PermissionType Identifier { get; set; }
        public PermissionValue Value { get; set; }

        #endregion
        #region Methods

        public static IEnumerable<PermissionType> AllPermissions()
        {
            return new[]{
                PermissionType.AllUsers,PermissionType.ViewUser,PermissionType.NewUser,PermissionType.EditUser,PermissionType.RemoveUser,
                PermissionType.AllRoles,PermissionType.ViewRole,PermissionType.NewRole,PermissionType.EditRole,PermissionType.RemoveRole,
                PermissionType.AllEmployees,PermissionType.ViewEmployee,PermissionType.NewEmployee,PermissionType.EditEmployee,PermissionType.RemoveEmployee,
                PermissionType.AllParams,PermissionType.ViewParam,PermissionType.NewParam,PermissionType.EditParam,PermissionType.RemoveParam,
                PermissionType.AllPackages,PermissionType.ViewPackage,PermissionType.NewPackage,PermissionType.EditPackage,PermissionType.RemovePackage,
                PermissionType.AllOrders,PermissionType.ViewOrder,PermissionType.NewOrder,PermissionType.EditOrder,PermissionType.RemoveOrder,
                PermissionType.ViewSample, PermissionType.EditSample, PermissionType.ViewStats, PermissionType.ViewRoutes
           };
        }
        public static string ToSpanish(PermissionType permissions)
        {
            switch (permissions)
            {
                case PermissionType.AllUsers:
                    return "Ver lista de Usuarios";
                case PermissionType.ViewUser:
                    return "Ver Usuario";
                case PermissionType.NewUser:
                    return "Crear Usuario";
                case PermissionType.EditUser:
                    return "Editar Usuario";
                case PermissionType.RemoveUser:
                    return "Eliminar Usuario";
                case PermissionType.AllRoles:
                    return "Ver lista de Roles";
                case PermissionType.ViewRole:
                    return "Ver Role";
                case PermissionType.NewRole:
                    return "Crear Role";
                case PermissionType.EditRole:
                    return "Editar Role";
                case PermissionType.RemoveRole:
                    return "Eliminar Role";
                case PermissionType.AllEmployees:
                    return "Ver lista de Empleados";
                case PermissionType.ViewEmployee:
                    return "Ver Empleado";
                case PermissionType.NewEmployee:
                    return "Crear Empleado";
                case PermissionType.EditEmployee:
                    return "Editar Empleado";
                case PermissionType.RemoveEmployee:
                    return "Eliminar Empleado";
                case PermissionType.AllParams:
                    return "Ver lista de Parametros";
                case PermissionType.ViewParam:
                    return "Ver Parametro";
                case PermissionType.NewParam:
                    return "Crear Parametro";
                case PermissionType.EditParam:
                    return "Editar Parametro";
                case PermissionType.RemoveParam:
                    return "Eliminar Parametro";
                case PermissionType.AllPackages:
                    return "Ver lista de Grupos";
                case PermissionType.ViewPackage:
                    return "Ver Grupo";
                case PermissionType.NewPackage:
                    return "Crear Grupo";
                case PermissionType.EditPackage:
                    return "Editar Grupo";
                case PermissionType.RemovePackage:
                    return "Eliminar Grupo";
                case PermissionType.AllOrders:
                    return "Ver lista de Ordenes de Trabajo";
                case PermissionType.ViewOrder:
                    return "Ver Orden de Trabajo";
                case PermissionType.NewOrder:
                    return "Crear Orden de Trabajo";
                case PermissionType.EditOrder:
                    return "Editar Orden de Trabajo";
                case PermissionType.RemoveOrder:
                    return "Eliminar Orden de Trabajo";
                case PermissionType.ViewSample:
                    return "Ver Muestra";
                case PermissionType.EditSample:
                    return "Editar Muestra";
                case PermissionType.ViewStats:
                    return "Ver Informe de Usuario";
                case PermissionType.ViewRoutes:
                    return "Ver Rutas";
            }
            throw new NotImplementedException();
        }

        #endregion
    }
}