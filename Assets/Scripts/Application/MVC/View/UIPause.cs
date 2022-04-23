using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : View
{
    //继续游戏按钮
    private Button resumeBtn;
    private Button homeBtn;

    //分数文本
    private Text scoreTxt;
    //金币文本
    private Text coinTxt;
    //距离文本
    private Text distanceTxt;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MeshRenderer meshRenderer;

    private GameModel gm;

    public override string Name => Const.V_Pause;

    private void Awake()
    {
        gm = GetModel<GameModel>();
        resumeBtn = transform.Find("BG/ContinueBtn").GetComponent<Button>();
        resumeBtn.onClick.AddListener(ResumeButtonClickListener);
        homeBtn = transform.Find("BG/HomeBtn").GetComponent<Button>();
        homeBtn.onClick.AddListener(HomeButtonClickListener);
        scoreTxt = transform.Find("BG/bg/scoreValue").GetComponent<Text>();
        coinTxt = transform.Find("BG/bg/coinValue").GetComponent<Text>();
        distanceTxt = transform.Find("BG/bg/distanceValue").GetComponent<Text>();
        skinnedMeshRenderer = transform.Find("Model/Jersey_BaXi").GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material = Game.Instance.staticData.GetPlayerClothInfo(gm.TakeOnSkinAndCloth.SkinId, gm.TakeOnSkinAndCloth.ClothId).material;
        meshRenderer = transform.Find("Model/Ball/Ball_SangBaRongYao").GetComponent<MeshRenderer>();
        meshRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.TakeOnFootball).material;
    }

    public override void HandleEvent(string eventName, object data)
    {

    }

    public void SetParams(int score,int coin,int distance)
    {
        scoreTxt.text = score.ToString();
        coinTxt.text = coin.ToString();
        distanceTxt.text = distance.ToString();
    }

    public void ResumeButtonClickListener()
    {
        Hide();
        SendEvent(Const.E_ResumeGame);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    private void HomeButtonClickListener()
    {
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
        Game.Instance.LoadLevel(1);
    }
}