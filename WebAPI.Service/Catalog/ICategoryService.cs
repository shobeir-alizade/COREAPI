using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Service.DTOs;

namespace WebAPI.Service.Catalog
{
    public interface ICategoryService
    {
        Task<bool> IsExistsCategoryAsync(int id);
        Task<CategoryDTO> RegisterCategoryAsync(CategoryDTO categoryDTO);
        Task RemoveCategoryAsync(int id);
        Task<IEnumerable<CategoryListItemDTO>> SearchCategoriesAsync();
        Task<CategoryListItemDTO> SearchCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(CategoryDTO categoryDTO);
    }
}