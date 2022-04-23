using System;
using UnityEngine;

public class BuySkinAndClothController : Controller
{
    public override void Excute(object data)
    {
        Debug.Log(data);
        BuySkinAndClothArgs e = data as BuySkinAndClothArgs;
        GameModel gm = GetModel<GameModel>();
        UIShop shop = GetView<UIShop>();
        if (gm.GetMoney(e.coin))
        {
            //更新数据
            gm.buySkinAndClothList.Add(e.buyId);
            //更新view
            shop.UpdateSkinAndClothBuyButton(e.select);
            shop.UpdateSkinAndClothIcon();
            shop.UpdateCoin();
        }
    }
}