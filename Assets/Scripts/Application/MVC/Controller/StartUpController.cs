using System;
using UnityEngine;

public class StartUpController : Controller
{
    public override void Excute(object data)
    {
        //注册所有的controller
        RegisterController(Const.E_EnterScene, typeof(EnterSceneController));
        RegisterController(Const.E_ExitScene, typeof(ExitSceneController));
        RegisterController(Const.E_EndGame, typeof(EndGameController));
        RegisterController(Const.E_PauseGame, typeof(PauseGameController));
        RegisterController(Const.E_ResumeGame, typeof(ResumeGameController));
        RegisterController(Const.E_HitItem, typeof(HitItemController));
        RegisterController(Const.E_FinalShouUI, typeof(FinalShouUIController));
        RegisterController(Const.E_BriberyClick, typeof(BriberyController));
        RegisterController(Const.E_ContinueGame, typeof(ContinueGameController));
        RegisterController(Const.E_BuyTools, typeof(BuyToolsController));
        RegisterController(Const.E_BuyFootball, typeof(BuyFootballController));
        RegisterController(Const.E_EquipFootball, typeof(EquipFootballController));
        RegisterController(Const.E_BuySkinAndCloth, typeof(BuySkinAndClothController));
        RegisterController(Const.E_EquipSkinAndCloth, typeof(EquipSkinAndClothController));
        
        //注册model
        RegisterModel(new GameModel());

        //初始化
        GameModel gm = GetModel<GameModel>();
        gm.Init();
    }
}