using System;
using System.Collections;
using UnityEngine;

public class Coin : Item
{
    private Transform effectParent;
    [Header("金币被磁铁吸的移动速度")]
    public float moveSpeed = 40;

    private void Awake()
    {
        effectParent = GameObject.Find("EffectParent").transform;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }

    public override void HitPlayer(Transform pos)
    {
        //播放特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_JinBi", effectParent);
        go.transform.position = pos.position;
        //播放音效
        Game.Instance.sound.PlayEffectAudio("Se_UI_JinBi");
        //回收
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            HitPlayer(other.transform);
            other.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
        }
        else if (other.gameObject.tag == Tag.MagnetCollider)
        {
            StartCoroutine(HitMagnet(other.transform));
        }
    }

    IEnumerator HitMagnet(Transform pos)
    {
        bool isLoop = true;
        while (isLoop)
        {
            transform.position = Vector3.Lerp(transform.position, pos.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position,pos.position) < 0.5f)
            {
                isLoop = false;
                HitPlayer(pos.transform);
                pos.parent.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
            }
            yield return 0;
        }
    }
}