using System;
using UnityEngine;

public class People : Obstacles
{
    [Header("小人的奔跑/撞飞的速度")]
    public float speed = 10f;
    //是否被撞飞
    private bool isFly = false;
    //是否触发了触发器
    private bool isHit = false;

    private Animation anim;
    private GameModel gm;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animation>();
        gm = MVC.GetModel<GameModel>();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        anim.Play("run");
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
        anim.transform.localPosition = Vector3.zero;
        isHit = false;
        isFly = false;
    }

    protected override void HitPlayer(Vector3 pos)
    {
        //播放撞击特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_ZhuangJi", effectParent);
        go.transform.position = pos;
        isHit = false;
        isFly = true;
        anim.Play("fly");
    }

    /// <summary>
    /// 当触发了触发器时，小人开始移动
    /// </summary>
    public void HitTrigger()
    {
        isHit = true;
    }

    private void Update()
    {
        if (isHit && gm.IsPlay && !gm.IsPause)
        {
            transform.position -= new Vector3(0, 0, speed) * Time.deltaTime;
        }
        if (isFly && gm.IsPlay && !gm.IsPause)
        {
            transform.position += new Vector3(speed, speed, 0) * Time.deltaTime;
        }
    }
}