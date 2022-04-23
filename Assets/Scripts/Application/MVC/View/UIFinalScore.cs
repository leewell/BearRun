using System;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalScore : View
{
    //距离文本
    private Text distanceValue;
    //金币文本
    private Text coinValue;
    //进球文本
    private Text goalValue;
    //总分数文本
    private Text scoreValue;
    //等级文本
    private Text levelValue;
    //经验值文本
    private Text expValue;
    //经验值滑动条
    private Slider expSlider;
    //重玩按钮
    private Button replayBtn;
    //返回主页按钮
    private Button homeBtn;
    //购物按钮
    private Button shoppingBtn;

    public override string Name => Const.V_FinalScore;

    private void Awake()
    {
        distanceValue = transform.Find("BG/bg/DistanceValue").GetComponent<Text>();
        coinValue = transform.Find("BG/bg/CoinValue").GetComponent<Text>();
        goalValue = transform.Find("BG/bg/GoalValue").GetComponent<Text>();
        scoreValue = transform.Find("BG/Score/Text").GetComponent<Text>();
        levelValue = transform.Find("BG/Exp/level").GetComponent<Text>();
        expValue = transform.Find("BG/Exp/Text").GetComponent<Text>();
        expSlider = transform.Find("BG/Exp").GetComponent<Slider>();
        replayBtn = transform.Find("ContinueBtn").GetComponent<Button>();
        replayBtn.onClick.AddListener(ReplayButtonClick);
        homeBtn = transform.Find("HomeBtn").GetComponent<Button>();
        homeBtn.onClick.AddListener(HomeButtonClick);
        shoppingBtn = transform.Find("ShoppingBtn").GetComponent<Button>();
        shoppingBtn.onClick.AddListener(ShopButtonClick);
    }

    public override void HandleEvent(string eventName, object data)
    {

    }
    /// <summary>
    /// 更新UI
    /// </summary>
    /// <param name="dis">总距离</param>
    /// <param name="coin">总金币数</param>
    /// <param name="goal">总进球数</param>
    public void UpdateUI(int dis,int coin,int goal,int exp,int grade)
    {
        distanceValue.text = dis.ToString();
        coinValue.text = coin.ToString();
        goalValue.text = goal.ToString();
        scoreValue.text = (dis * (goal + 1) + coin).ToString();
        levelValue.text = grade.ToString() + "级";
        expValue.text = exp.ToString() + "/" + ((grade * 100 + 500)).ToString();
        expSlider.value = (exp * 1.0f) / (grade * 100 + 500);
    }

    /// <summary>
    /// 重玩按钮点击事件
    /// </summary>
    private void ReplayButtonClick()
    {
        Game.Instance.LoadLevel(4);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    private void HomeButtonClick()
    {
        Game.Instance.LoadLevel(1);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    private void ShopButtonClick()
    {
        Game.Instance.LoadLevel(2);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }
}