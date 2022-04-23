using System;
using UnityEngine;

public class BuyFootballController : Controller
{
    public override void Excute(object data)
    {
        BuyFootballArgs e = data as BuyFootballArgs;
        GameModel gm = GetModel<GameModel>();
        if (e != null && gm.GetMoney(e.coin))
        {
            //把购买的道具加到列表里
            gm.buyFootball.Add(e.selectIndex);
            //更新UI
            UIShop uiShop = GetView<UIShop>();
            uiShop.UpdateBuyFootballButton(e.selectIndex);
            uiShop.UpdateBuyIcon();
            uiShop.UpdateCoin();
        }
    }
}