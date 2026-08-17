using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.APIs.Errors;
using Onion.Core.Entities;
using Onion.Core.Repositories;

namespace Onion.APIs.Controllers
{
   
    public class BasketController : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        //GetOrCreate

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>>GetCustomerBasket(string Basketid)
        {
            var Basket = await _basketRepository.GetBasketAsync(Basketid);
            //if (Basketid is null)
            //{
            //    return new CustomerBasket(Basketid);
            //}
            return Basket is null? new CustomerBasket(Basketid): Ok(Basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket Basket)
        {
            var CreatedOrUpdated = await _basketRepository.UpdateBasketAsync(Basket);
            if (CreatedOrUpdated is null)
                return BadRequest(new ApiResponse(400));
            return Ok(CreatedOrUpdated);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string Basketid)
        {
            return await _basketRepository.DeleteBasketAsync(Basketid);
        }

    }
}
