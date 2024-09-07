using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Identiy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cms.Service.Implement
{
    public class UIElementService : ServiceBase<UIElement, Guid>, IUIElementService
    {
        private readonly IRepository<UIElement, Guid> _uIElementRepository;
        private readonly IRepository<RoleUIElement, Guid> _roleUIElementRepository;
        private readonly IRepository<Data.Entities.Identity.Business, Guid> _businessRepository;
        private IUnitOfWork _unitOfWork;
        public UIElementService(IRepository<UIElement, Guid> uIElementRepository, IRepository<RoleUIElement, Guid> roleUIElementRepository,IRepository<Data.Entities.Identity.Business, Guid> businessRepository,
           IUnitOfWork unitOfWork) : base(uIElementRepository, unitOfWork)
        {
            _uIElementRepository = uIElementRepository;
            _roleUIElementRepository = roleUIElementRepository;
            _businessRepository = businessRepository;
            _unitOfWork = unitOfWork;
        }

        public IList<TreeViewItem> GetTreeViewItemsByRoleId(Guid roleId, AppUser appUser)
        {
            var roleUis = _roleUIElementRepository.GetAll(x => x.RoleId == roleId);
            var uIElements = _uIElementRepository.GetAll().OrderBy(x=>x.Order).ToList();

            List<RoleUIElementDto> roleUIElements = new List<RoleUIElementDto>();
            foreach (var element in uIElements)
            {
                var roleUIElement = new RoleUIElementDto();
                roleUIElement.Id = element.Id;
                roleUIElement.ParentId = element.ParentId;
                roleUIElement.Name = element.Name;
                foreach (var item in roleUis)
                {
                    if (element.Id == item.ElementId)
                    {
                        roleUIElement.Checked = true;
                    }
                }
                roleUIElements.Add(roleUIElement);
            }
            return GenerateTree(roleUIElements);
        }

        public void UpdateUIRole(List<Guid> uIelements, Guid roleId)
        {
            _roleUIElementRepository.Delete(x => x.RoleId == roleId);
            foreach (var ui in uIelements)
            {
                var roleUI = new RoleUIElement();
                roleUI.RoleId = roleId;
                roleUI.ElementId = ui;
                _roleUIElementRepository.Insert(roleUI);
            }
            _unitOfWork.Commit();
        }


        public List<Menu> GetMenuByRoleIds(IList<string> roleIds, AppUser appUser)
        {
            var roleUis = _roleUIElementRepository.GetAll(x => roleIds.Contains(x.AppRole.Name.ToString()));
            var uIElements = _uIElementRepository.GetAll().OrderBy(x => x.Order).ToList();
            List<Menu> menus = new List<Menu>();
            List<MainMenuItems> mainMenuItems = new List<MainMenuItems>();
            List<UIElement> newUIElemets = new List<UIElement>();
            foreach (var element in uIElements)
            {
                var menu = new Menu();
                foreach (var item in roleUis)
                {
                    if (element.Id == item.ElementId)
                    {
                        if (!newUIElemets.Any(x => x.Id == element.Id))
                            newUIElemets.Add(element);
                    }
                }

            }
            return GetMenus(newUIElemets);
        }


        #region Load TreeViewItem in Settings

        public List<TreeViewItem> GenerateTree(IEnumerable<RoleUIElementDto> uIElements)
        {
            List<TreeViewItem> treeViewItems = new List<TreeViewItem>();
            foreach (var c in uIElements.Where(c => c.ParentId == null))
            {
                treeViewItems.Add(new TreeViewItem
                {
                    text = c.Name,
                    value = c.Id,
                    @checked = c.Checked,
                    children = GenerateSubTree(uIElements, c.Id)
                });
            }
            return treeViewItems;
        }

        public List<TreeViewItem> GenerateSubTree(IEnumerable<RoleUIElementDto> uIElements, Guid subId)
        {
            List<TreeViewItem> treeViewItems = new List<TreeViewItem>();
            foreach (var c in uIElements.Where(c => c.ParentId == subId))
            {
                treeViewItems.Add(new TreeViewItem
                {
                    text = c.Name,
                    value = c.Id,
                    @checked = c.Checked,
                    children = GenerateSubTree(uIElements, c.Id)
                });
            }
            return treeViewItems;
        }

        #endregion

        #region LoadMenuByRoleIds
        public List<Menu> GetMenus(IEnumerable<UIElement> uIElements)
        {
            List<Menu> menus = new List<Menu>();
            foreach (var item in uIElements.Where(x => x.ParentId == null))
            {
                menus.Add(new Menu
                {
                    label = item.Name,
                    main = GetMainMenuItems(uIElements, item.Id)
                });
            }
            return menus;
        }

        public List<MainMenuItems> GetMainMenuItems(IEnumerable<UIElement> uIElements, Guid parentId)
        {
            List<MainMenuItems> mainMenuItems = new List<MainMenuItems>();
            foreach (var item in uIElements.Where(x => x.ParentId == parentId))
            {
                mainMenuItems.Add(new MainMenuItems
                {
                    name = item.Name,
                    icon = item.IconPath,
                    state = item.TargetURL,
                    type = item.Type,
                    children = GetSubMenus(uIElements, item.Id)
                });
            }
            return mainMenuItems;
        }


        public List<ChildrenItems> GetSubMenus(IEnumerable<UIElement> uIElements, Guid subId)
        {
            List<ChildrenItems> childrenItems = new List<ChildrenItems>();
            foreach (var item in uIElements.Where(x => x.ParentId == subId))
            {
                childrenItems.Add(new ChildrenItems
                {
                    name = item.Name,
                    state = item.TargetURL,
                    type = item.Type,
                    children = GetSubMenus(uIElements, item.Id)
                });
            }
            return childrenItems;
        }
        #endregion
       

    }
}

