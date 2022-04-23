using System;
using UnityEngine;

public class Obstacles : ReusableObject
{
    protected Transform effectParent;

    protected virtual void Awake()
    {
        effectParent = GameObject.Find("EffectParent").transform;
    }
    public override void OnSpawn()
    {

    }

    public override void OnUnSpawn()
    {

    }

    /// <summary>
    /// 撞击玩家
    /// </summary>
    protected virtual void HitPlayer(Vector3 pos)
    {
        //播放撞击特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_ZhuangJi", effectParent);
        go.transform.position = pos;
        //回收
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }
}