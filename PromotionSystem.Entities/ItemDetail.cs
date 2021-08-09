using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionSystem.Entities
{
    public class ItemDetail
    {
        public ItemCode ItemCode { get; set; }

        public double UnitPrice { get; set; }

        public bool IsCombinedDiscountApplicable { get; set; }

        public DiscountDetail DiscountDetail { get; set; }

        public Dictionary<ItemCode,ItemCode> ItemCodeWithCombinedDiscount { get; set; }
        public bool IsActiveItem { get; set; }
    }

    public enum ItemCode
    {
        A,
        B,
        C,
        D
    }
}
