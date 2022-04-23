using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : View
{
    //当前选择的索引
    private int selectIndex = 0;
    //当前的材质
    private MeshRenderer ballRender;
    private Toggle headToggle;
    private Toggle clothToggle;
    private Toggle shoesToggle;
    private Toggle footballToggle;
    private GameObject selectSkinToggle;
    private GameObject selectClothToggle;
    private GameObject selectShoesToggle;
    private GameObject selectFootbalToggle;

    private Button playBtn;
    private Button returnBtn;
    private Toggle footballToggle1;
    private Toggle footballToggle2;
    private Toggle footballToggle3;
    private Toggle skinToggle1;
    private Toggle skinToggle2;
    private Toggle skinToggle3;
    private Toggle clothToggle1;
    private Toggle clothToggle2;
    private Toggle clothToggle3;
    private Button buyButton1;
    private Button buyButton2;
    private Button buyButton3;
    private Button buyButton4;
    private Image buyImg4;
    private Text levelTxt;
    //购买图片的sprite
    public Sprite buySprite;
    //装备图片的sprite
    public Sprite equipSprite;
    //未购买flag的sprite
    public Sprite unBuyFlagSprite;
    //已购买flag的sprite
    public Sprite buyFlagSprite;
    //已装备flag的sprite
    public Sprite equipFlagSprite;
    //足球的购买图标flag图片
    private Image buyFlagImg1;
    private Image buyFlagImg2;
    private Image buyFlagImg3;
    //皮肤的购买图标flag图片
    private Image buySkinFlagImg1;
    private Image buySkinFlagImg2;
    private Image buySkinFlagImg3;
    //衣服的购买图标flag图片
    private Image buyClothFlagImg1;
    private Image buyClothFlagImg2;
    private Image buyClothFlagImg3;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private List<Image> buyFlags = new List<Image>();
    private List<Image> buySkinFlags = new List<Image>();
    private List<Image> buyClothFlags = new List<Image>();
    private List<Image> skinAndClothFlags = new List<Image>();
    //存放皮肤和衣服的购买按钮
    private List<Button> skinAndClothList = new List<Button>();

    //其他UI
    private Image headImg;
    private Text moneyTxt;
    private Text nameTxt;
    //头像sprite集合
    public List<Sprite> headSprites;

    private GameModel gm;
    private ItemState state;
    //0-选的皮肤；1-衣服；2-鞋子；3-足球
    private int tab = 0;

    public override string Name => Const.V_UIShop;

    private void Awake()
    {
        gm = GetModel<GameModel>();
        skinnedMeshRenderer = transform.Find("misc/Model/Jersey_BaXi").GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material = Game.Instance.staticData.GetPlayerClothInfo(gm.TakeOnSkinAndCloth.SkinId, gm.TakeOnSkinAndCloth.ClothId).material;

        headToggle = transform.Find("head").GetComponent<Toggle>();
        selectSkinToggle = transform.Find("selectDetail/selectSkin").gameObject;
        headToggle.onValueChanged.AddListener((selected) => {
            Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
            selectSkinToggle.SetActive(selected);
            if (selected)
            {
                GizmoSkin();
            }
        });
        clothToggle = transform.Find("select/cloth").GetComponent<Toggle>();
        selectClothToggle = transform.Find("selectDetail/selectCloth").gameObject;
        clothToggle.onValueChanged.AddListener((selected) => {
            Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
            selectClothToggle.SetActive(selected);
            if (selected)
            {
                GizmoCloth();
            }
        });
        shoesToggle = transform.Find("select/shoes").GetComponent<Toggle>();
        selectShoesToggle = transform.Find("selectDetail/selectShoes").gameObject;
        shoesToggle.onValueChanged.AddListener((selected) => {
            Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
            selectShoesToggle.SetActive(selected);
            if (selected)
            {

            }
        });
        footballToggle = transform.Find("select/football").GetComponent<Toggle>();
        selectFootbalToggle = transform.Find("selectDetail/selectFootbal").gameObject;
        footballToggle.onValueChanged.AddListener((selected) => {
            Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
            selectFootbalToggle.SetActive(selected);
            if (selected)
            {
                GizmoFootball();
            }
        });
        ballRender = transform.Find("misc/Model/Ball/Ball_SangBaRongYao").GetComponent<MeshRenderer>();
        ballRender.material = Game.Instance.staticData.GetFootballInfo(gm.TakeOnFootball).material;

        footballToggle1 = transform.Find("selectDetail/selectFootbal/black").GetComponent<Toggle>();
        footballToggle2 = transform.Find("selectDetail/selectFootbal/pink").GetComponent<Toggle>();
        footballToggle3 = transform.Find("selectDetail/selectFootbal/green").GetComponent<Toggle>();
        footballToggle1.onValueChanged.AddListener((value) => FootballValueChanged(value,0));
        footballToggle2.onValueChanged.AddListener((value) => FootballValueChanged(value, 1));
        footballToggle3.onValueChanged.AddListener((value) => FootballValueChanged(value, 2));
        clothToggle1 = transform.Find("selectDetail/selectCloth/black").GetComponent<Toggle>();
        clothToggle2 = transform.Find("selectDetail/selectCloth/pink").GetComponent<Toggle>();
        clothToggle3 = transform.Find("selectDetail/selectCloth/green").GetComponent<Toggle>();
        clothToggle1.onValueChanged.AddListener((value) => ClothValueChanged(value,1, 0));
        clothToggle2.onValueChanged.AddListener((value) => ClothValueChanged(value,1, 1));
        clothToggle3.onValueChanged.AddListener((value) => ClothValueChanged(value,1, 2));
        skinToggle1 = transform.Find("selectDetail/selectSkin/black").GetComponent<Toggle>();
        skinToggle2 = transform.Find("selectDetail/selectSkin/pink").GetComponent<Toggle>();
        skinToggle3 = transform.Find("selectDetail/selectSkin/green").GetComponent<Toggle>();
        skinToggle1.onValueChanged.AddListener((value) => ClothValueChanged(value,0, 0));
        skinToggle2.onValueChanged.AddListener((value) => ClothValueChanged(value,0, 1));
        skinToggle3.onValueChanged.AddListener((value) => ClothValueChanged(value,0, 2));

        buyButton1 = transform.Find("selectDetail/selectSkin/buyButton").GetComponent<Button>();
        buyButton1.onClick.AddListener(() => BuySkinAndClothButtonClickListener(0));
        buyButton2 = transform.Find("selectDetail/selectCloth/buyButton").GetComponent<Button>();
        buyButton2.onClick.AddListener(() => BuySkinAndClothButtonClickListener(1));
        buyButton3 = transform.Find("selectDetail/selectShoes/buyButton").GetComponent<Button>();
        buyButton4 = transform.Find("selectDetail/selectFootbal/buyButton").GetComponent<Button>();
        buyImg4 = buyButton4.GetComponent<Image>();
        buyButton4.onClick.AddListener(BuyFootballButtonClickListener);

        skinAndClothList.Add(buyButton1);
        skinAndClothList.Add(buyButton2);
        skinAndClothList.Add(buyButton3);

        buyFlagImg1 = transform.Find("selectDetail/selectFootbal/black/buyFlagImg").GetComponent<Image>();
        buyFlagImg2 = transform.Find("selectDetail/selectFootbal/pink/buyFlagImg").GetComponent<Image>();
        buyFlagImg3 = transform.Find("selectDetail/selectFootbal/green/buyFlagImg").GetComponent<Image>();
        buyFlags.Add(buyFlagImg1);
        buyFlags.Add(buyFlagImg2);
        buyFlags.Add(buyFlagImg3);
        buySkinFlagImg1 = transform.Find("selectDetail/selectSkin/black/buyFlagImg").GetComponent<Image>();
        buySkinFlagImg2 = transform.Find("selectDetail/selectSkin/pink/buyFlagImg").GetComponent<Image>();
        buySkinFlagImg3 = transform.Find("selectDetail/selectSkin/green/buyFlagImg").GetComponent<Image>();
        buySkinFlags.Add(buySkinFlagImg1);
        buySkinFlags.Add(buySkinFlagImg2);
        buySkinFlags.Add(buySkinFlagImg3);
        buyClothFlagImg1 = transform.Find("selectDetail/selectCloth/black/buyFlagImg").GetComponent<Image>();
        buyClothFlagImg2 = transform.Find("selectDetail/selectCloth/pink/buyFlagImg").GetComponent<Image>();
        buyClothFlagImg3 = transform.Find("selectDetail/selectCloth/green/buyFlagImg").GetComponent<Image>();
        buyClothFlags.Add(buyClothFlagImg1);
        buyClothFlags.Add(buyClothFlagImg2);
        buyClothFlags.Add(buyClothFlagImg3);

        skinAndClothFlags = buySkinFlags;

        levelTxt = transform.Find("head/levelBg/Text").GetComponent<Text>();
        levelTxt.text = gm.Grade.ToString();
        headImg = transform.Find("head/Image").GetComponent<Image>();
        moneyTxt = transform.Find("misc/money/Text").GetComponent<Text>();
        nameTxt = transform.Find("name/Text").GetComponent<Text>();
        playBtn = transform.Find("misc/playBtn").GetComponent<Button>();
        playBtn.onClick.AddListener(PlayButtonClickListener);
        returnBtn = transform.Find("misc/returnBtn").GetComponent<Button>();
        returnBtn.onClick.AddListener(ReturnButtonClickListener);

        UpdateNameAndHead();
        UpdateCoin();

        GizmoSkin();
    }

    public override void HandleEvent(string eventName, object data)
    {

    }

    public void GizmoSkin()
    {
        UpdateSkinAndClothIcon();
        ballRender.material = Game.Instance.staticData.GetFootballInfo(gm.TakeOnFootball).material;
        skinnedMeshRenderer.material = Game.Instance.staticData.GetPlayerClothInfo(gm.TakeOnSkinAndCloth.SkinId, gm.TakeOnSkinAndCloth.ClothId).material;
        HideBuyButton();
    }

    public void GizmoCloth()
    {
        UpdateSkinAndClothIcon();
        ballRender.material = Game.Instance.staticData.GetFootballInfo(gm.TakeOnFootball).material;
        skinnedMeshRenderer.material = Game.Instance.staticData.GetPlayerClothInfo(gm.TakeOnSkinAndCloth.SkinId, gm.TakeOnSkinAndCloth.ClothId).material;
        HideBuyButton();
    }

    public void GizmoFootball()
    {
        UpdateBuyIcon();
        HideBuyButton();
        skinnedMeshRenderer.material = Game.Instance.staticData.GetPlayerClothInfo(gm.TakeOnSkinAndCloth.SkinId, gm.TakeOnSkinAndCloth.ClothId).material;
    }

    private void HideBuyButton()
    {
        buyButton1.gameObject.SetActive(false);
        buyButton2.gameObject.SetActive(false);
        buyButton3.gameObject.SetActive(false);
        buyButton4.gameObject.SetActive(false);
    }

    public void UpdateNameAndHead()
    {
        string nameStr = "";
        switch (gm.TakeOnSkinAndCloth.SkinId)
        {
            case 0:
                nameStr = "MoMo";
                break;
            case 1:
                nameStr = "SaLi";
                break;
            case 2:
                nameStr = "Suger";
                break;
        }
        nameTxt.text = nameStr;
        headImg.sprite = headSprites[gm.TakeOnSkinAndCloth.SkinId];
    }

    /// <summary>
    /// 更新金币
    /// </summary>
    public void UpdateCoin()
    {
        moneyTxt.text = gm.Coin.ToString();
    }

    private void FootballValueChanged(bool isSelect, int index)
    {
        if (isSelect)
        {
            Game.Instance.sound.PlayEffectAudio("Se_UI_Dress");
            selectIndex = index;
            ballRender.material = Game.Instance.staticData.GetFootballInfo(selectIndex).material;
            UpdateBuyFootballButton(selectIndex);
        }
    }

    private void ClothValueChanged(bool isSelect, int t, int index)
    {
        if (isSelect)
        {
            Game.Instance.sound.PlayEffectAudio("Se_UI_Dress");
            selectIndex = index;
            if (t == 0) // 修改皮肤
            {
                state = gm.CheckSkinAndClothState(index, gm.TakeOnSkinAndCloth.ClothId, tab);
                skinnedMeshRenderer.material = Game.Instance.staticData.GetPlayerClothInfo(index, gm.TakeOnSkinAndCloth.ClothId).material;
            }
            else // 修改衣服
            {
                state = gm.CheckSkinAndClothState(gm.TakeOnSkinAndCloth.SkinId, index, tab);
                skinnedMeshRenderer.material = Game.Instance.staticData.GetPlayerClothInfo(gm.TakeOnSkinAndCloth.SkinId, index).material;
            }
            tab = t;
            UpdateSkinAndClothBuyButton(index);
        }
    }

    public void UpdateBuyFootballButton(int i)
    {
        ItemState state = gm.CheckState(i);
        switch (state)
        {
            case ItemState.UnBuy:
                buyButton4.gameObject.SetActive(true);
                buyImg4.overrideSprite = buySprite;
                break;
            case ItemState.Buy:
                buyButton4.gameObject.SetActive(true);
                buyImg4.overrideSprite = equipSprite;
                break;
            case ItemState.Equip:
                buyButton4.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 购买足球按钮点击事件
    /// </summary>
    public void BuyFootballButtonClickListener()
    {
        state = gm.CheckState(selectIndex);
        int money = Game.Instance.staticData.GetFootballInfo(selectIndex).coin;
        BuyFootballArgs e = new BuyFootballArgs
        {
            coin = money,
            selectIndex = selectIndex
        };
        switch (state)
        {
            case ItemState.UnBuy: // 购买
                SendEvent(Const.E_BuyFootball, e);
                break;
            case ItemState.Buy: // 装备
                SendEvent(Const.E_EquipFootball, e);
                break;
            default:
                break;
        }
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
        UpdateBuyIcon();
    }

    /// <summary>
    /// 购买皮肤和衣服的点击事件
    /// </summary>
    public void BuySkinAndClothButtonClickListener(int flagId)
    {
        int skinId = flagId == 0 ? selectIndex : gm.TakeOnSkinAndCloth.SkinId;
        int clothId = flagId == 1 ? selectIndex : gm.TakeOnSkinAndCloth.ClothId;
        int money = Game.Instance.staticData.GetPlayerClothInfo(skinId,clothId).coin;
        BuySkinAndClothID skinAndClothID = new BuySkinAndClothID() { SkinId = skinId, ClothId = clothId };
        BuySkinAndClothArgs e = new BuySkinAndClothArgs
        {
            coin = money,
            buyId = skinAndClothID,
            select = selectIndex
        };
        state = gm.CheckSkinAndClothState(skinId, clothId, tab);
        switch (state)
        {
            case ItemState.UnBuy: // 购买
                SendEvent(Const.E_BuySkinAndCloth, e);
                break;
            case ItemState.Buy: // 装备
                SendEvent(Const.E_EquipSkinAndCloth, e);
                break;
            default:
                break;
        }
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
        UpdateSkinAndClothBuyButton(selectIndex);
    }

    /// <summary>
    /// 更新购买足球后的标签图标
    /// </summary>
    public void UpdateBuyIcon()
    {
        for (int i = 0; i < 3; i++)
        {
            ItemState state = gm.CheckState(i);
            switch (state)
            {
                case ItemState.UnBuy:
                    buyFlags[i].overrideSprite = unBuyFlagSprite;
                    break;
                case ItemState.Buy:
                    buyFlags[i].overrideSprite = buyFlagSprite;
                    break;
                case ItemState.Equip:
                    buyFlags[i].overrideSprite = equipFlagSprite;
                    break;
            }
        }
    }

    //更新皮肤和衣服的购买按钮
    public void UpdateSkinAndClothBuyButton(int index)
    {
        Image img = skinAndClothList[tab].GetComponent<Image>();
        int skinId = tab == 0 ? index : gm.TakeOnSkinAndCloth.SkinId;
        int clothId = tab == 1 ? index : gm.TakeOnSkinAndCloth.ClothId;
        state = gm.CheckSkinAndClothState(skinId, clothId, tab);
        switch (state)
        {
            case ItemState.UnBuy:
                img.gameObject.SetActive(true);
                img.overrideSprite = buySprite;
                break;
            case ItemState.Buy:
                img.gameObject.SetActive(true);
                img.overrideSprite = equipSprite;
                break;
            case ItemState.Equip:
                img.gameObject.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// 更新购买后的标签图标
    /// </summary>
    public void UpdateSkinAndClothIcon()
    {
        int skinId, clothId;
        for (int i = 0; i < 3; i++)
        {
            if (tab == 0)
            {
                skinId = i;
                clothId = gm.TakeOnSkinAndCloth.ClothId;
                skinAndClothFlags = buySkinFlags;
            }
            else
            {
                skinId = gm.TakeOnSkinAndCloth.SkinId;
                clothId = i;
                skinAndClothFlags = buyClothFlags;
            }
            ItemState state = gm.CheckSkinAndClothState(skinId, clothId, tab);
            switch (state)
            {
                case ItemState.UnBuy:
                    skinAndClothFlags[i].overrideSprite = unBuyFlagSprite;
                    break;
                case ItemState.Buy:
                    skinAndClothFlags[i].overrideSprite = buyFlagSprite;
                    break;
                case ItemState.Equip:
                    skinAndClothFlags[i].overrideSprite = equipFlagSprite;
                    break;
            }
        }
    }

    private void PlayButtonClickListener()
    {
        Game.Instance.LoadLevel(3);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }
    
    private void ReturnButtonClickListener()
    {
        if (gm.lastIndex == 4)
        {
            gm.lastIndex = 1;
        }
        Game.Instance.LoadLevel(gm.lastIndex);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }
}