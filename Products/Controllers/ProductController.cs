using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.Repository;

namespace Products.Controllers
{
    [ApiController]
    [Route("Products")]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Fetch data from domain 
            var products = await productRepository.GetAll();

            //convert domain to dto
            var walksDTO = mapper.Map<List<Models.DTO.Product>>(products);
            
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            //get walks domain object from database
            var productDomain = await productRepository.GetById(id);

            if (productDomain == null) 
                return NotFound();

            //convert to DTO
            var productDTO = mapper.Map<Models.DTO.Product>(productDomain);

            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Models.DTO.AddProductRequest addProductRequest)
        {
            //convert dto to domain
            var productDomain = new Models.Domain.Product
            {
                Name = addProductRequest.Name,
                Price = addProductRequest.Price,
                Description = addProductRequest.Description,
                Stock = addProductRequest.Stock,
                SellerId = addProductRequest.SellerId,
                CategoryId = addProductRequest.CategoryId,  
            };

            productDomain = await productRepository.AddProduct(productDomain);

            //convert domain to dto
            var productDTO = new Models.DTO.Product
            {
                Name=productDomain.Name,
                Price = productDomain.Price,
                Description = productDomain.Description,
                Stock = productDomain.Stock,
                SellerId = productDomain.SellerId,
                CategoryId = productDomain.CategoryId,  
            };

            return Ok(productDTO);
        }

        

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateProduct([FromBody] Models.DTO.UpdateProductRequest updateProductRequest, [FromRoute] Guid id)
        {
            //convert DTO to Domain
            var exisitingProduct = new Models.Domain.Product
            {
                Name = updateProductRequest.Name,
                Price = updateProductRequest.Price,
                Description = updateProductRequest.Description,
                Stock = updateProductRequest.Stock,
                SellerId = updateProductRequest.SellerId,
                CategoryId = updateProductRequest.CategoryId,
            };

            //passing domain object to the repository
            exisitingProduct = await productRepository.UpdateProduct(id, exisitingProduct);

            if(exisitingProduct == null)
                return NotFound();

            //convert domain to DTO
            var productDTO = mapper.Map<Models.DTO.Product>(exisitingProduct);

            return Ok(productDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var product = await productRepository.DeleteProduct(id);

            if(product == null)
                return NotFound();

            var walkDTO = mapper.Map<Models.DTO.Product> (product);

            return Ok(walkDTO);
        }
    }
}
