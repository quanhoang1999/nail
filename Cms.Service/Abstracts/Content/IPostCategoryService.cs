using Cms.Data.Entities.Content;
using Cms.Service.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Content
{
    public interface IPostCategoryService : IWebServiceBase<PostCategory, int, PostCategoryViewModel>
    {
        void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items);
        void ReOrder(int sourceId, int targetId);

    }
}
