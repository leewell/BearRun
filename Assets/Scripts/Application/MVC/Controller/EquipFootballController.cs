using System;
using UnityEngine;

public class EquipFootballController : Controller
{
    public override void Excute(object data)
    {
        BuyFootballArgs buyFootballArgs = data as BuyFootballArgs;
        if (buyFootballArgs != null)
        {
            GameModel gm = GetModel<GameModel>();
            gm.TakeOnFootball = buyFootballArgs.selectIndex;
            UIShop shop = GetView<UIShop>();
            shop.UpdateBuyFootballButton(buyFootballArgs.selectIndex);
            shop.UpdateBuyIcon();

        }
    }
}