using System;
using UnityEngine;

public class BuyToolsController : Controller
{
    public override void Excute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        UIBuyTools buyTools = GetView<UIBuyTools>();
        BuyToolsArgs e = data as BuyToolsArgs;
        switch (e.kind)
        {
            case ItemKind.ItemInvincible:
                if (gm.GetMoney(e.coin))
                {
                    gm.Invincible++;
                }
                break;
            case ItemKind.ItemMagnet:
                if (gm.GetMoney(e.coin))
                {
                    gm.Magnet++;
                }
                break;
            case ItemKind.ItemCoinMultiply:
                if (gm.GetMoney(e.coin))
                {
                    gm.CoinMultiply++;
                }
                break;
            default:
                break;
        }
        buyTools.InitUI();
    }
}