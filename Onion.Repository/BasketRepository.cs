using Onion.Core.Entities;
using Onion.Core.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Onion.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private IDatabase _Database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _Database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _Database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var Basket = await _Database.StringGetAsync(basketId);
            if (Basket.IsNull) return null;
            return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var JsonBasket = JsonSerializer.Serialize<CustomerBasket>(Basket);
           var CreatedOrUpdated= await _Database.StringSetAsync(Basket.Id, JsonBasket, TimeSpan.FromDays(2));
            if (!CreatedOrUpdated) return null;
            return await GetBasketAsync(Basket.Id);

        }
    }
}
