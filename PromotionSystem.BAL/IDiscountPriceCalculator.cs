using PromotionSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionSystem.BAL
{
   public interface IDiscountPriceCalculator
    {
        double CalculateDiscountedPriceForCombinedItemCodes(List<CartItem> cartItems, CartItem currentCartItem);
        double CalculateDiscountPriceForSingleItemCode(CartItem currentCartItem);
    }
}
