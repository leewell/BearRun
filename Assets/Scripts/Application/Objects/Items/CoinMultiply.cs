using System;
using UnityEngine;

public class CoinMultiply : Item
{
    private void Awake()
    {

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
        //播放音效
        Game.Instance.sound.PlayEffectAudio("Se_UI_Stars");
        //回收
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            HitPlayer(other.transform);
            //other.SendMessage("HitCoinMultiply", SendMessageOptions.RequireReceiver);
            other.SendMessage("HitItem", ItemKind.ItemCoinMultiply);
        }
    }
}