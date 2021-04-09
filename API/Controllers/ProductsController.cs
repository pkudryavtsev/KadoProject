using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductDb.DataClasses;
using Services;
using Services.Dtos;
using DAL;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Gets all products with specified parameters
        /// </summary>
        /// <param name="productParams">Product params binded from query string</param>
        /// <returns>Returns list of products</returns>
        /// <response code="200">Successfully retrieves products</response>
        /// <response code="204">No products were found with specified parameters</response>
        /// <response code="400">Fails to map product parameters</response>
        [HttpGet("Products")]
        [HttpGet("Products/Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery] ProductParams productParams)
        {
            var products = await _productService.GetProductsWithParams(productParams);

            if (products is null)
                return BadRequest();

            if (products.Count is 0)
                return NoContent();

            return Ok(products);
        }

        [HttpGet("Products/{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product is null)
                return NotFound();
            
            return Ok(product);
        } 

        [HttpPost("Products/Create")]
        public async Task<ActionResult> CreateProduct([FromBody]ProductToCreateDto productToCreate)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
                isSuccess = await _productService.AddProduct(productToCreate);

            if (!isSuccess)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("Products/Delete/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var isSuccess = await _productService.DeleteProduct(id);

            if (!isSuccess)
                return NotFound();

            return NoContent();
        }

        [HttpPut("Products/Update")]
        public async Task<ActionResult> UpdateProduct([FromBody]Product productToUpdate)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
                isSuccess =  await _productService.EditProduct(productToUpdate);

            if (!isSuccess)
                return BadRequest();

            return NoContent();
        }

        [HttpGet("Boxes")]
        public async Task<ActionResult<IReadOnlyList<BoxToReturnDto>>> GetBoxes([FromQuery] ProductParams productParams)
        {
            var boxes = await _productService.GetBoxesWithParams(productParams);

            if (boxes is null)
                return BadRequest();

            return Ok(boxes);
        }


        /// <summary>
        /// Gets all product brands
        /// </summary>
        /// <returns>Returns list of product brands</returns>
        /// <response code="200">Successfully retrieves product brands</response>
        [HttpGet("ProductBrands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productService.GetProductBrands();

            return Ok(productBrands);
        }

        // [HttpPut("ProductBrands/Edit/{id}")]
        // public async Task<ActionResult<ProductBrand>> EditProductBrand(ProductBrand productbrand)
        // {
        //     var productBrand = await _productService.GetProductBrand(productid);

        //     if (productBrand is null)
        //         return BadRequest();

        //     return Ok();
        // }

        [HttpPost("ProductBrands/Edit/{id}")]
        public async Task<ActionResult> EditProductBrand([FromBody] string productBrandName)
        {
            var isSuccess = await _productService.EditProductBrand(productBrandName);

            if (!isSuccess)
                return BadRequest();

            return Redirect("ProductBrands");
        }

        /// <summary>
        /// Gets all product types
        /// </summary>
        /// <returns>Returns list of product types</returns>
        /// <response code="200">Successfully retrieves product types</response>
        [HttpGet("ProductTypes")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productService.GetProductTypes();

            return Ok(productTypes);
        }
        
        /// <summary>
        /// Gets all product categories
        /// </summary>
        /// <returns>Returns list of product categories</returns>
        /// <response code="200">Successfully retrieves product categories</response>
        [HttpGet("ProductCategories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetProductCategories()
        {
            var productCategories = await _productService.GetCategories();

            return Ok(productCategories);
        }


    }
}