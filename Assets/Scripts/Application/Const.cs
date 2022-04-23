using System;
using UnityEngine;

public static class Const
{
	//事件名称
	public const string E_ExitScene = "E_SceneExit"; // 需要SceneArgs参数
	public const string E_EnterScene = "E_EnterScene"; // 需要SceneArgs参数
	public const string E_StartUp = "E_StartUp";
	public const string E_EndGame = "E_EndGame";
	public const string E_PauseGame = "E_PauseGame";
	public const string E_ResumeGame = "E_ResumeGame";

	//事件-view相关
	public const string E_UpdateDis = "E_UpdateDis"; // 需要DistanceArgs参数
	public const string E_UpdateCoin = "E_UpdateCoin"; // 需要CoinArgs参数
	public const string E_HitAddTime = "E_HitAddTime";
	public const string E_HitItem = "E_HitItem"; // 需要ItemArgs参数

	public const string E_HitGoalTrigger = "E_HitGoalTrigger";
	public const string E_OnGoalClick = "E_OnGoalClick";
	//进球了事件，得分+1
	public const string E_ShootGoal = "E_ShootGoal";
	//结算事件
	public const string E_FinalShouUI = "E_FinalShouUI";
	//贿赂事件
	public const string E_BriberyClick = "E_BriberyClick"; // 需要CoinArgs参数
	//resume之后，继续游戏
	public const string E_ContinueGame = "E_ContinueGame";
	//道具购买
	public const string E_BuyTools = "E_BuyTools";
	//购买足球
	public const string E_BuyFootball = "E_BuyFootball"; // 需要BuyFootballArgs参数
	//装备足球
	public const string E_EquipFootball = "E_EquipFootball";
	//购买皮肤和衣服
	public const string E_BuySkinAndCloth = "E_BuySkinAndCloth"; // 需要BuySkinAndClothArgs参数
	//装备皮肤和衣服
	public const string E_EquipSkinAndCloth = "E_EquipSkinAndCloth";


	//model名称
	public const string M_GameModel = "M_GameModel";

	// view名称
	public const string V_PlayerMove = "V_PlayerMove";
	public const string V_PlayerAnim = "V_PlayerAnim";
	public const string V_Board = "V_Board";
	public const string V_Pause = "V_Pause";
	public const string V_Resume = "V_Resume";
	public const string V_Dead = "V_Dead";
	public const string V_FinalScore = "V_FinalScore";
	public const string V_UIBuyTools = "V_UIBuyTools";
	public const string V_UIMainMenu = "V_UIMainMenu";
	public const string V_UIShop = "V_UIShop";


}

public enum InputDirection
{
	Null,Left,Right,Down,Up
}

/// <summary>
/// 吃到奖励物品的类型
/// </summary>
public enum ItemKind
{
	ItemInvincible,ItemMagnet,ItemCoinMultiply
}

public enum ItemState
{
	UnBuy,Buy,Equip
}