using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mapster;
using WebAPI.Core;
using WebAPI.Core.Domain;
using WebAPI.Core.Extensions;
using WebAPI.Service.DTOs;
 

namespace WebAPI.Service.Extentions
{
    public static class MappingExtentions
    {
        public static TDTO TODTO<TDTO>(this Entity entity) where TDTO : BaseDTO
        {
            if (entity == null)
                return null;
            if (typeof(TDTO).GetInterface("IDateDTO") != null && entity.GetType().GetInterface("IDateEntity") != null)
            {
                //TypeAdapterConfig<IDateEntity, TDTO>.NewConfig().Map("LocalCreateOn", p => p.CreateOn.ToPersian()).
                //    Map("LocalUpdateOn", p => p.UpdateOn.ToPersian());

               // // No other changes are needed in the file as the error is caused by the missing namespace for TypeAdapterConfig.
            }

            var dto = entity.Adapt<TDTO>();

            //if (entity is Category && dto is CategoryDTO)
            //{
            //    var category = entity as Category;
            //    var categoryDTO = dto as CategoryDTO;

            //    if (category.ParentCategory != null)
            //        categoryDTO.ParentName = category.ParentCategory?.Name;
            //}
            //if (entity is Category && dto is CategoryListItemDTO)
            //{
            //    var category = entity as Category;
            //    var categoryDTO = dto as CategoryListItemDTO;

            //    if (category.ParentCategory != null)
            //        categoryDTO.ParentName = category.ParentCategory?.Name;

            //    if (category.ProductCategories != null)
            //        categoryDTO.ProductCount = category.ProductCategories.Count;

            //    if (category.Children != null)
            //        categoryDTO.ChildCount = category.Children.Count;
            //}


            //if (entity is Product && dto is ProductListItem)
            //{
            //    var product = entity as Product;
            //    var productdto = dto as ProductListItem;

            //    productdto.LocalPublishDate = product.PublishDate.ToPersian();

            //    if(product.ProductCategories!=null)
            //    {
            //        productdto.CategoryNames = product.ProductCategories.Select(p => p.Category.Name).ToList();
            //        productdto.CategoryIds = product.ProductCategories.Select(p => p.CategoryID).ToList();

            //    }

            //}
            return dto;
        }


        public static TEntity ToEntity<TEntity>(this BaseDTO baseDTO) where TEntity : Entity
        {

            if (typeof(TEntity).GetInterface("IDateEntity") != null)
            {
                TypeAdapterConfig<BaseDTO, TEntity>.NewConfig().Ignore("CreateOn", "UpdateOn", "LocalCreateOn", "LocalUpdateOn");
            }
            var entity = baseDTO.Adapt<TEntity>();


          
            return entity;
        }

    }
}
