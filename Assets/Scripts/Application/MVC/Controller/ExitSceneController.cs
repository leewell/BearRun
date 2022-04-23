using System;
using UnityEngine;

public class ExitSceneController : Controller
{
    public override void Excute(object data)
    {
        SceneArgs args = data as SceneArgs;
        switch (args.scenesIndex)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:
                break;
            case 4:
                Game.Instance.objectPool.Clear();
                break;
            default:
                break;
        }
        GameModel gm = GetModel<GameModel>();
        gm.lastIndex = args.scenesIndex;
    }
}