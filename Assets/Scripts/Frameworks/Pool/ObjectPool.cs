using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
	Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();
	public string resourceDir = "";

	public GameObject Spawn(string name,Transform parent)
    {
        if (!m_pools.ContainsKey(name))
        {
            RegisterNewPool(name, parent);
        }
        SubPool sub = m_pools[name];
        return sub.OnSpawn();
    }

    /// <summary>
    /// 创建一个新的对象池子
    /// </summary>
	private void RegisterNewPool(string name,Transform parent)
    {
        //资源路径
        string path = resourceDir + "/" + name;
        //生成对象
        GameObject go = Resources.Load<GameObject>(path);
        //新建池子
        SubPool subPool = new SubPool(parent, go);
        m_pools.Add(name,subPool);
    }

    /// <summary>
    /// 回收
    /// </summary>
    public void UnSpawn(GameObject go)
    {
        foreach (var subs in m_pools.Values)
        {
            if (subs.Contain(go))
            {
                subs.OnUnSpawn(go);
                break;
            }
        }
    }

    /// <summary>
    /// 回收所有
    /// </summary>
    public void UnSpawnAll()
    {
        foreach (var subs in m_pools.Values)
        {
            subs.OnUnSpawnAll();
        }
    }

    /// <summary>
    /// 清楚所有
    /// </summary>
    public void Clear()
    {
        m_pools.Clear();
    }
}