using System;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoSingleton<StaticData>
{
	private Dictionary<int, FootballInfo> m_footballDic = new Dictionary<int, FootballInfo>();
	public List<Material> matList;

    private Dictionary<int, Dictionary<int, FootballInfo>> m_playerClothDic = new Dictionary<int, Dictionary<int, FootballInfo>>();
    public List<Material> playerClothList;

    protected override void Awake()
    {
        base.Awake();
        InitInfo();
        InitPlayerCloth();
    }

    /// <summary>
    /// 初始化足球材质信息
    /// </summary>
    private void InitInfo()
    {
        m_footballDic.Add(0, new FootballInfo() { coin = 0, material = matList[0] });
        m_footballDic.Add(1, new FootballInfo() { coin = 200, material = matList[1] });
        m_footballDic.Add(2, new FootballInfo() { coin = 500, material = matList[2] });
    }

    /// <summary>
    /// 初始化皮肤信息
    /// </summary>
    private void InitPlayerCloth()
    {
        int t = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!m_playerClothDic.ContainsKey(i))
                {
                    m_playerClothDic[i] = new Dictionary<int, FootballInfo>();
                }
                m_playerClothDic[i].Add(j, new FootballInfo() { coin = t*200, material = playerClothList[t]});
                t++;
            }
        }
    }

    /// <summary>
    /// 获取信息
    /// </summary>
    public FootballInfo GetFootballInfo(int i)
    {
        return m_footballDic[i];
    }

    public FootballInfo GetPlayerClothInfo(int i,int j)
    {
        return m_playerClothDic[i][j];
    }
}