//using WebAPI.Core.Caching;
//using WebAPI.Core.Domain;
//using WebAPI.Core.Extensions;
//using WebAPI.Data;
//using WebAPI.Service.DTOs;
//using WebAPI.Service.Extentions;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebAPI.Data.Repository;

//namespace WebAPI.Service.Catalog
//{
//    public class ProductService : IProductService
//    {
//        //private readonly IRepository<Product> _repositoryProduct = null;
//        //private readonly IRepository<ProductCategory> _repositoryProductCategory = null;
//        //private readonly IRepository<ProductPicture> _repositoryProductPicture = null;
//        //private readonly ICacheManager _cacheManager = null;


//        private string ProudctSearchKey = "Product-categoryId-{0}-productName-{1}-sku-{2}";

//        public ProductService(IRepository<Product> repositoryProduct, IRepository<ProductCategory> repositoryProductCategory, IRepository<ProductPicture> repositoryProductPicture, ICacheManager cacheManager)
//        {
//            _repositoryProduct = repositoryProduct;
//            _repositoryProductCategory = repositoryProductCategory;
//            _repositoryProductPicture = repositoryProductPicture;
//            _cacheManager = cacheManager;
//        }

//        public async Task<IEnumerable<ProductListItem>> SearchProductsAsync(ProductFilterDTO productFilterDTO)
//        {
//            ProudctSearchKey = string.Format(ProudctSearchKey, productFilterDTO.CategoryId, productFilterDTO.ProductName, productFilterDTO.Sku);
//           var _list=await _cacheManager.GetAsych(ProudctSearchKey, 35, async () => {


//                var query = _repositoryProduct.TableNoTracking;
//                if (!string.IsNullOrEmpty(productFilterDTO.ProductName))
//                {
//                    query = query.Where(p => p.ProductName.Contains(productFilterDTO.ProductName));
//                }
//                if (productFilterDTO.CategoryId.HasValue && productFilterDTO.CategoryId != 0)
//                {
//                    query = query.Where(p => p.ProductCategories.Any(q => q.CategoryID == productFilterDTO.CategoryId));
//                }
//                if (productFilterDTO.FromPrice.HasValue && productFilterDTO.FromPrice != 0)
//                {
//                    query = query.Where(p => p.Price >= productFilterDTO.FromPrice);
//                }
//                if (productFilterDTO.ToPrice.HasValue && productFilterDTO.ToPrice != 0)
//                {
//                    query = query.Where(p => p.Price <= productFilterDTO.ToPrice);
//                }
//                if (productFilterDTO.IsAvailable.HasValue && productFilterDTO.IsAvailable.Value)
//                {
//                    query = query.Where(p => p.StockQuantity > 0);
//                }
//                //if (!string.IsNullOrEmpty(productFilterDTO.FromPublishDate))
//                //{
//                //    query = query.Where(p => p.PublsihDate >= productFilterDTO.FromPublishDate.ToDateTime());
//                //}
//                //if (!string.IsNullOrEmpty(productFilterDTO.ToPublishDate))
//                //{
//                //    query = query.Where(p => p.PublsihDate <= productFilterDTO.ToPublishDate.ToDateTime());
//                //}
//                if (!string.IsNullOrEmpty(productFilterDTO.Sku))
//                {
//                    query = query.Where(p => p.Sku == productFilterDTO.Sku);
//                }

//               return  await query.Where(p => !p.Deleted)
//                  .Select(p => new ProductListItem
//                  {
//                      CreateOn = p.CreateOn,
//                      UpdateOn = p.UpdateOn,
//                      LocalPublishDate = p.PublishDate.ToPersian(),
//                      PublishDate = p.PublishDate,
//                      Price = p.Price,
//                      ProductName = p.ProductName,
//                      Sku = p.Sku,
//                      StockQuantity = p.StockQuantity,
//                      ID = p.ID,
//                      LocalCreateOn = p.CreateOn.ToPersian(),
//                      LocalUpdateOn = p.UpdateOn.ToPersian(),
//                      CategoryNames = p.ProductCategories.Select(x => x.Category.Name).ToList(),
//                      CategoryIds = p.ProductCategories.Select(x => x.CategoryID).ToList(),
//                  }).ToListAsync();

//            });

//            return _list;
//        }


//        public async Task<IEnumerable<ProductListItem>> SearchUnAvailableProductAsync()
//        {

//            var _list = await _repositoryProduct.TableNoTracking.Where(p => !p.Deleted && p.StockQuantity<=0)
//              .Select(p => new ProductListItem
//              {
//                  CreateOn = p.CreateOn,
//                  UpdateOn = p.UpdateOn,
//                  LocalPublishDate = p.PublishDate.ToPersian(),
//                  PublishDate = p.PublishDate,
//                  Price = p.Price,
//                  ProductName = p.ProductName,
//                  Sku = p.Sku,
//                  StockQuantity = p.StockQuantity,
//                  ID = p.ID,
//                  LocalCreateOn = p.CreateOn.ToPersian(),
//                  LocalUpdateOn = p.UpdateOn.ToPersian(),
//                  CategoryNames = p.ProductCategories.Select(x => x.Category.Name).ToList(),
//                  CategoryIds = p.ProductCategories.Select(x => x.CategoryID).ToList(),
//              }).ToListAsync();

//            return _list;
//        }

//        public async Task<IEnumerable<ProductListItem>> SearchAllProductsAsync()
//        {

//            var _list = await _repositoryProduct.TableNoTracking.Where(p => !p.Deleted)
//              .Select(p => new ProductListItem
//              {
//                  CreateOn = p.CreateOn,
//                  UpdateOn = p.UpdateOn,
//                  LocalPublishDate = p.PublishDate.ToPersian(),
//                  PublishDate = p.PublishDate,
//                  Price = p.Price,
//                  ProductName = p.ProductName,
//                  Sku = p.Sku,
//                  StockQuantity = p.StockQuantity,
//                  ID = p.ID,
//                  LocalCreateOn = p.CreateOn.ToPersian(),
//                  LocalUpdateOn = p.UpdateOn.ToPersian(),
//                  CategoryNames = p.ProductCategories.Select(x => x.Category.Name).ToList(),
//                  CategoryIds = p.ProductCategories.Select(x => x.CategoryID).ToList(),
//              }).ToListAsync();

//            return _list;
//        }

//        public async Task<ProductListItem> SearchPRoductByIdAsync(int id)
//        {
//            var product = await _repositoryProduct.GetByIdAsync(id);

//            return product.TODTO<ProductListItem>();
//        }

//        public async Task<bool> IsExistsProdcutAsync(int id)
//        {
//            var product = await _repositoryProduct.GetByIdAsNoTrackingAsync(id);
//            if (product == null)
//                return false;

//            return true;
//        }

//        public async Task<ProductDTO> RegisterProductAsync(ProductDTO productDTO)
//        {
//            var product = productDTO.ToEntity<Product>();

//            await _repositoryProduct.InsertAsync(product);

//            productDTO.ID = product.ID;

//            return productDTO;
//        }

//        public async Task RemoveProductAsync(int id)
//        {
//            var product = _repositoryProduct.GetById(id);
//            product.Deleted = true;
//            await _repositoryProduct.UpdateAsync(product);

         
//        }

//        public async Task UpdateProductAsync(ProductDTO productDTO)
//        {

//            var product = _repositoryProduct.GetById(productDTO.ID);

//            product.Price = productDTO.Price;
//            product.ProductName = productDTO.ProductName;
//            product.PublishDate = productDTO.PublsihDate;
//            product.Sku = productDTO.Sku;
//            product.StockQuantity = productDTO.StockQuantity;

//            await _repositoryProduct.UpdateAsync(product);
//        }

//        public async Task UpdateProductStockQuantityAsync(ProductStockQuantityDTO productStockQuantityDTO)
//        {

//            var product = _repositoryProduct.GetById(productStockQuantityDTO.ID);

//            product.StockQuantity = productStockQuantityDTO.StockQuantity;

//            await _repositoryProduct.UpdateAsync(product);
//        }

//        public async Task AddProductToCategory(ProductCategoryDTO productCategoryDTO)
//        {
//            var productCategory = productCategoryDTO.ToEntity<ProductCategory>();

//            await _repositoryProductCategory.InsertAsync(productCategory);
//        }
//        public async Task RemoveProductToCategory(ProductCategoryDTO productCategoryDTO)
//        {
//            var productCategory = productCategoryDTO.ToEntity<ProductCategory>();

//            await _repositoryProductCategory.DeleteAsync(productCategory);
//        }

//        public async Task AddPictureForProduct(ProductPictureDTO productPictureDTO)
//        {
//            var productPicture = productPictureDTO.ToEntity<ProductPicture>();
//            await _repositoryProductPicture.InsertAsync(productPicture);
//        }

//        public async Task RemovePictureForProduct(ProductPictureDTO productPictureDTO)
//        {
//            var productPicture = productPictureDTO.ToEntity<ProductPicture>();
//            await _repositoryProductPicture.DeleteAsync(productPicture);
//        }

//        public async Task<IEnumerable<int>> GetPicturesForProductAsync(int ProductID)
//        {
//            return await _repositoryProductPicture.TableNoTracking.Where(p => p.ProductID == ProductID).Select(p => p.PictureID).ToListAsync();
//        }

//        public string GetProductName(int ProdId)
//        {
//            string name;
//            if (ProdId == 1)
//            {
//                name = "Mobile";
//            }
//            else if (ProdId == 2)
//            {
//                name = "Shirt";
//            }
//            else
//            {
//                name = "Not Found";
//            }
//            return name;
//        }
//        public int GetPriceByDiscount(int ProductID)
//        {
       
//            var prod = _repositoryProduct.GetById(ProductID);

//            var cats = prod.ProductCategories.Where(p => p.Category.DiscountAmount != 0).ToList();
//            if (cats == null || cats.Count == 0)
//                return prod.Price;

//            var dis = cats.Max(p => p.Category.DiscountAmount);
//            return (prod.Price - ((prod.Price * dis) / 100));
//        }
//    }
//}
