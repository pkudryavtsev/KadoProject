using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductDb.DataClasses;
using Repository;

namespace API.Controllers
{

    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly RP _repo;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, RP repo, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("products")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var products = await _repo.GetAllProducts();

            var result = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(result);
        }

        [HttpGet("boxes")]
        public async Task<ActionResult<IReadOnlyList<Box>>> GetBoxes()
        {
            var products = await _repo.GetAllBoxes();

            return Ok(products);
        }

        [HttpGet("productBrands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _repo.GetProductBrands();

            return Ok(productBrands);
        }

        [HttpGet("productTypes")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _repo.GetProductTypes();

            return Ok(productTypes);
        }

        [HttpGet("productCategories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetProductCategories()
        {
            var productCategories = await _repo.GetProductCategories();

            return Ok(productCategories);
        }

        
    }
}