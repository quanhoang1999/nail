using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using static Cms.Utilities.Helpers.TextHelper;

namespace Cms.Service.Abstracts.Identiy
{
    public interface IUIElementService : IServiceBase<UIElement, Guid>
    {
        IList<TreeViewItem> GetTreeViewItemsByRoleId(Guid roleId, AppUser appUser);

        void UpdateUIRole(List<Guid> uIelements, Guid roleId);

        List<Menu> GetMenuByRoleIds(IList<string> roleIds, AppUser appUser);
        
    }
}
