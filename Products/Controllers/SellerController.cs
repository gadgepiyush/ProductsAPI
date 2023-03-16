using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.Repository;

namespace Products.Controllers
{
    [ApiController]
    [Route("Seller")]
    public class SellerController : Controller
    {
        private readonly ISellerRepository sellerRepository;
        private readonly IMapper mapper;

        public SellerController(ISellerRepository sellerRepository, IMapper mapper)
        {
            this.sellerRepository = sellerRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //fetch details from repository
            var sellers = await sellerRepository.GetAll();

            //convert to dto
            var sellerDTO = mapper.Map<List<Models.DTO.Seller>>(sellers);

            return Ok(sellerDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetSellerAsync")]
        public async Task<IActionResult> GetSeller([FromRoute] Guid id)
        {
            //fetch data from repo Domain
            var sellerDomain = await sellerRepository.GetById(id);

            //convert to DTO
            var sellerDTO = mapper.Map <Models.DTO.Seller>(sellerDomain);

            return Ok(sellerDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddSeller([FromBody]Models.DTO.AddSellerRequest addSellerRequest)
        {
            //convert DTO to Domain 
            var sellerDomain = new Models.Domain.Seller
            {
                Name = addSellerRequest.Name,
                City = addSellerRequest.City,
                State = addSellerRequest.State,
            };

            
            //pass Domain object to repo to persist data
           sellerDomain = await sellerRepository.AddSeller(sellerDomain);

            //convert domain back to DTO
            var sellerDTO = mapper.Map<Models.DTO.Seller>(sellerDomain);
            
            return Ok(sellerDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateSeller([FromRoute] Guid id, [FromBody] Models.DTO.UpdateSellerRequest updateSellerRequest)
        {
            //Convert DTO to Domain
            var sellerDomain = new Models.Domain.Seller
            {
                Name = updateSellerRequest.Name,
                City = updateSellerRequest.City,
                State = updateSellerRequest.State,
            };

            sellerDomain = await sellerRepository.UpdateSeller(id, sellerDomain);

            if(sellerDomain == null) 
                return NotFound();

            var sellerDTO = mapper.Map<Models.DTO.Seller>(sellerDomain);

            return Ok(sellerDTO);
        }



        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteSeller([FromRoute] Guid id)
        {
            var seller = await sellerRepository.DeleteSeller(id);

            if(seller == null)
                return NotFound();

            var sellerDTO = mapper.Map<Models.DTO.Seller>(seller);

            return Ok(sellerDTO);
        }

    }
}
