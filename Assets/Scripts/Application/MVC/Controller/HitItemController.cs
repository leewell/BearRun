using System;
using UnityEngine;

public class HitItemController : Controller
{
    public override void Excute(object data)
    {
        PlayerMove player = GetView<PlayerMove>();
        UIBoard ui = GetView<UIBoard>();
        GameModel gm = GetModel<GameModel>();
        ItemArgs args = data as ItemArgs;
        switch (args.kind)
        {
            case ItemKind.ItemInvincible:
                player.HitInvincible();
                ui.HitInvincible();
                gm.Invincible -= args.hitCount;
                break;
            case ItemKind.ItemMagnet:
                player.HitMagnet();
                ui.HitMagnet();
                gm.Magnet -= args.hitCount;
                break;
            case ItemKind.ItemCoinMultiply:
                player.HitCoinMultiply();
                ui.HitCoinMultiply();
                gm.CoinMultiply -= args.hitCount;
                break;
            default:
                break;
        }
        ui.UpdateUI();
    }
}