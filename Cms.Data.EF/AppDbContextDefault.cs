using Cms.Data.Entities.Content;
using Cms.Data.Entities.Identity;
using Cms.Data.Entities.System;
using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using Cms.Data.EF.Configurations;
using Cms.Data.Entities.CRM;
using Cms.Data.Entities.Country;
using Cms.Data.Entities.Zaloka;
using Cms.Data.Entities.Products;
using System.Data;
using System.Data.Common;
using Cms.Data.Entities.Nails;
using Cms.Data.Entities.Media;

namespace Cms.Data.EF
{
    public class AppDbContextDefault : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContextDefault(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Identity Config

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
                .HasKey(x => x.Id);

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
                .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
               .HasKey(x => new { x.UserId });

            //modelBuilder.Entity<AppUser>(b =>
            //{
            //    b.HasIndex(u => u.PhoneNumber).HasName("PhoneNumberIndex").IsUnique();
            //    b.ToTable("AppUsers");
            //});
            #endregion Identity Config
        }
        public override int SaveChanges()
        {
            try
            {
                var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
                foreach (EntityEntry item in modified)
                {
                    if (item.Entity is IDateTracking changedOrAddedItem)
                    {
                        if (item.State == EntityState.Added)
                        {
                            changedOrAddedItem.DateCreated = DateTime.Now;
                        }
                        //  changedOrAddedItem.DateCreated = Convert.ToDateTime(item.GetDatabaseValues().GetValue<>);
                        changedOrAddedItem.DateModified = DateTime.Now;
                    }
                }
                return base.SaveChanges();
            }
            catch (DbUpdateException entityException)
            {
                throw new ModelValidationException(entityException.Message);
            }
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }

        public DbSet<AppGroup> AppGroup { get; set; }
        public DbSet<AppUserGroup> AppUserGroup { get; set; }
        public DbSet<AppRoleGroup> AppRoleGroup { get; set; }
        public DbSet<Post> Post { set; get; }
        public DbSet<Tag> Tag { set; get; }
        public DbSet<PostTag> PostTag { set; get; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostCate> PostCates { get; set; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<Language> Language { get; set; }
        public DbSet<Setting> SystemConfig { get; set; }
        public DbSet<Page> Pages { get; set; }

        
        public DbSet<AuditLog> AuditLogs { set; get; }
        //public DbSet<RoleFunction> RoleFunctions { get; set; }
        public DbSet<Feedback> Feedback { set; get; }
        public DbSet<Business> Business { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<RoleUIElement> RoleUIElements { get; set; }
        public DbSet<UIElement> UIElements { get; set; }
        public DbSet<UserBussiness> UserBussinesses { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        //public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Function> Functions { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Ward> Wards { get; set; }
        #region Products
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<ProductQuantity> ProductQuantities { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }

        public DbSet<Size> Sizes { set; get; }
        public DbSet<Color> Colors { set; get; }
        public DbSet<WholePrice> WholePrices { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        #endregion
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        #region Nails
        public DbSet<Nail> Nails { get; set; }
        public DbSet<NailType> NailType { get; set; }

        public DbSet<NailCustomer> NailCustomers { get; set; }
        public DbSet<CustomerCoupon> CustomerCoupons { get; set; }

        public DbSet<NailEmployee> NailEmployees { get; set; }
        public DbSet<NailPromotion> NailPromotions { get; set; }
        public DbSet<NailEGift> NailEGifts { get; set; }

        public DbSet<NailService> NailServices { get; set; }
        public DbSet<NailEmployeeService> NailEmployeeServices { get; set; }
        public DbSet<NailServicePicture> NailServicePictures { get; set; }

        public DbSet<NailCategory> NailCategories { get; set; }
        public DbSet<NailCategoryPicture> NailCategoryPictures { get; set; }

        public DbSet<NailOrder> NailOrders { get; set; }
        public DbSet<NailOrderDetail> NailOrderDetails { get; set; }
        public DbSet<NailStore> NailStores { get; set; }
        #endregion
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContextDefault>
        {
            public AppDbContextDefault CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var builder = new DbContextOptionsBuilder<AppDbContextDefault>();
                var connectionString = configuration.GetConnectionString("AppDbConnection");
                builder.UseSqlServer(connectionString);
                return new AppDbContextDefault(builder.Options);
            }
        }

        [Obsolete]
        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class
        {
            return Query<TQuery>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        }
        protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            //add parameters to sql
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is DbParameter parameter))
                    continue;

                sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";

                //whether parameter is output
                if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                    sql = $"{sql} output";
            }

            return sql;
        }



    }
}
