using System;
using UnityEngine;

public class Item : ReusableObject
{
    [Header("每秒转的度数")]
    public float speed = 60f;
    public override void OnSpawn()
    {
        transform.Rotate(new Vector3(0, speed, 0) * Time.deltaTime);
    }

    public override void OnUnSpawn()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    public virtual void HitPlayer(Transform pos)
    {

    }
}