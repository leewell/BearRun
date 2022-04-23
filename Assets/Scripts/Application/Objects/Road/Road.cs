using System;
using UnityEngine;

public class Road : ReusableObject
{
    public override void OnSpawn()
    {

    }

    public override void OnUnSpawn()
    {
        //回收Item下的游戏对象
        var itemChild = transform.Find("Item");
        if (itemChild != null)
        {
            foreach (Transform child in itemChild)
            {
                if (child != null)
                {
                    Game.Instance.objectPool.UnSpawn(child.gameObject);
                }
            }
        }
    }
}