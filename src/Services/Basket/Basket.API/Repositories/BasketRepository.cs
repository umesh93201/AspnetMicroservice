using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository: IBasketRepository
    {
        private readonly IDistributedCache _rediscache;
        public BasketRepository(IDistributedCache rediscache)
        {
            _rediscache = rediscache?? throw new ArgumentNullException(nameof(rediscache));
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _rediscache.GetStringAsync(username);
            if (String.IsNullOrEmpty(basket))
                return null;
          return  JsonConvert.DeserializeObject<ShoppingCart>(basket);
            
        }

        public async Task RemoveBasket(string username)
        {
            await _rediscache.RemoveAsync(username);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart cart)
        {
            var basket = JsonConvert.SerializeObject(cart);
            await _rediscache.SetStringAsync(cart.Username, basket);

            return await GetBasket(cart.Username);
        }
    }
}
