using System;
using System.Collections.Generic;
using UnityEngine;

public class SubPool
{
    //对象集合
	private List<GameObject> m_objects = new List<GameObject>();
    //预设
    private GameObject m_prefabs;
    //父物体
	private Transform m_parent;
	public string Name
    {
        get
        {
            return m_prefabs.name;
        }
    }

    public SubPool(Transform parent, GameObject go)
    {
        m_prefabs = go;
        m_parent = parent;
    }

    public GameObject OnSpawn()
    {
        GameObject go = null;
        foreach (var obj in m_objects)
        {
            if (!obj.activeSelf)
            {
                go = obj;
            }
        }
        if (go == null)
        {
            go = GameObject.Instantiate<GameObject>(m_prefabs);
            go.transform.parent = m_parent;
            m_objects.Add(go);
        }
        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
        return go;
    }

    public void OnUnSpawn(GameObject go)
    {
        if (Contain(go))
        {
            go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    public void OnUnSpawnAll()
    {
        foreach (var obj in m_objects)
        {
            if (obj.activeSelf)
            {
                OnUnSpawn(obj);
            }
        }
    }

    public bool Contain(GameObject go)
    {
        return m_objects.Contains(go);
    }
}