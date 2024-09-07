using Cms.Data.Entities.Content;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Content
{
    public interface IPostService : IWebServiceBase<Post, Guid, PostViewModel>
    {
        PagedResult<PostViewModel> Filter(FilterCommonViewModel viewModel);
        List<PostViewModel> GetLastest(int top);
        List<PostViewModel> GetHotProduct(int top);
        List<PostViewModel> GetReatedBlogs(int id, int top);
        List<PostViewModel> GetListTagById(int id);

        PostViewModel GetTag(string tagId);

        void IncreaseView(Guid id);

        List<PostViewModel> GetListByTag(string tagId, int page, int pagesize, out int totalRow);

        List<PostViewModel> GetListTag(string searchText);
        PostViewModel Create(PostViewModel postVm);
        void UpdatePost(PostViewModel post);
        void ImportExcel(string filePath, int categoryId);
        List<PostViewModel> GetTop(int top);
    }
}
