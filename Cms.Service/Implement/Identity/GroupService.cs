using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Identiy;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cms.Service.Implement
{
    public class GroupService : ServiceBase<AppGroup, Guid>, IGroupService
    {
        private readonly IRepository<AppGroup, Guid> _groupRepository;
        private readonly IRepository<AppUserGroup, Guid> _userGroupRepository;
        private readonly IRepository<AppRoleGroup, Guid> _roleGroupRepository;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private IUnitOfWork _unitOfWork;
       
        public GroupService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IRepository<AppGroup, Guid> groupRepository, IRepository<AppUserGroup, Guid> userGroupRepository, IRepository<AppRoleGroup, Guid> roleGroupRepository,
            IUnitOfWork unitOfWork) : base(groupRepository, unitOfWork)
        {
            _userGroupRepository = userGroupRepository;
            _groupRepository = groupRepository;
            _roleGroupRepository = roleGroupRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
          
        }

        public bool AddUserToGroups(IEnumerable<AppUserGroup> appUserGroup, Guid groupId)
        {
            _userGroupRepository.Delete(x => x.GroupId == groupId);
            foreach (var userGroup in appUserGroup)
            {
                _userGroupRepository.Insert(userGroup);
            }
            return true;
        }

        public bool AddGroupToUser(IEnumerable<AppUserGroup> appUserGroup, Guid userId)
        {
            _userGroupRepository.Delete(x => x.UserId == userId);
            foreach (var userGroup in appUserGroup)
            {
                _userGroupRepository.Insert(userGroup);
            }
            return true;
        }



        public bool AddGroupToRoles(IEnumerable<AppRoleGroup> appRoleGroup, Guid roleid)
        {
            _roleGroupRepository.Delete(x => x.RoleId == roleid);
            foreach (var roleGroup in appRoleGroup)
            {
                _roleGroupRepository.Insert(roleGroup);
            }
            return true;
        }


        public bool AddRoleToGroups(IEnumerable<AppRoleGroup> appRoleGroup, Guid groupId)
        {
            _roleGroupRepository.Delete(x => x.GroupId == groupId);
            foreach (var roleGroup in appRoleGroup)
            {
                _roleGroupRepository.Insert(roleGroup);
            }
            return true;
        }

        public bool DeleteRoleToGroups(List<Guid> appRoleGroup, Guid groupId)
        {
            _roleGroupRepository.Delete(x => x.GroupId == groupId && appRoleGroup.Contains(x.RoleId));
            return true;
        }

        public bool DeleteGroupToUser(List<Guid> appUserGroup, Guid userId)
        {
            _userGroupRepository.Delete(x => x.UserId == userId && appUserGroup.Contains(x.GroupId));
            return true;
        }

        public bool DeleteUserToGroups(List<Guid> appUserGroup, Guid groupId)
        {
            _userGroupRepository.Delete(x => x.GroupId == groupId && appUserGroup.Contains(x.UserId));
            return true;
        }

        public bool DeleteGroupToRoles(List<Guid> appRoleGroup, Guid roleId)
        {
            _roleGroupRepository.Delete(x => x.RoleId == roleId && appRoleGroup.Contains(x.GroupId));
            return true;
        }

        public List<AppRole> GetRoleByGroupIdAsync(Guid groupId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = (from x in _roleManager.Roles
                         join y in _roleGroupRepository.GetAll()
                         on x.Id equals y.RoleId
                         into xy
                         from y in xy.DefaultIfEmpty()
                         where ( y.GroupId == groupId)
                         select x);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public List<AppGroup> GetGroupyRoleId(Guid roleId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = (from x in _groupRepository.GetAll()
                         join y in _roleGroupRepository.GetAll()
                         on x.Id equals y.GroupId
                         into xy
                         from y in xy.DefaultIfEmpty()
                         where (y.RoleId == roleId)
                         select x);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public List<AppGroup> GetGroupByUserId(Guid userId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = (from x in _groupRepository.GetAll()
                         join y in _userGroupRepository.GetAll()
                         on x.Id equals y.GroupId
                         into xy
                         from y in xy.DefaultIfEmpty()
                         where (y.UserId == userId)
                         select x);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public List<AppUser> GetUserByGroupId(Guid groupId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = (from x in _userManager.Users
                         join y in _userGroupRepository.GetAll()
                         on x.Id equals y.UserId
                         into xy
                         from y in xy.DefaultIfEmpty()
                         where (y.GroupId == groupId)
                         select x);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }


    }
}
