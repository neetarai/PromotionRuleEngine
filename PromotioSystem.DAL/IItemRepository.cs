using PromotionSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PromotioSystem.DAL
{
    public interface IItemRepository
    {
        Task<ItemDetail> GetItemDetailByItemCode(ItemCode itemCode);

        Task<List<ItemDetail>> GetItemDetailByItemCodes(List<ItemCode> itemCodes);


        Task<bool> AddItemDetailByItemCode(ItemDetail itemDetail);

        Task<ItemDetail> DeleteItemDetailByItemCode(ItemCode itemCode);
    }
}
