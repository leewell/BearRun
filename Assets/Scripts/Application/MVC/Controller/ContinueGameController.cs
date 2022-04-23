using System;
using UnityEngine;

public class ContinueGameController : Controller
{
    public override void Excute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        UIBoard uiBoard = GetView<UIBoard>();
        if (uiBoard.Times < 0.1f)
        {
            uiBoard.Times += 20;
        }
        gm.IsPause = false;
        gm.IsPlay = true;
    }
}