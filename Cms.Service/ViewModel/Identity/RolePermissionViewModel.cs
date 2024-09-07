using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Identity
{
    public class RolePermissionViewModel
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }

        public bool IsUpdate { get; set; }
       
    }
    public class GrantPermissionViewModel
    {
        public IList<AppRoleViewModel> AppRoles { get; set; }

        public IList<RolePermissionViewModel> RolePermissions { get; set; }

        public IList<PermissionViewModel> Permissions { get; set; }
        
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }



    }
    public class PermissionViewModel
    {
        public int Id { get; set; }


        public Guid RoleId { get; set; }

        public string FunctionId { get; set; }

        public bool CanCreate { set; get; }

        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }

        public bool CanDelete { set; get; }

        public AppRoleViewModel AppRole { get; set; }

        public FunctionViewModel Function { get; set; }
    }
    
}
