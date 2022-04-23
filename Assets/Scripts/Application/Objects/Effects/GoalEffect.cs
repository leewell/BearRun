using System;
using UnityEngine;

public class GoalEffect : Effect
{
    public override void OnSpawn()
    {
        transform.localPosition = new Vector3(0, 13.7f, -2.9f);
        transform.localScale = new Vector3(3.33f, 3.33f, 3.33f);
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
}