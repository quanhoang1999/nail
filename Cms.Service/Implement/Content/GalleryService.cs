using AutoMapper;
using Cms.Data.Entities.Content;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Content
{
    public class GalleryService : WebServiceBase<Gallery, int, GalleryViewModel>, IGalleryService
    {
        private readonly IRepository<Gallery, int> _galleryRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GalleryService(IRepository<Gallery, int> galleryRepository,
           IUnitOfWork unitOfWork, IMapper mapper) : base(galleryRepository, unitOfWork, mapper)
        {
            _galleryRepository = galleryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public PagedResult<GalleryViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var query = _galleryRepository.GetAll().Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(viewModel.KeyWord))
                query = query.Where(x => x.Name.Contains(viewModel.KeyWord));
            if (viewModel.StoreId > 0)
                query = query.Where(x => x.NailStoreId == viewModel.StoreId);
            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize);
            var galleries = _mapper.Map<List<Gallery>, List<GalleryViewModel>>(data.ToList());
            var paginationSet = new PagedResult<GalleryViewModel>()
            {
                Results = galleries,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize,
            };

            return paginationSet;
        }

        public PagedResult<GalleryViewModel> GalleryGetType(FilterCommonViewModel viewModel)
        {
            var query = _galleryRepository.GetAll().Where(x => x.FileType == 2);
            var data = query.OrderByDescending(x => x.DateCreated)
             .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
             .Take(viewModel.PageSize);
            var galleries = _mapper.Map<List<Gallery>, List<GalleryViewModel>>(data.ToList());
            var paginationSet = new PagedResult<GalleryViewModel>()
            {
                Results = galleries
            };
            return paginationSet;
        }
        public List<GalleryViewModel> GetGalleryByNailStoreId(int nailStoreId)
        {
            var query = _galleryRepository.GetAll().Where(x => x.NailStoreId == nailStoreId);
            List<Gallery> data = query
                .Select(ga => new Gallery()
                {
                    Name = ga.Name,
                    FileUrl = ga.FileUrl,
                    FileType = ga.FileType,
                    NailStoreId=ga.NailStoreId,
                    DateCreated=ga.DateCreated
                })
                .OrderByDescending(x => x.DateCreated).ToList();
            var galleries = _mapper.Map<List<Gallery>, List<GalleryViewModel>>(data.ToList());
            return galleries;
        }
        public bool UpdateGallery(GalleryViewModel viewModel)
        {
            if (viewModel.Id > 0)
            {
                var gallery = _galleryRepository.GetById(viewModel.Id);
                gallery.Name = viewModel.Name;
                gallery.DateModified = viewModel.DateModified;
                gallery.Description = viewModel.Description;
                gallery.FileType = viewModel.FileType;
                gallery.FileUrl = viewModel.FileUrl;
                _galleryRepository.Update(gallery);
                return true;
            }
            return false;
        }

    }
}
