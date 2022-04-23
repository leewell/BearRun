using System;
using System.Collections;
using UnityEngine;

public class Effect : ReusableObject
{
    //销毁撞击特效的时间
    public float destroyTime = 1f;

    private void Start()
    {
        //Invoke("DestroyEffect", destroyTime);
        //教程里通过启动一个协程来延迟销毁
        //StartCoroutine(DestroyEffectByCoroutine());
    }
    public override void OnSpawn()
    {
        Invoke("DestroyEffect", destroyTime);
    }

    public override void OnUnSpawn()
    {
        StopAllCoroutines();
    }

    private void DestroyEffect()
    {
        Game.Instance.objectPool.UnSpawn(gameObject);
    }

    IEnumerator DestroyEffectByCoroutine()
    {
        yield return new WaitForSeconds(destroyTime);
    }
}