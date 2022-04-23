using System;
using UnityEngine;

public class BriberyController : Controller
{
    public override void Excute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        CoinArgs e = data as CoinArgs;
        if (gm.GetMoney(e.coin))
        {
            UIDead dead = GetView<UIDead>();
            dead.Hide();
            dead.BriberyCount++;
            UIResume resume = GetView<UIResume>();
            resume.StartCount();
        }
        
    }
}