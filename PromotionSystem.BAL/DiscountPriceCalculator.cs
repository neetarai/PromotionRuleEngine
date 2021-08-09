using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PromotionSystem.Entities;
using PromotioSystem.DAL;

namespace PromotionSystem.BAL
{
    public class DiscountPriceCalculator:IDiscountPriceCalculator

    {
        IItemRepository _ItemRepository;

        public DiscountPriceCalculator(IItemRepository itemRepository)
        {
            _ItemRepository = itemRepository;
        }

        public double CalculateDiscountedPriceForCombinedItemCodes(List<CartItem> cartItems,CartItem currentCartItem)
        
        {
            double totalPrice = 0;List<ItemCode> otherCombinedItemCodesHavingOffer = new List<ItemCode>();
            Dictionary<ItemCode, int> quantityItemCodeInCartHavingCombinedOffer = new Dictionary<ItemCode, int>();
            quantityItemCodeInCartHavingCombinedOffer.Add(currentCartItem.ItemType.ItemCode, currentCartItem.Quantity);
            foreach(var cardItem in cartItems)
            {
                if(currentCartItem.ItemType.ItemCodeWithCombinedDiscount.Values.Contains(cardItem.ItemType.ItemCode))
                {
                    quantityItemCodeInCartHavingCombinedOffer.Add(cardItem.ItemType.ItemCode, cardItem.Quantity);

                }
            }

            int minQuant = quantityItemCodeInCartHavingCombinedOffer.Min(x => x.Value);
            double unitPrice = 0;
            foreach (var keyValuePair in quantityItemCodeInCartHavingCombinedOffer)
            {
                unitPrice = cartItems.First(x => x.ItemType.ItemCode == keyValuePair.Key).ItemType.UnitPrice;
                totalPrice = totalPrice + (keyValuePair.Value - minQuant) * unitPrice;
                cartItems.Remove(cartItems.First(x => x.ItemType.ItemCode == keyValuePair.Key));
            }
            totalPrice = totalPrice + minQuant * currentCartItem.ItemType.DiscountDetail.DiscountPrice;
                return totalPrice;
        }

        public double CalculateDiscountPriceForSingleItemCode(CartItem currentCartItem)
        {
            double totalPrice = 0;
            if(currentCartItem.Quantity>=currentCartItem.ItemType.DiscountDetail.QuantityRequiredForDiscount&&
                currentCartItem.ItemType.DiscountDetail.QuantityRequiredForDiscount!=0)
            {
                totalPrice = (currentCartItem.Quantity / currentCartItem.ItemType.DiscountDetail.QuantityRequiredForDiscount *
                    currentCartItem.ItemType.DiscountDetail.DiscountPrice) +
                   (currentCartItem.Quantity % currentCartItem.ItemType.DiscountDetail.QuantityRequiredForDiscount *
                    currentCartItem.ItemType.UnitPrice);
            }
            else
            {
                totalPrice = currentCartItem.Quantity * currentCartItem.ItemType.UnitPrice;
            }

            return totalPrice;
        }
    }
}
