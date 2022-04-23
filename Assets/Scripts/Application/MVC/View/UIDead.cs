using System;
using UnityEngine;
using UnityEngine.UI;

public class UIDead : View
{
    //取消按钮
    private Button cancelBtn;
    //贿赂按钮
    private Button giveMoneyBtn;
    //贿赂次数
    private int m_BriberyCount = 1;
    //本次需要贿赂的金币文本
    private Text briberyCoinTxt;

    public override string Name => Const.V_Dead;

    public int BriberyCount { get => m_BriberyCount; set => m_BriberyCount = value; }

    private void Awake()
    {
        cancelBtn = transform.Find("BG/Cancel").GetComponent<Button>();
        cancelBtn.onClick.AddListener(CancelButtonClickListener);
        giveMoneyBtn = transform.Find("BG/MoneyCount/Button").GetComponent<Button>();
        giveMoneyBtn.onClick.AddListener(BriberyClickListener);
        briberyCoinTxt = transform.Find("BG/MoneyCount/Text").GetComponent<Text>();
    }
    public override void HandleEvent(string eventName, object data)
    {

    }

    public override void Show()
    {
        base.Show();
        briberyCoinTxt.text = (BriberyCount * 500).ToString();
    }

    /// <summary>
    /// 取消按钮点击事件
    /// </summary>
    public void CancelButtonClickListener()
    {
        SendEvent(Const.E_FinalShouUI);
    }

    /// <summary>
    /// 贿赂按钮点击事件
    /// </summary>
    public void BriberyClickListener()
    {
        CoinArgs e = new CoinArgs
        {
            coin = BriberyCount * 500
        };
        SendEvent(Const.E_BriberyClick, e);
    }
}