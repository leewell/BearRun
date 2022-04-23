using System;
using UnityEngine;

public class EquipSkinAndClothController : Controller
{
    public override void Excute(object data)
    {
        BuySkinAndClothArgs e = data as BuySkinAndClothArgs;
        GameModel gm = GetModel<GameModel>();
        if (e != null)
        {
            gm.TakeOnSkinAndCloth = e.buyId;
            UIShop shop = GetView<UIShop>();
            shop.UpdateSkinAndClothBuyButton(e.select);
            shop.UpdateSkinAndClothIcon();
            shop.UpdateNameAndHead();
        }
    }
}