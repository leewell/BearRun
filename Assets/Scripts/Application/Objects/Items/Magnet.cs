using System;
using UnityEngine;

public class Magnet : Item
{
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
        Game.Instance.sound.PlayEffectAudio("Se_UI_Magnet");
        //回收
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            HitPlayer(other.transform);
            //other.SendMessage("HitMagnet", SendMessageOptions.RequireReceiver);
            other.SendMessage("HitItem", ItemKind.ItemMagnet);
        }
    }
}