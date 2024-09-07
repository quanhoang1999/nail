using AutoMapper;
using Cms.Data.EF;
using Cms.Data.Entities.Media;
using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Nails;
using Cms.Service.Mapping;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Nails
{
    public class NailCategoryService : WebServiceBase<NailCategory, int, NailCategoryViewModel>, INailCategoryService
    {
        private readonly IRepository<NailCategory, int> _nailCategoryRepository;
        private readonly IRepository<NailCategoryPicture, int> _nailCategoryPictureRepository;
        private readonly IRepository<Picture, int> _pictureRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContextDefault _context;
        public NailCategoryService(IRepository<NailCategory, int> nailCategoryRepository,
            IRepository<NailCategoryPicture, int> nailCategoryPictureRepository,
            IRepository<Picture, int> pictureRepository,
            IWebHostEnvironment hostingEnvironment,
            AppDbContextDefault context,
           IUnitOfWork unitOfWork, IMapper mapper) : base(nailCategoryRepository, unitOfWork, mapper)
        {
            _nailCategoryRepository = nailCategoryRepository;
            _nailCategoryPictureRepository = nailCategoryPictureRepository;
            _pictureRepository = pictureRepository;
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }

        public PagedResult<NailCategoryViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var query = _nailCategoryRepository.GetAllIncluding(x => x.NailStore).Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(viewModel.KeyWord))
                query = query.Where(x => x.Name.Contains(viewModel.KeyWord));
            if (viewModel.StoreId > 0)
                query = query.Where(x => x.NailStoreId == viewModel.StoreId);
            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize);
            var nailCustomers = _mapper.Map<List<NailCategory>, List<NailCategoryViewModel>>(data.ToList());
            var paginationSet = new PagedResult<NailCategoryViewModel>()
            {
                Results = nailCustomers,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize,
            };
            return paginationSet;
        }
        public void InsertCategory(NailCategoryViewModel viewModel)
        {
            try
            {
                var nailCategory = _mapper.Map<NailCategoryViewModel, NailCategory>(viewModel);
                List<NailCategoryPicture> catePictures = new List<NailCategoryPicture>();
                string images = string.Empty;
                foreach (var item in viewModel.Pictures)
                {
                    NailCategoryPicture nailCategoryPicture = new NailCategoryPicture { PictureId = item.Id };
                    images = images + item.VirtualPath + ",";
                    catePictures.Add(nailCategoryPicture);
                }
                foreach (var item in catePictures)
                {
                    nailCategory.NailCategoryPictures.Add(item);
                }
                nailCategory.Images = images;
                nailCategory.NailStore = null;
                _nailCategoryRepository.Insert(nailCategory);
                Save();
            }
            catch (Exception e)
            {
                string message = e.Message;
                throw;
            }

        }
        public void UpdateCategory(NailCategoryViewModel viewModel)
        {
            var nailCategory = _nailCategoryRepository.GetById(viewModel.Id);
            string images = nailCategory.Images;
            var dateCreated = nailCategory.DateCreated;
            nailCategory = viewModel.ToEntity(nailCategory);
            nailCategory.DateCreated = dateCreated;
            nailCategory.NailStore = null;
            //var nailCategory = _mapper.Map<NailCategoryViewModel, NailCategory>(viewModel);
            List<NailCategoryPicture> catePictures = new List<NailCategoryPicture>();

            foreach (var item in viewModel.Pictures)
            {
                _nailCategoryPictureRepository.RemoveMultiple(_nailCategoryPictureRepository.GetAll(x => x.NailCategoryId == viewModel.Id).ToList());
                NailCategoryPicture nailCategoryPicture = new NailCategoryPicture { PictureId = item.Id };
                images = images + item.VirtualPath + ",";
                nailCategoryPicture.NailCategoryId = viewModel.Id;
                _nailCategoryPictureRepository.Insert(nailCategoryPicture);
            }
            nailCategory.Images = images;
            _nailCategoryRepository.Update(nailCategory);
            Save();
        }

        public List<NailCategoryViewModel> GetListShowOnHomePage(int storeId)
        {
            var data = _nailCategoryRepository.GetAll().Where(x => x.IsDeleted == false && x.IsShowHomePage && x.IsActive && x.NailStoreId == storeId).OrderBy(c => c.OrderIndex).Take(4);
            var nailCate = _mapper.Map<List<NailCategory>, List<NailCategoryViewModel>>(data.ToList());
            return nailCate;
        }
        public List<NailCategoryViewModel> GetListShowOnMenu(int storeId)
        {
            var data = _nailCategoryRepository.GetAll().Where(x => x.IsDeleted == false && x.IsShowMenu && x.IsActive && x.NailStoreId == storeId).OrderBy(c => c.OrderIndex);
            var nailCate = _mapper.Map<List<NailCategory>, List<NailCategoryViewModel>>(data.ToList());
            return nailCate;
        }
        public List<NailCategoryViewModel> GetListShowOnMenu()
        {
            var data = _nailCategoryRepository.GetAll().Where(x => x.IsDeleted == false && x.IsShowMenu && x.IsActive).OrderBy(c => c.OrderIndex);
            var nailCate = _mapper.Map<List<NailCategory>, List<NailCategoryViewModel>>(data.ToList());
            return nailCate;
        }

        public List<NailCategoryViewModel> GetAllInclude(int storeId)
        {
            var model = _nailCategoryRepository.GetAllIncluding(x => x.NailServices).Where(c => !c.IsDeleted && c.NailStoreId == storeId).ToList();
            var nailCategories = _mapper.Map<List<NailCategory>, List<NailCategoryViewModel>>(model);
            return nailCategories;
        }
        public List<NailCategoryViewModel> GetAllInclude()
        {
            var model = _nailCategoryRepository.GetAllIncluding(x => x.NailServices).Where(c => !c.IsDeleted).ToList();
            var nailCategories = _mapper.Map<List<NailCategory>, List<NailCategoryViewModel>>(model);
            return nailCategories;
        }
        public bool IsDelete(int id)
        {
            var model = _nailCategoryRepository.GetById(id);
            model.IsDeleted = true;
            model.DateDeleted = DateTime.Now;
            _nailCategoryRepository.Update(model);
            Save();
            return true;
        }

        public bool DeleteFile(int id, string images)
        {
            var nailCategory = _nailCategoryRepository.GetById(id);
            string fileDelete = _hostingEnvironment.WebRootPath + images;
            string filename = Path.GetFileName(fileDelete);
            string filenameWithoutEx = Path.GetFileNameWithoutExtension(fileDelete);
            if (File.Exists(fileDelete))
            {
                // If file found, delete it    
                File.Delete(fileDelete);
            }
            string newImages = string.Empty;

            _pictureRepository.Delete(x => x.FileName == filenameWithoutEx);
            _nailCategoryPictureRepository.Delete(x => x.NailCategoryId == id && x.Picture.FileName == filenameWithoutEx);
            foreach (var item in nailCategory.Images.Split(','))
            {
                if (item != filename && !string.IsNullOrEmpty(item))
                    newImages += item + ",";
            }
            nailCategory.Images = newImages;
            _nailCategoryRepository.Update(nailCategory);
            Save();
            return true;
        }
    }
}
