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
    public class NailServiceService : WebServiceBase<NailService, int, NailServiceViewModel>, INailServiceService
    {
        private readonly IRepository<NailService, int> _nailServiceRepository;
        private readonly IRepository<Picture, int> _pictureRepository;
        private readonly IRepository<NailServicePicture, int> _nailServicePictureRepository;
        private readonly IRepository<Cms.Data.Entities.Nails.NailEmployeeService, int> _nailServiceEmployeeRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContextDefault _context;
        public NailServiceService(IRepository<NailService, int> nailServiceRepository,
            IRepository<Picture, int> pictureRepository,
            IRepository<NailServicePicture, int> nailServicePictureRepository,
            IRepository<Cms.Data.Entities.Nails.NailEmployeeService, int> nailServiceEmployeeRepository,
        IWebHostEnvironment hostingEnvironment,
            AppDbContextDefault context,
           IUnitOfWork unitOfWork, IMapper mapper) : base(nailServiceRepository, unitOfWork, mapper)
        {
            _nailServiceRepository = nailServiceRepository;
            _nailServicePictureRepository = nailServicePictureRepository;
            _nailServiceEmployeeRepository = nailServiceEmployeeRepository;
            _pictureRepository = pictureRepository;
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }
        public PagedResult<NailServiceViewModel> Filter(FilterCommonViewModel viewModel)
        {
            string[] includes = { "NailCategory", "NailStore" };
            var query = _nailServiceRepository.GetAllIcludes(includes).Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(viewModel.KeyWord))
                query = query.Where(x => x.Name.Contains(viewModel.KeyWord));
            if (viewModel.CategoryId > 0)
                query = query.Where(x => x.NailCategoryId == viewModel.CategoryId);
            if(viewModel.StoreId > 0)
                query = query.Where(x => x.NailStoreId == viewModel.StoreId);
            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize);
            var nailServices = _mapper.Map<List<NailService>, List<NailServiceViewModel>>(data.ToList());
            var paginationSet = new PagedResult<NailServiceViewModel>()
            {
                Results = nailServices,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize,
            };
            return paginationSet;
        }

        public void InsertService(NailServiceViewModel viewModel)
        {
            var nailService = _mapper.Map<NailServiceViewModel, NailService>(viewModel);
            List<NailServicePicture> catePictures = new List<NailServicePicture>();
            List<Cms.Data.Entities.Nails.NailEmployeeService> nailEmployeeServices = new List<Cms.Data.Entities.Nails.NailEmployeeService>();
            string images = string.Empty;
            foreach (var item in viewModel.Pictures)
            {
                NailServicePicture nailCategoryPicture = new NailServicePicture { PictureId = item.Id };
                images += images + item.VirtualPath + ",";
                catePictures.Add(nailCategoryPicture);
            }
            foreach (var item in viewModel.Employees)
            {
                var nailCategoryPicture = new Cms.Data.Entities.Nails.NailEmployeeService { NailEmployeeId = item };
                nailEmployeeServices.Add(nailCategoryPicture);
            }
            foreach (var item in catePictures)
            {
                nailService.NailServicePictures.Add(item);
            }
            foreach (var item in nailEmployeeServices)
            {
                nailService.NailEmployeeServices.Add(item);
            }
            nailService.Images = images;
            _nailServiceRepository.Insert(nailService);
            Save();


        }

        public void UpdateService(NailServiceViewModel viewModel)
        {
            var nailService = _nailServiceRepository.GetById(viewModel.Id);
            string images = nailService.Images;
            string employees = string.Empty;
            var dateCreated = nailService.DateCreated;
            nailService = viewModel.ToEntity(nailService);
            nailService.DateCreated = dateCreated;
            nailService.NailStore = null;
            List<NailServicePicture> catePictures = new List<NailServicePicture>();

            foreach (var item in viewModel.Pictures)
            {
                _nailServicePictureRepository.RemoveMultiple(_nailServicePictureRepository.GetAll(x => x.NailServiceId == viewModel.Id).ToList());
                NailServicePicture nailServicePicture = new NailServicePicture { PictureId = item.Id };
                images = images + item.VirtualPath + ",";
                nailServicePicture.NailServiceId = viewModel.Id;
                _nailServicePictureRepository.Insert(nailServicePicture);
            }

            foreach (var item in viewModel.Employees)
            {
                _nailServiceEmployeeRepository.RemoveMultiple(_nailServiceEmployeeRepository.GetAll(x => x.NailServiceId == viewModel.Id).ToList());
                var nailESv = new Cms.Data.Entities.Nails.NailEmployeeService { NailEmployeeId = item };
                nailESv.NailServiceId = viewModel.Id;
                _nailServiceEmployeeRepository.Insert(nailESv);
            }


            nailService.Images = images;
            _nailServiceRepository.Update(nailService);
            Save();
        }

        public bool IsDelete(int id)
        {
            var model = _nailServiceRepository.GetById(id);
            model.IsDeleted = true;
            model.DateDeleted = DateTime.Now;
            _nailServiceRepository.Update(model);
            Save();
            return true;
        }
        public bool DeleteFile(int id, string images)
        {
            var nailCategory = _nailServiceRepository.GetById(id);
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
            _nailServicePictureRepository.Delete(x => x.NailServiceId == id && x.Picture.FileName == filenameWithoutEx);
            foreach (var item in nailCategory.Images.Split(','))
            {
                if (item != filename && !string.IsNullOrEmpty(item))
                    newImages += item + ",";
            }
            nailCategory.Images = newImages;
            _nailServiceRepository.Update(nailCategory);
            Save();
            return true;
        }

        public List<NailServiceViewModel> GetbyCategoryId(int id)
        {
            var query = _nailServiceRepository.GetAll().Where(x => x.IsDeleted == false && x.IsActive && x.NailCategoryId == id);
            var nailServices = _mapper.Map<List<NailService>, List<NailServiceViewModel>>(query.ToList());
            return nailServices;
        }
    }
}
