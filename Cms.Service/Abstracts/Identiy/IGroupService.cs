using Cms.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service.Abstracts.Identiy
{
    public interface IGroupService : IServiceBase<AppGroup, Guid>
    {
        List<AppRole> GetRoleByGroupIdAsync(Guid groupId, int pageIndex, int pageSize, out int totalRow);

        bool AddRoleToGroups(IEnumerable<AppRoleGroup> appRoleGroups, Guid groupId);

        bool AddUserToGroups(IEnumerable<AppUserGroup> appUserGroups, Guid groupId);

        bool AddGroupToUser(IEnumerable<AppUserGroup> appUserGroups, Guid userId);

        bool AddGroupToRoles(IEnumerable<AppRoleGroup> appRoleGroup, Guid roleid);

        bool DeleteRoleToGroups(List<Guid> appRoleGroup, Guid groupId);

        bool DeleteUserToGroups(List<Guid> appUserGroup, Guid groupId);

        bool DeleteGroupToUser(List<Guid> appUserGroup, Guid userId);

        bool DeleteGroupToRoles(List<Guid> appRoleGroup, Guid roledId);

        List<AppUser> GetUserByGroupId(Guid groupId, int pageIndex, int pageSize, out int totalRow);

        List<AppGroup> GetGroupByUserId(Guid userId, int pageIndex, int pageSize, out int totalRow);

        List<AppGroup> GetGroupyRoleId(Guid roleId, int pageIndex, int pageSize, out int totalRow);


    }
}
