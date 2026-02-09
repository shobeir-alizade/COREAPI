 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Core.Domain;

namespace Devsharp.Data.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //builder.HasMany(p => p.Children).WithOne(p => p.ParentCategory).
            //    HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.NoAction);

            //builder.HasData(new Category { 
            //ID=1,
            //CreateOn=DateTime.Now,
            //Name="دسته",
            //ParentId=1,
            //UpdateOn=DateTime.Now
            //});
        }
    }
}
