using System;
using System.Collections;
using UnityEngine;

public class ShootGoal : ReusableObject
{
    //守门员动画组件
    private Animation goalKeeper;
    //球门动画组件
    private Animation goalDoor;
    //球网对象
    private GameObject net;

    [Header("守门员撞飞速度")]
    public float speed = 10;
    //是否被撞飞
    private bool m_isFly = false;

    private void Awake()
    {
        goalKeeper = transform.Find("Root/goalkeeperRoot/goalkeeper/root/ShouMenYuan").GetComponent<Animation>();
        goalDoor = transform.Find("Root/QiuMen").GetComponent<Animation>();
        net = goalDoor.transform.Find("Block_QiuMen_Wang").gameObject;
    }

    public override void OnSpawn()
    {

    }

    public override void OnUnSpawn()
    {
        goalKeeper.transform.localPosition = Vector3.zero;
        goalKeeper.Play("standard");
        goalDoor.Play("QiuMen_St");
        net.SetActive(true);
        goalKeeper.transform.parent.parent.gameObject.SetActive(true);
        m_isFly = false;
        StopAllCoroutines();
    }

    /// <summary>
    /// 球进了，孩子需要发送一个消息
    /// </summary>
    public void ShootAGoal(int i)
    {
        switch (i)
        {
            case -2:
                goalKeeper.Play("left_flutter");
                break;
            case 0:
                goalKeeper.Play("flutter");
                break;
            case 2:
                goalKeeper.Play("right_flutter");
                break;
            default:
                break;
        }
        StartCoroutine(HideGoalKeeperCor());
    }

    IEnumerator HideGoalKeeperCor()
    {
        yield return new WaitForSeconds(1f);
        goalKeeper.transform.parent.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// 撞飞守门员
    /// </summary>
    public void HitGoalKeeper()
    {
        m_isFly = true;
        goalKeeper.Play("fly");
    }

    public void HitDoor(int i)
    {
        //播放球网动画
        switch (i)
        {
            case 0: //左边道
                goalDoor.Play("QiuMen_RR");
                break;
            case 1: // 中间道
                goalDoor.Play("QiuMen_St");
                break;
            case 2: // 右边道
                goalDoor.Play("QiuMen_LR");
                break;
            default:
                break;
        }
        //球网消失
        net.SetActive(false);
    }

    private void Update()
    {
        if (m_isFly)
        {
            goalKeeper.transform.position += new Vector3(speed / 2, speed, speed) * Time.deltaTime;
        }
    }
}