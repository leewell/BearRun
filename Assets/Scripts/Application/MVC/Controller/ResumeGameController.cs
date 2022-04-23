using System;
using UnityEngine;

public class ResumeGameController : Controller
{
    public override void Excute(object data)
    {
        UIResume uiResume = GetView<UIResume>();
        uiResume.StartCount();
    }
}