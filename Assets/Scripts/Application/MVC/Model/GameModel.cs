using System;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : Model
{
    private const int InitCoin = 10000;

    //是否正在游戏
    private bool m_isPlay = true;
    //是否暂停
    private bool m_isPause = false;
    //技能相关的持续时间（比如吃到双倍金币、磁铁等）
    private int m_skillTime;

    //磁铁道具数量
    private int m_magnet;
    //双倍金币道具数量
    private int m_coinMultiply;
    //无敌道具数量
    private int m_invincible;

    //经验值
    private int m_Exp;
    //等级
    private int m_Grade;
    //玩家金币
    private int m_Coin;
    //装备足球的索引（第0个都是默认装备的）
    private int m_TakeOnFootball = 0;
    //已购买的足球索引集合
    public List<int> buyFootball = new List<int>();
    //穿的衣服和皮肤信息(默认穿的是第一套)
    private BuySkinAndClothID m_TakeOnSkinAndCloth = new BuySkinAndClothID() { SkinId = 0, ClothId = 0 };
    //已购买的皮肤和衣服索引集合
    public List<BuySkinAndClothID> buySkinAndClothList = new List<BuySkinAndClothID>();
    private ItemState itemState = ItemState.UnBuy;

    //上一个view的下标
    public int lastIndex = 1;

    public override string Name
    {
        get
        {
            return Const.M_GameModel;
        }
    }

    public bool IsPlay { get => m_isPlay; set => m_isPlay = value; }
    public bool IsPause { get => m_isPause; set => m_isPause = value; }
    public int SkillTime { get => m_skillTime; set => m_skillTime = value; }
    public int Magnet { get => m_magnet; set => m_magnet = value; }
    public int CoinMultiply { get => m_coinMultiply; set => m_coinMultiply = value; }
    public int Invincible { get => m_invincible; set => m_invincible = value; }
    public int Exp { 
        get => m_Exp;
        set
        {
            while (value > Grade * 100 + 500)
            {
                value -= Grade * 100 + 500;
                Grade++;
            }
            m_Exp = value;
        }
    }
    public int Grade { get => m_Grade; set => m_Grade = value; }
    public int Coin { get => m_Coin;
        set
        {
            m_Coin = value;
            Debug.Log("还剩：" + value + "钱");
        }
    }

    public int TakeOnFootball { get => m_TakeOnFootball; set => m_TakeOnFootball = value; }
    public BuySkinAndClothID TakeOnSkinAndCloth { get => m_TakeOnSkinAndCloth; set => m_TakeOnSkinAndCloth = value; }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        m_magnet = 0;
        m_coinMultiply = 0;
        m_invincible = 0;
        m_skillTime = 5;
        m_Grade = 1;
        m_Exp = 0;
        Coin = InitCoin;

        InitSkin();
    }

    /// <summary>
    /// 初始化所有的换装信息
    /// </summary>
    private void InitSkin()
    {
        //添加足球
        buyFootball.Add(TakeOnFootball);
        //把默认穿着的衣服和皮肤加入到列表里
        buySkinAndClothList.Add(TakeOnSkinAndCloth);
    }

    /// <summary>
    /// 购买装备
    /// </summary>
    /// <param name="coin">所需金币</param>
    /// <returns>true-购买成功；false购买失败</returns>
    public bool GetMoney(int coin)
    {
        if (coin <= Coin)
        {
            Coin -= coin;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 检测足球状态
    /// </summary>
    public ItemState CheckState(int i)
    {
        if (i == TakeOnFootball)
        {
            return ItemState.Equip;
        }
        else
        {
            if (buyFootball.Contains(i))
            {
                return ItemState.Buy;
            }
            else
            {
                return ItemState.UnBuy;
            }
        }
    }

    /// <summary>
    /// 检测皮肤和衣服状态
    /// </summary>
    public ItemState CheckSkinAndClothState(int skinId,int clothId, int tab)
    {
        if (tab == 0) // 皮肤
        {
            if (skinId == TakeOnSkinAndCloth.SkinId)
            {
                return ItemState.Equip;
            }
            else
            {
                foreach (BuySkinAndClothID item in buySkinAndClothList)
                {
                    if (skinId == item.SkinId)
                    {
                        return ItemState.Buy;
                    }
                }
                return ItemState.UnBuy;
            }
        }
        else // 衣服
        {
            if (clothId == TakeOnSkinAndCloth.ClothId)
            {
                return ItemState.Equip;
            }
            else
            {
                foreach (BuySkinAndClothID item in buySkinAndClothList)
                {
                    if (clothId == item.ClothId)
                    {
                        return ItemState.Buy;
                    }
                }
                return ItemState.UnBuy;
            }
        }
    }
}

/// <summary>
/// 存储购买的皮肤和衣服数据的类
/// </summary>
public class BuySkinAndClothID
{
    public int SkinId;
    public int ClothId;
}