using AutoMapper;
using Cms.Data.Entities.Content;
using Cms.Data.Entities.Country;
using Cms.Data.Entities.CRM;
using Cms.Data.Entities.Identity;
using Cms.Data.Entities.Media;
using Cms.Data.Entities.Nails;
using Cms.Data.Entities.Products;
using Cms.Data.Entities.Zaloka;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using Cms.Service.ViewModel.Country;
using Cms.Service.ViewModel.Customer;
using Cms.Service.ViewModel.Identity;
using Cms.Service.ViewModel.Media;
using Cms.Service.ViewModel.Nails;
using Cms.Service.ViewModel.Products;
using Cms.Service.ViewModel.Zaloka;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Mapping
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {

            CreateMap<AppRole, AppRoleViewModel>(MemberList.None).ReverseMap();
            CreateMap<Function, FunctionViewModel>(MemberList.None).ReverseMap();
            CreateMap<AppUser, RegisterViewModel>(MemberList.None)
                   .ForMember(a => a.AvatarFullUrl, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(src.Avatar))).ReverseMap();
            CreateMap<Permission, PermissionViewModel>(MemberList.None).ReverseMap();
            CreateMap<Page, PageViewModel>(MemberList.None).ReverseMap();
            CreateMap<Customer, CustomerViewModel>(MemberList.None).ReverseMap();

            CreateMap<Ward, WardViewModel>(MemberList.None).ReverseMap();
            CreateMap<Country, CountryViewModel>(MemberList.None).ReverseMap();
            CreateMap<District, DistrictViewModel>(MemberList.None).ReverseMap();
            CreateMap<Province, ProvinceViewModel>(MemberList.None).ReverseMap();
            //Zaloka
            CreateMap<OAInfomation, OAInfomationViewModel>(MemberList.None).ReverseMap();
            CreateMap<AppUser, Cms.Service.ViewModel.Zaloka.AppUserViewModel>(MemberList.None).ReverseMap();
            CreateMap<Message, MessageViewModel>(MemberList.None).ReverseMap();
            CreateMap<MessageGroup, MessageGroupViewModel>(MemberList.None).ReverseMap();
            CreateMap<UserOA, UserOAViewModel>(MemberList.None)
                 .ForMember(a => a.NumberDateAdded, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.SubDate(src.DateAddedGroup ?? DateTime.Now))).ReverseMap();
            CreateMap<Campaign, CampaignViewModel>(MemberList.None).ReverseMap();
            CreateMap<CampaignDetail, CampaignDetailViewModel>(MemberList.None).ReverseMap();

            CreateMap<Product, ProductViewModel>(MemberList.None)
                .ForMember(a => a.ImageFullUrl, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(src.Image))).ReverseMap();
            CreateMap<ProductCategory, ProductCategoryViewModel>(MemberList.None).ReverseMap();
            CreateMap<ProductImage, ProductImageViewModel>(MemberList.None).ReverseMap();
            CreateMap<ProductTag, ProductTag>(MemberList.None).ReverseMap();
            CreateMap<Size, SizeViewModel>(MemberList.None).ReverseMap();
            CreateMap<ProductQuantity, ProductQuantityViewModel>(MemberList.None).ReverseMap();
            CreateMap<WholePrice, WholePriceViewModel>(MemberList.None).ReverseMap();
            CreateMap<Color, ColorViewModel>(MemberList.None).ReverseMap();
            CreateMap<PostCategory, PostCategoryViewModel>(MemberList.None).ReverseMap();
            CreateMap<Post, PostViewModel>(MemberList.None)
                .ForMember(a => a.FullImageUrl, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(src.Image))).ReverseMap();
            CreateMap<Bill, BillViewModel>(MemberList.None).ReverseMap();
            CreateMap<BillDetail, BillDetailViewModel>(MemberList.None).ReverseMap();
            CreateMap<Contact, ContactViewModel>(MemberList.None).ReverseMap();
            CreateMap<Feedback, FeedbackViewModel>(MemberList.None).ReverseMap();
            CreateMap<Slide, SlideViewModel>(MemberList.None)
                   .ForMember(a => a.ImageFullUrl, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(src.Image))).ReverseMap();
            CreateMap<Nail, NailViewModel>(MemberList.None).ReverseMap();
            CreateMap<NailType, NailTypeViewModel>(MemberList.None).ReverseMap();
            CreateMap<Review, ReviewViewModel>(MemberList.None)
                  .ForMember(a => a.AvartarFullUrl, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(src.Avartar))).ReverseMap();
            CreateMap<Gallery, GalleryViewModel>(MemberList.None)
                .ForMember(a => a.FileFullUrl, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(src.FileUrl))).ReverseMap();
            CreateMap<CustomerCoupon, CustomerCouponViewModel>(MemberList.None)
                  .ForMember(a => a.NailCustomerFullName, opt => opt.MapFrom(src => src.NailCustomer.FirstName + " " + src.NailCustomer.LastName)).ReverseMap();
            CreateMap<NailCustomer, NailCustomerViewModel>(MemberList.None)
                 .ForMember(a => a.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)).ReverseMap();
            CreateMap<NailEmployee, NailEmployeeViewModel>(MemberList.None)
               .ForMember(a => a.AvatarFullUrl, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(src.Avatar))).ReverseMap();
            CreateMap<CustomerCoupon, CustomerCouponViewModel>(MemberList.None)
                  .ForMember(a => a.NailCustomerFullName, opt => opt.MapFrom(src => src.NailCustomer.FirstName + " " + src.NailCustomer.LastName)).ReverseMap();
            CreateMap<NailPromotion, NailPromotionViewModel>(MemberList.None)
             .ForMember(a => a.ImageFullUrl, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(src.Image))).ReverseMap();
            CreateMap<NailCategory, NailCategoryViewModel>(MemberList.None)
                 .ForMember(a => a.DefaultImage, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(Utilities.Helpers.UtilHepler.GetFirstSplit(src.Images), 1)))
                 .ForMember(a => a.Images, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetImages(src.Images)))
                 .ReverseMap();
            CreateMap<NailService, NailServiceViewModel>(MemberList.None)
                .ForMember(a => a.DefaultImage, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(Utilities.Helpers.UtilHepler.GetFirstSplit(src.Images), 1)))
                 .ForMember(a => a.Images, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetImages(src.Images)))
                 .ForMember(a => a.Employees, opt => opt.MapFrom(src => Employees(src.NailEmployeeServices)))
                 .ReverseMap();
            CreateMap<Picture, PictureViewModel>(MemberList.None).ReverseMap();
            CreateMap<NailCategoryPicture, NailCategoryPictureViewModel>(MemberList.None).ReverseMap();
            CreateMap<NailServicePicture, NailServicePictureViewModel>(MemberList.None).ReverseMap();
            CreateMap<NailEmployeeService, NailEmployeeServiceViewModel>(MemberList.None).ReverseMap();
            CreateMap<NailOrder, NailOrderViewModel>(MemberList.None).ReverseMap();
            CreateMap<NailOrderDetail, NailOrderDetailViewModel>(MemberList.None).ReverseMap();
            CreateMap<NailStore, NailStoreViewModel>(MemberList.None)
                  .ForMember(a => a.ImageFullUrl, opt => opt.MapFrom(src => Utilities.Helpers.UtilHepler.GetUrl(src.Image))).ReverseMap();

        }
        public List<Guid> Employees(ICollection<NailEmployeeService> employeeServices)
        {
            List<Guid> newEmployees = new List<Guid>();
            foreach (var item in employeeServices)
            {
                if (item.NailEmployeeId != Guid.Empty)
                    newEmployees.Add(item.NailEmployeeId ?? Guid.Empty);
            }
            return newEmployees;
        }
    }
}
