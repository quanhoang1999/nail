using Cms.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Data.EF.Configurations
{
    public class AppUserConfiguration : DbEntityConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> entity)
        {
            //entity.Property(c => c.TagId).HasMaxLength(255).IsRequired()
            //.HasColumnType("varchar(255)");
            // etc.
            //entity.HasOne(p => p.Business)
            //    .WithMany(pt => pt.AppUsers)
            //    .HasForeignKey(p => p.BusinessId);

          
        }
    }
}
