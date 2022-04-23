using UnityEngine;
using UnityEngine.UI;

public class UIBuyTools : View
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MeshRenderer meshRenderer;

    private Text coinText;
    private Text coinMultiplyText;
    private Text magnetText;
    private Text invincibleText;

    private Button buyRandomBtn;
    private Button buyInvincibleBtn;
    private Button buyMagnetBtn;
    private Button buyCoinMultiplyBtn;
    private Button playGameBtn;
    private Button returnBtn;

    private GameModel gm;
    
    public override string Name => Const.V_UIBuyTools;

    private void Awake()
    {
        gm = GetModel<GameModel>();

        coinText = transform.Find("money/Text").GetComponent<Text>();
        coinMultiplyText = transform.Find("Grid/coinMultplyBg/Image/countBg/count").GetComponent<Text>();
        magnetText = transform.Find("Grid/magnetBg/Image/countBg/count").GetComponent<Text>();
        invincibleText = transform.Find("Grid/invincibleBg/Image/countBg/count").GetComponent<Text>();
        buyRandomBtn = transform.Find("Grid/randomBg/Button").GetComponent<Button>();
        buyRandomBtn.onClick.AddListener(RandomButtonClick);
        buyInvincibleBtn = transform.Find("Grid/invincibleBg/Button").GetComponent<Button>();
        buyInvincibleBtn.onClick.AddListener(() => InvincibleItemClick());
        buyMagnetBtn = transform.Find("Grid/magnetBg/Button").GetComponent<Button>();
        buyMagnetBtn.onClick.AddListener(() => MagnetItemClick());
        buyCoinMultiplyBtn = transform.Find("Grid/coinMultplyBg/Button").GetComponent<Button>();
        buyCoinMultiplyBtn.onClick.AddListener(() => CoinMultiplyItemClick());
        playGameBtn = transform.Find("playBtn").GetComponent<Button>();
        playGameBtn.onClick.AddListener(PlayButtonClick);
        returnBtn = transform.Find("returnBtn").GetComponent<Button>();
        returnBtn.onClick.AddListener(ReturnButtonClick);

        InitUI();
    }

    public override void HandleEvent(string eventName, object data)
    {

    }

    public void InitUI()
    {
        coinText.text = gm.Coin.ToString();
        ShowOrHide(gm.CoinMultiply, coinMultiplyText);
        ShowOrHide(gm.Magnet, magnetText);
        ShowOrHide(gm.Invincible, invincibleText);

        skinnedMeshRenderer = transform.parent.Find("Model/Jersey_BaXi").GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material = Game.Instance.staticData.GetPlayerClothInfo(gm.TakeOnSkinAndCloth.SkinId, gm.TakeOnSkinAndCloth.ClothId).material;
        meshRenderer = transform.parent.Find("Model/Ball/Ball_SangBaRongYao").GetComponent<MeshRenderer>();
        meshRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.TakeOnFootball).material;
    }

    public void ShowOrHide(int i,Text txt)
    {
        if (i > 0)
        {
            txt.transform.parent.gameObject.SetActive(true);
            txt.text = i.ToString();
        }
        else
        {
            txt.transform.parent.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 购买无敌道具
    /// </summary>
    private void InvincibleItemClick(int money = 200)
    {
        BuyToolsArgs e = new BuyToolsArgs
        {
            coin = money,
            kind = ItemKind.ItemInvincible
        };
        SendEvent(Const.E_BuyTools, e);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    /// <summary>
    /// 购买磁铁道具
    /// </summary>
    private void MagnetItemClick(int money = 100)
    {
        BuyToolsArgs e = new BuyToolsArgs
        {
            coin = money,
            kind = ItemKind.ItemMagnet
        };
        SendEvent(Const.E_BuyTools, e);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    /// <summary>
    /// 购买双倍金币道具
    /// </summary>
    private void CoinMultiplyItemClick(int money = 200)
    {
        BuyToolsArgs e = new BuyToolsArgs
        {
            coin = money,
            kind = ItemKind.ItemCoinMultiply
        };
        SendEvent(Const.E_BuyTools, e);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    /// <summary>
    /// 购买随机道具
    /// </summary>
    private void RandomButtonClick()
    {
        int randomMoney = 300;
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            InvincibleItemClick(randomMoney);
        }else if (r == 1)
        {
            MagnetItemClick(randomMoney);
        }
        else
        {
            CoinMultiplyItemClick(randomMoney);
        }
    }

    /// <summary>
    /// 开始游戏按钮点击事件
    /// </summary>
    private void PlayButtonClick()
    {
        Game.Instance.LoadLevel(4);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    private void ReturnButtonClick()
    {
        if (gm.lastIndex == 4)
        {
            gm.lastIndex = 1;
        }
        Game.Instance.LoadLevel(gm.lastIndex);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }
}