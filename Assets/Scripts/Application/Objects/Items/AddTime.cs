using System;
using UnityEngine;

public class AddTime : Item
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            HitPlayer(other.transform);
            other.SendMessage("HitAddTime", SendMessageOptions.RequireReceiver);
        }
    }

    public override void HitPlayer(Transform pos)
    {
        //播放音效
        Game.Instance.sound.PlayEffectAudio("Se_UI_Time");
        //回收
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
}