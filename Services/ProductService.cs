using ProductDb.DataClasses;
using System.Threading.Tasks;
using System.Collections.Generic;
using Services.Dtos;
using AutoMapper;
using Services.Helpers;
using DAL;
using System.Linq;
using DAL.Specifications;
using DAL.Repository.Products;
using DAL.Repository.ProductBrands;
using DAL.Repository.Boxes;
using DAL.Repository.ProductTypes;
using DAL.Repository.Categories;

namespace Services
{
    public class ProductService
    {
        private readonly IMapper _mapper;
        private readonly Repo _repo;
        public ProductService(Repo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductToReturnDto>> GetProductsWithParams(ProductParams productParams)
        {
            var specification = new ProductFilterSpecification(productParams);

            var products = await _repo.GetProductsWithSpecification(null);

            return _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        }

        public async Task<ProductToReturnDto> GetProductById(int id)
        {
            var product = await _repo.GetProductById(id);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        public async Task<bool> AddProduct(ProductToCreateDto productToCreate)
        {
            var product = _mapper.Map<ProductToCreateDto, Product>(productToCreate);

            bool isAdded = await _repo.CreateProduct(product);

            return isAdded;
        }

        public Task<bool> EditProduct(Product productToUpdate)
        {
            //var product = _mapper.Map<ProductToCreateDto, Product>(productToUpdate);

            Task<bool> isEdited = _repo.UpdateProduct(productToUpdate);

            return isEdited;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var isDeleted = await _repo.RemoveProduct(id);

            return isDeleted;
        }

        public async Task<bool> AddProductBrand(string productBrandName)
        {
            var productBrand = new ProductBrand {Name = productBrandName};

            bool isAdded = await _repo.CreateProductBrand(productBrand);

            return isAdded;
        }
        
        public async Task<bool> EditProductBrand(string productBrandName)
        {
            var productBrand = new ProductBrand {Name = productBrandName};

            bool isAdded = await _repo.UpdateProductBrand(productBrand);

            return isAdded;
        }

        public async Task<bool> DeleteProductBrand(int id)
        {
            bool isAdded = await _repo.RemoveProductBrand(id);

            return isAdded;
        }

        public async Task<IReadOnlyList<BoxToReturnDto>> GetBoxesWithParams(ProductParams productParams)
        {
            var specification = new BoxFilterSpecification(productParams);

            var boxes = await _repo.GetBoxesWithSpecification(specification);

            return _mapper.Map<IReadOnlyList<Box>, IReadOnlyList<BoxToReturnDto>>(boxes);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
        {
            var brands = await _repo.GetProductBrands();

            return brands;
        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypes()
        {
            var brands = await _repo.GetProductTypes();

            return brands;
        }
        public async Task<IReadOnlyList<Category>> GetCategories()
        {
            var brands = await _repo.GetCategories();

            return brands;
        }
    }
}