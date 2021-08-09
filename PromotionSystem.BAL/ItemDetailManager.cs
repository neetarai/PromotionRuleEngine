using PromotionSystem.Entities;
using PromotioSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionSystem.BAL
{
   public class ItemDetailManager : IItemDetailManager
    {
        IItemRepository _itemRepository;
        public ItemDetailManager(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<List<CartItem>> FetchItemDetailForItemInCart(List<CartItem> inputCartItems)
        {
            List<CartItem> cartItemWithItemDetails = new List<CartItem>();
            List<ItemCode> itemCodes = new List<ItemCode>();
            foreach(var item in inputCartItems)
            {
                itemCodes.Add(item.ItemType.ItemCode);
            }
            var itemDetailsFromDB = await _itemRepository.GetItemDetailByItemCodes(itemCodes);

            foreach (var item in inputCartItems)
            {
                item.ItemType = itemDetailsFromDB.FirstOrDefault(x => x.ItemCode == item.ItemType.ItemCode);
                cartItemWithItemDetails.Add(item);
            }

            return cartItemWithItemDetails;
        }
    }
}
