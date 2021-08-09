using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionSystem.BAL;
using PromotionSystem.Client.Models;
using PromotionSystem.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionSystem.Client.Controllers
{
    public class CartController : Controller
    {
        //Client request object for now hardcoded
        List<CartItem> itemAddedByClient = new List<CartItem>()
        {
            new CartItem{Quantity=3,ItemType=new ItemDetail{ItemCode=ItemCode.A}},
             new CartItem{Quantity=5,ItemType=new ItemDetail{ItemCode=ItemCode.B}},
              new CartItem{Quantity=1,ItemType=new ItemDetail{ItemCode=ItemCode.C} },
               new CartItem{Quantity=1,ItemType=new ItemDetail{ItemCode=ItemCode.D} }
        };
        private readonly ILogger<CartController> _logger;
        private readonly IDiscountPriceCalculator _discountPriceCalculator;
        private readonly IItemDetailManager _itemDetailManager;

        public CartController(ILogger<CartController> logger, IDiscountPriceCalculator discountPriceCalculator,
          IItemDetailManager itemDetailManager)
        {
            _logger = logger;
            _discountPriceCalculator = discountPriceCalculator;
            _itemDetailManager = itemDetailManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<double> CheckOut()
        {
            double totalPrice = 0;
            itemAddedByClient = await _itemDetailManager.FetchItemDetailForItemInCart(itemAddedByClient);
            for(int i=0; i<itemAddedByClient.Count;i++)
            {
                if(itemAddedByClient[i].ItemType.IsCombinedDiscountApplicable 
                    && itemAddedByClient[i].ItemType.ItemCodeWithCombinedDiscount!=null)
                {
                    bool isAllItemHavingCombinedDiscountAvailableInCart = false;
                    var matchedCartItems = new List<CartItem>();
                    foreach (var item in itemAddedByClient[i].ItemType.ItemCodeWithCombinedDiscount.Values)
                    {
                        if (itemAddedByClient.Find(x => x.ItemType.ItemCode == item) != null)
                            matchedCartItems.Add(itemAddedByClient.Find(x => x.ItemType.ItemCode == item));
                    }
                    if(matchedCartItems.Count==itemAddedByClient[i].ItemType.ItemCodeWithCombinedDiscount.Values.Count)
                    {
                        isAllItemHavingCombinedDiscountAvailableInCart = true;
                    }
                    if(isAllItemHavingCombinedDiscountAvailableInCart)
                    {
                        totalPrice += _discountPriceCalculator.CalculateDiscountedPriceForCombinedItemCodes(itemAddedByClient, itemAddedByClient[i]);
                    }
                    else
                    {
                        totalPrice += _discountPriceCalculator.CalculateDiscountPriceForSingleItemCode(itemAddedByClient[i]);
                    }
                    
                }
                
                    else
                    {
                        totalPrice += _discountPriceCalculator.CalculateDiscountPriceForSingleItemCode(itemAddedByClient[i]);
                    }

                
            }

            return totalPrice;

        }
       
    }
}
