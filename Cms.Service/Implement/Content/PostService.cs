using AutoMapper;
using Cms.Data.Entities.Content;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Enums;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using Cms.Utilities.Contants;
using Cms.Utilities.Helpers;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Content
{
    public class PostService : WebServiceBase<Post, Guid, PostViewModel>, IPostService
    {
        private IRepository<Post, Guid> _postRepository;
        private IRepository<PostTag, Guid> _postTagRepository;
        private IRepository<Tag, string> _tagRepository;
        private IRepository<PostCate, int> _postCategoryRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PostService(IRepository<Post, Guid> postRepository, IMapper mapper, IRepository<PostTag, Guid> postTagRepository,
            IRepository<Tag, string> tagRepository,
            IRepository<PostCate, int> postCategoryRepository,
            IUnitOfWork unitOfWork) : base(postRepository, unitOfWork, mapper)
        {
            this._postRepository = postRepository;
            _postTagRepository = postTagRepository;
            _tagRepository = tagRepository;
            _postCategoryRepository = postCategoryRepository;
            _mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public PagedResult<PostViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _postRepository.GetAll();
            var model = filterModel.OrderByDescending(x => x.DateCreated).Skip((viewModel.PageIndex - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
            var totalRow = filterModel.Count();
            var postVm = _mapper.Map<List<Post>, List<PostViewModel>>(model);
            var paginationSet = new PagedResult<PostViewModel>()
            {
                Results = postVm,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize
            };
            return paginationSet;
        }

        public List<PostViewModel> GetLastest(int top)
        {
            var model = _postRepository.GetAll(x => x.Status == Status.Active).OrderByDescending(x => x.DateCreated)
                .Take(top).ToList();
            return _mapper.Map<List<Post>, List<PostViewModel>>(model);
        }

        public List<PostViewModel> GetHotProduct(int top)
        {
            throw new NotImplementedException();
        }

        public List<PostViewModel> GetReatedBlogs(int id, int top)
        {
            throw new NotImplementedException();
        }

        public List<PostViewModel> GetListTagById(int id)
        {
            throw new NotImplementedException();
        }

        public PostViewModel GetTag(string tagId)
        {
            throw new NotImplementedException();
        }

        public void IncreaseView(Guid id)
        {
            var product = _postRepository.GetById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public List<PostViewModel> GetListByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in _postRepository.GetAll()
                        join pt in _postTagRepository.GetAll()
                        on p.Id equals pt.PostId
                        where pt.TagId == tagId && p.Status == Status.Active
                        orderby p.DateCreated descending
                        select p;

            totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var model = _mapper.Map<List<Post>, List<PostViewModel>>(query.ToList());
            return model;
        }

        public List<PostViewModel> GetListTag(string searchText)
        {
            throw new NotImplementedException();
        }

        public PostViewModel Create(PostViewModel postVm)
        {
            var blog = _mapper.Map<PostViewModel, Post>(postVm);

            List<PostTag> postTags = new List<PostTag>();
            List<PostCate> postCates = new List<PostCate>();
            if (!string.IsNullOrEmpty(blog.Tags))
            {
                var tags = blog.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.GetAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = TagType.Content
                        };
                        _tagRepository.Insert(tag);
                    }

                    var blogTag = new PostTag { TagId = tagId };
                    postTags.Add(blogTag);
                }
                foreach (var postTag in postTags)
                {
                    blog.PostTags.Add(postTag);
                }
                //  _productRepository.Insert(product);
            }
            if (!string.IsNullOrEmpty(blog.Categories))
            {
                var categories = blog.Categories.Split(',');
                foreach (string t in categories)
                {
                    var categoryId = Convert.ToInt32(t);

                    var postCate = new PostCate { PostCategoryId = categoryId };
                    postCates.Add(postCate);
                }
                foreach (var postCate in postCates)
                {
                    blog.PostCates.Add(postCate);
                }
            }
            _postRepository.Insert(blog);
            Save();
            return postVm;
        }
        public void UpdatePost(PostViewModel post)
        {
            _postRepository.Update(_mapper.Map<PostViewModel, Post>(post));
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] tags = post.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.GetAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = TagType.Content
                        };
                        _tagRepository.Insert(tag);
                    }
                    _postTagRepository.RemoveMultiple(_postTagRepository.GetAll(x => x.PostId == post.Id).ToList());
                    PostTag blogTag = new PostTag
                    {
                        PostId = post.Id,
                        TagId = tagId
                    };
                    _postTagRepository.Insert(blogTag);
                }
            }
            Save();
        }

        public void ImportExcel(string filePath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Post post;
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    post = new Post();
                    //     post.CategoryId = categoryId;

                    post.Name = workSheet.Cells[i, 1].Value.ToString();

                    post.Description = workSheet.Cells[i, 2].Value.ToString();
                    post.Content = workSheet.Cells[i, 3].Value.ToString();
                    post.SeoKeywords = workSheet.Cells[i, 4].Value.ToString();
                    post.SeoDescription = workSheet.Cells[i, 5].Value.ToString();
                    bool.TryParse(workSheet.Cells[i, 6].Value.ToString(), out var hotFlag);
                    post.HotFlag = hotFlag;
                    bool.TryParse(workSheet.Cells[i, 7].Value.ToString(), out var homeFlag);
                    post.HomeFlag = homeFlag;
                    post.Status = Status.Active;
                    _postRepository.Insert(post);
                }
            }
        }

        public List<PostViewModel> GetTop(int top)
        {
            // var model = _postRepository.GetAll().OrderByDescending(x => x.DateCreated).Take(top).ToList();
            //  var blogs = _mapper.Map<List<Post>, List<PostViewModel>>(model);
            //return blogs;
            return null;
        }
    }
}
