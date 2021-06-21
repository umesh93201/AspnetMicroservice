using Basket.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart> GetBasket(string userName);

        public Task<ShoppingCart> UpdateBasket(ShoppingCart basket);

        Task RemoveBasket(string userName);
    }
}
