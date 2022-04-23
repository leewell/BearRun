using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : View
{
    private Button playBtn;
    private Button shopBtn;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MeshRenderer meshRenderer;

    private GameModel gm;

    public override string Name => Const.V_UIMainMenu;

    private void Awake()
    {
        gm = GetModel<GameModel>();
        playBtn = transform.Find("playBtn").GetComponent<Button>();
        playBtn.onClick.AddListener(PlayButtonClickListener);
        shopBtn = transform.Find("shopBtn").GetComponent<Button>();
        shopBtn.onClick.AddListener(ShopButtonClickListener);
        skinnedMeshRenderer = transform.Find("Model/Jersey_BaXi").GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material = Game.Instance.staticData.GetPlayerClothInfo(gm.TakeOnSkinAndCloth.SkinId, gm.TakeOnSkinAndCloth.ClothId).material;
        meshRenderer = transform.Find("Model/Ball/Ball_SangBaRongYao").GetComponent<MeshRenderer>();
        meshRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.TakeOnFootball).material;
    }

    public override void HandleEvent(string eventName, object data)
    {

    }

    private void PlayButtonClickListener()
    {
        Game.Instance.LoadLevel(4);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    private void ShopButtonClickListener()
    {
        Game.Instance.LoadLevel(2);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }
}