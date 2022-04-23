using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : View
{
    //游戏的初始时间
    private const int StartTime = 50;
    //吃到加时间道具的时间增量
    private const int AddTime = 20;

    //技能时间
    private int m_skillTime;
    //进球个数
    private int m_score = 0;
    //金币数量
    private int m_coin = 0;
    //跑了多远的距离
    private int m_distance = 0;
    // 金币文本
    private Text txtCoin;
    //距离文本
    private Text txtDic;
    //时间文本
    private Text txtTimer;
    //时间滑动条
    private Slider sliderTime;
    //射门倒计时滑动条
    private Slider sliderFootball;
    //射门按钮
    private Button footballButton;
    //暂停按钮
    private Button pauseBtn;
    //无敌道具按钮
    private Button invincibleButton;
    //磁铁道具按钮
    private Button magnetButton;
    //双倍金币道具按钮
    private Button coinMultiplyButton;
    //无敌gizom文本
    private Text invincibleTimeTxt;
    //磁铁gizom文本
    private Text magnetTimeTxt;
    //双倍金币gizom文本
    private Text coinMultiplyTimeTxt;
    //当前时间
    private float m_times;

    private GameModel gm;
    //双倍金币协程
    private IEnumerator MultiplyCor;
    //磁铁协程
    private IEnumerator MagnetCor;
    //无敌状态协程
    private IEnumerator InvincibleCor;

    private void Awake()
    {
        txtCoin = transform.Find("Money/Text").GetComponent<Text>();
        txtDic = transform.Find("Distance/Text").GetComponent<Text>();
        txtTimer = transform.Find("Timer/Text").GetComponent<Text>();
        sliderTime = transform.Find("Timer").GetComponent<Slider>();
        sliderFootball = transform.Find("Football").GetComponent<Slider>();
        footballButton = transform.Find("Football/FootballButton").GetComponent<Button>();
        pauseBtn = transform.Find("PauseButton").GetComponent<Button>();
        invincibleButton = transform.Find("InvincibleButton").GetComponent<Button>();
        magnetButton = transform.Find("MagnetButton").GetComponent<Button>();
        coinMultiplyButton = transform.Find("CoinMultiplyButton").GetComponent<Button>();
        invincibleTimeTxt = transform.Find("InvincibleTime/Text").GetComponent<Text>();
        magnetTimeTxt = transform.Find("MagnetTime/Text").GetComponent<Text>();
        coinMultiplyTimeTxt = transform.Find("CoinMultiplyTime/Text").GetComponent<Text>();
        pauseBtn.onClick.AddListener(PauseGameClick);
        invincibleButton.onClick.AddListener(InvincibleItemClick);
        magnetButton.onClick.AddListener(MagnetItemClick);
        coinMultiplyButton.onClick.AddListener(CoinMultiplyItemClick);
        footballButton.onClick.AddListener(FootballButtonClick);

        gm = GetModel<GameModel>();
        m_skillTime = gm.SkillTime;

        Times = StartTime;

        UpdateUI();
    }

    public override string Name => Const.V_Board;

    public int Coin { get => m_coin; 
        set
        {
            m_coin = value;
            txtCoin.text = value.ToString();
        }
    }
    public int Distance { get => m_distance; 
        set 
        {
            m_distance = value;
            txtDic.text = value.ToString() + "米";
        } 
    }

    public float Times { get => m_times;
        set {
            if (value < 0)
            {
                value = 0;
                SendEvent(Const.E_EndGame);
            }
            if (value > StartTime)
            {
                value = StartTime;
            }
            m_times = value;
            txtTimer.text = value.ToString("f2") + " s";
            sliderTime.value = value / StartTime;
        }
    }

    public int Score { get => m_score; set => m_score = value; }

    /// <summary>
    /// 暂停按钮点击回调
    /// </summary>
    public void PauseGameClick()
    {
        PauseArgs e = new PauseArgs
        {
            coin = Coin,
            distance = Distance,
            score = Coin + Distance * (Score + 1)
        };
        SendEvent(Const.E_PauseGame, e);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    /// <summary>
    /// 无敌道具按钮点击回调
    /// </summary>
    private void InvincibleItemClick()
    {
        ItemArgs e = BuildItemArgs(ItemKind.ItemInvincible);
        SendEvent(Const.E_HitItem, e);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    /// <summary>
    /// 磁铁道具按钮点击回调
    /// </summary>
    private void MagnetItemClick()
    {
        ItemArgs e = BuildItemArgs(ItemKind.ItemMagnet);
        SendEvent(Const.E_HitItem, e);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    /// <summary>
    /// 双倍金币道具按钮点击回调
    /// </summary>
    private void CoinMultiplyItemClick()
    {
        ItemArgs e = BuildItemArgs(ItemKind.ItemCoinMultiply);
        SendEvent(Const.E_HitItem, e);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    /// <summary>
    /// 射门按钮点击事件
    /// </summary>
    private void FootballButtonClick()
    {
        SendEvent(Const.E_OnGoalClick);
        Game.Instance.sound.PlayEffectAudio("Se_UI_Button");
    }

    private ItemArgs BuildItemArgs(ItemKind item)
    {
        ItemArgs e = new ItemArgs
        {
            hitCount = 1,
            kind = item
        };
        return e;
    }

    public override void RegisterAttentionEvent()
    {
        attentionList.Add(Const.E_UpdateDis);
        attentionList.Add(Const.E_UpdateCoin);
        attentionList.Add(Const.E_HitAddTime);
        attentionList.Add(Const.E_HitGoalTrigger);
        attentionList.Add(Const.E_ShootGoal);
    }

    public override void HandleEvent(string eventName, object data)
    {
        if (eventName == Const.E_UpdateDis)
        {
            Distance = (data as DistanceArgs).distance;
        }else if (eventName == Const.E_UpdateCoin)
        {
            Coin += (data as CoinArgs).coin;
        }
        else if (eventName == Const.E_HitAddTime)
        {
            Times += AddTime;
        }else if (eventName == Const.E_HitGoalTrigger)
        {
            StartCountDown();
        }else if (eventName == Const.E_ShootGoal)
        {
            Score += 1;
        }
    }

    /// <summary>
    /// 开启射门倒计时
    /// </summary>
    private void StartCountDown()
    {
        StartCoroutine(CountDownCor());
    }

    IEnumerator CountDownCor()
    {
        footballButton.interactable = true;
        sliderFootball.value = 1;
        while (sliderFootball.value > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                sliderFootball.value -= Time.deltaTime;
            }
            yield return 0;
        }
        footballButton.interactable = false;
        sliderFootball.value = 0;
    }

    private void Update()
    {
        if (!gm.IsPause && gm.IsPlay)
        {
            Times -= Time.deltaTime;
        }
    }

    //更新UI
    public void UpdateUI()
    {
        ShowOrHideBtn(gm.Magnet, magnetButton);
        ShowOrHideBtn(gm.CoinMultiply, coinMultiplyButton);
        ShowOrHideBtn(gm.Invincible, invincibleButton);
    }

    private void ShowOrHideBtn(int num,Button btn)
    {
        Transform childTrans = btn.transform.GetChild(1);
        if (num > 0)
        {
            btn.interactable = true;
            childTrans.gameObject.SetActive(false);
        }
        else
        {
            btn.interactable = false;
            childTrans.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 获取吃到技能道具的时间
    /// </summary>
    private string GetTime(float time)
    {
        return ((int)time + 1).ToString();
    }

    public void HitCoinMultiply()
    {
        if (MultiplyCor != null)
        {
            StopCoroutine(MultiplyCor);
        }
        MultiplyCor = MultiplyCoroutine();
        StartCoroutine(MultiplyCor);
    }

    IEnumerator MultiplyCoroutine()
    {
        float timer = m_skillTime;
        coinMultiplyTimeTxt.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                timer -= Time.deltaTime;
            }
            coinMultiplyTimeTxt.text = GetTime(timer);
            yield return 0;
        }
        coinMultiplyTimeTxt.transform.parent.gameObject.SetActive(false);
    }

    public void HitMagnet()
    {
        if (MagnetCor != null)
        {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCoroutine();
        StartCoroutine(MagnetCor);
    }

    IEnumerator MagnetCoroutine()
    {
        float timer = m_skillTime;
        magnetTimeTxt.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                timer -= Time.deltaTime;
            }
            magnetTimeTxt.text = GetTime(timer);
            yield return 0;
        }
        magnetTimeTxt.transform.parent.gameObject.SetActive(false);
    }

    public void HitInvincible()
    {
        if (InvincibleCor != null)
        {
            StopCoroutine(InvincibleCor);
        }
        InvincibleCor = InvincibleCoroutine();
        StartCoroutine(InvincibleCor);
    }

    IEnumerator InvincibleCoroutine()
    {
        float timer = m_skillTime;
        invincibleTimeTxt.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                timer -= Time.deltaTime;
            }
            invincibleTimeTxt.text = GetTime(timer);
            yield return 0;
        }
        invincibleTimeTxt.transform.parent.gameObject.SetActive(false);
    }
}