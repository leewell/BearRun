using System;
using UnityEngine;

public class Net : Effect
{
    public override void OnSpawn()
    {
        transform.localPosition = new Vector3(-0.4f, 0f, -3.56f);
        transform.localScale = Vector3.one * 1.66f;
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
}