using PromotionSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PromotionSystem.BAL
{
    public interface IItemDetailManager
    {
        Task<List<CartItem>> FetchItemDetailForItemInCart(List<CartItem> inputCartItems);
    }
}
