 
using WebAPI.Core.Extensions;
using WebAPI.Data;
using WebAPI.Service.DTOs;
using WebAPI.Service.Extentions;
using Microsoft.EntityFrameworkCore;
 
using WebAPI.Core.Domain;
using WebAPI.Data.Repository;
using WebAPI.Service.Catalog;

namespace WebAPI.Service.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repositoryCategory ;

        public CategoryService(IRepository<Category> repositoryCategory)
        {
            _repositoryCategory = repositoryCategory;
        }


        public async Task<IEnumerable<CategoryListItemDTO>> SearchCategoriesAsync()
        {
            
            //var _list = await _repositoryCategory.TableNoTracking
            //  .Select(p => new CategoryListItemDTO
            //  {
            //      CreateOn = p.CreateOn,
            //      ID = p.ID,
            //      Name = p.Name,
            //      ParentId = p.ParentId,
            //      ParentName = p.ParentCategory.Name,
            //      UpdateOn = p.UpdateOn,
            //      ChildCount = p.Children.Count,
            //      ProductCount = p.ProductCategories.Count,
            //      LocalCreateOn = p.CreateOn.ToPersian(),
            //      LocalUpdateOn = p.UpdateOn.ToPersian()
            //  }).ToListAsync();


            //return _list;
            return  default(IEnumerable<CategoryListItemDTO>);
        }

        public async Task<CategoryListItemDTO> SearchCategoryByIdAsync(int id)
        {
            var category = await _repositoryCategory.GetByIdAsync(id);
           
            return category.TODTO<CategoryListItemDTO>();
        }

        public async Task<bool> IsExistsCategoryAsync(int id)
        {
           var category= await _repositoryCategory.GetByIdAsNoTrackingAsync(id);
            if (category == null)
                return false;

            return true;
        }

        public async Task<CategoryDTO> RegisterCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = categoryDTO.ToEntity<Category>();
            await _repositoryCategory.InsertAsync(category);
            //categoryDTO.ID = category.ID;

            return categoryDTO;
        }

        public async Task RemoveCategoryAsync(int id)
        {
            var category = _repositoryCategory.GetById(id);
           await _repositoryCategory.DeleteAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            // var category = categoryDTO.ToEntity<Devsharp.Core.Domian.Category>();

            var category = _repositoryCategory.GetById(categoryDTO.ID);
            //category.Name = categoryDTO.Name;
            //category.ParentId = categoryDTO.ParentId;

            await _repositoryCategory.UpdateAsync(category);
        }
    }
}
