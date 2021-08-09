using PromotionSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotioSystem.DAL
{
    public class ItemRepository:IItemRepository
    {
        IList<ItemDetail> MasterListOfItemsDetail = new List<ItemDetail>();

        public ItemRepository()
        {
            AddItemDetailByItemCode(null);
        }

        public async Task<bool> AddItemDetailByItemCode(ItemDetail itemDetail)
        {
            ItemDetail itemDetail_A = new ItemDetail
            {
                ItemCode = ItemCode.A,
                IsCombinedDiscountApplicable = false,
                UnitPrice = 50,
                DiscountDetail = new DiscountDetail { QuantityRequiredForDiscount = 3, DiscountPrice = 130 },
                IsActiveItem = true

            };


            ItemDetail itemDetail_B = new ItemDetail
            {
                ItemCode = ItemCode.B,
                IsCombinedDiscountApplicable = false,
                UnitPrice = 30,
                DiscountDetail = new DiscountDetail { QuantityRequiredForDiscount = 2, DiscountPrice = 45 },
                IsActiveItem = true

            };

            ItemDetail itemDetail_C = new ItemDetail
            {
                ItemCode = ItemCode.C,
                IsCombinedDiscountApplicable = true,
                UnitPrice = 20,
                DiscountDetail = new DiscountDetail { QuantityRequiredForDiscount = 0, DiscountPrice = 30 },
                ItemCodeWithCombinedDiscount = new Dictionary<ItemCode, ItemCode>() { {ItemCode.C, ItemCode.D} },
                
                IsActiveItem = true

            };

            ItemDetail itemDetail_D = new ItemDetail
            {
                ItemCode = ItemCode.D,
                IsCombinedDiscountApplicable = false,
                UnitPrice = 15,
                DiscountDetail = new DiscountDetail { QuantityRequiredForDiscount = 0, DiscountPrice = 0 },
                IsActiveItem = true

            };
            //Adding data to this list so thatbcan be accessed for data fetch considering as main data source
            MasterListOfItemsDetail.Add(itemDetail_A);
            MasterListOfItemsDetail.Add(itemDetail_B);
            MasterListOfItemsDetail.Add(itemDetail_C);
            MasterListOfItemsDetail.Add(itemDetail_D);
            return true;


        }

        public async Task<ItemDetail> DeleteItemDetailByItemCode(ItemCode itemCode)
        {
            var foundItem = MasterListOfItemsDetail.FirstOrDefault(x => x.ItemCode == itemCode);
            foundItem.IsActiveItem = false;
            return foundItem;
        }

        public async Task<ItemDetail> GetItemDetailByItemCode(ItemCode itemCode)
        {
            //for now it is sync call but in case of actual DB call it will be sync call
            return MasterListOfItemsDetail.FirstOrDefault(x => x.ItemCode == itemCode);
        }

        public async Task<List<ItemDetail>>GetItemDetailByItemCodes(List<ItemCode> itemCodes)
        {
            //for now it is async call but in case of actual DB call it will be a sync call
            List<ItemDetail> itemDetailList = new List<ItemDetail>();
            foreach(var itemcode in itemCodes)
            {
                itemDetailList.Add(MasterListOfItemsDetail.FirstOrDefault(x => x.ItemCode == itemcode));
            }
            return itemDetailList;
        }
    }
}
