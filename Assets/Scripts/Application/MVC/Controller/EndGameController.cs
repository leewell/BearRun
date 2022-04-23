using System;
using UnityEngine;

public class EndGameController : Controller
{
    public override void Excute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        gm.IsPlay = false;

        //显示游戏结束UI
        UIDead dead = GetView<UIDead>();
        dead.Show();
    }
}