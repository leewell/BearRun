using System;
using UnityEngine;

public class Car : Obstacles
{
    //汽车是否可以移动
    [Header("汽车是否能移动")]
    public bool canMove = false;
    //是否能撞上
    private bool isBlock = false;

    [Header("汽车的移动速度")]
    public float speed = 10f;

    private GameModel gm;
    protected override void Awake()
    {
        base.Awake();
        gm = MVC.GetModel<GameModel>();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        isBlock = false;
        base.OnUnSpawn();
    }

    protected override void HitPlayer(Vector3 pos)
    {
        base.HitPlayer(pos);
    }

    /// <summary>
    /// 碰撞到触发器
    /// </summary>
    public void HitTrigger()
    {
        isBlock = true;
    }

    private void Update()
    {
        if (canMove && isBlock && gm.IsPlay && !gm.IsPause)
        {
            transform.Translate(-transform.forward * speed * Time.deltaTime);
        }
    }
}