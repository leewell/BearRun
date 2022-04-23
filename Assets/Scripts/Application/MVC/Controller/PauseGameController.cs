using System;
using UnityEngine;

public class PauseGameController : Controller
{
    public override void Excute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        gm.IsPause = true;

        PauseArgs pauseArgs = data as PauseArgs;
        UIPause uIPause = GetView<UIPause>();
        uIPause.Show();
        uIPause.SetParams(pauseArgs.score, pauseArgs.coin, pauseArgs.distance);
    }
}