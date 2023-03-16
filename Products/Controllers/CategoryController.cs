using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.Models.Domain;
using Products.Repository;

namespace Products.Controllers
{
    [ApiController]
    [Route("Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //get the domain object
            var categoryDomain = await categoryRepository.GetAll();

            //convert into a DTO
            var categoryDTO = mapper.Map<List<Models.DTO.Category>>(categoryDomain);

            return Ok(categoryDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetById")]
        public async Task<IActionResult> GetByID([FromRoute]Guid id)
        {
            var category = await categoryRepository.GetById(id);

            if(category == null)
                return NotFound();
            
            //convert to DTO
            var categoryDTO = mapper.Map<Models.DTO.Category>(category);

            return Ok(categoryDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Models.DTO.AddCategoryRequest category)
        {
            //convert DTO to Domain
            var addCategory = new Models.Domain.Category
            {
                Name = category.Name,
            };

            //pass domain to the repository
            addCategory = await categoryRepository.AddCategory(addCategory);

            //convert Domain back to DTO
            var addCategoryDTO = mapper.Map<Models.DTO.Category>(addCategory);

            return Ok(addCategoryDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] Models.DTO.UpdateCategoryRequest updateCategoryRequest)
        {
            //convert dto to Domain
            var updateCategory = new Models.Domain.Category{
                Name = updateCategoryRequest.Name
            };

            updateCategory = await categoryRepository.UpdateCategory(id, updateCategory);

            //convert Domain back to DTO
            var updateCategoryDTO = mapper.Map<Models.DTO.Category> (updateCategory);


            return Ok(updateCategoryDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteCategory(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

    }
}
