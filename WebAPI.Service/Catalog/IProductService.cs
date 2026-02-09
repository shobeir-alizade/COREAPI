//using System.Collections.Generic;
//using System.Threading.Tasks;
//using WebAPI.Service.DTOs;

//namespace WebAPI.Service.Catalog
//{
//    public interface IProductService
//    {
//        Task AddPictureForProduct(ProductPictureDTO productPictureDTO);
//        Task AddProductToCategory(ProductCategoryDTO productCategoryDTO);
//        Task<IEnumerable<int>> GetPicturesForProductAsync(int ProductID);
//        int GetPriceByDiscount(int ProductID);
//        string GetProductName(int ProdId);
//        Task<bool> IsExistsProdcutAsync(int id);
//        Task<ProductDTO> RegisterProductAsync(ProductDTO productDTO);
//        Task RemovePictureForProduct(ProductPictureDTO productPictureDTO);
//        Task RemoveProductAsync(int id);
//        Task RemoveProductToCategory(ProductCategoryDTO productCategoryDTO);
//        Task<IEnumerable<ProductListItem>> SearchAllProductsAsync();
//        Task<ProductListItem> SearchPRoductByIdAsync(int id);
//        Task<IEnumerable<ProductListItem>> SearchProductsAsync(ProductFilterDTO productFilterDTO);
//        Task<IEnumerable<ProductListItem>> SearchUnAvailableProductAsync();
//        Task UpdateProductAsync(ProductDTO productDTO);
//        Task UpdateProductStockQuantityAsync(ProductStockQuantityDTO productStockQuantityDTO);
//    }
//}