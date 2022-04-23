using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIResume : View
{
    private Image imgCountDown;
    public Sprite[] countDownSprites;

    public override string Name => Const.V_Resume;

    private void Awake()
    {
        imgCountDown = transform.Find("CountDown").GetComponent<Image>();
    }

    public override void HandleEvent(string eventName, object data)
    {

    }

    public void StartCount()
    {
        Show();
        StartCoroutine(CountDownCor());
    }

    IEnumerator CountDownCor()
    {
        int i = 3;
        while (true)
        {
            imgCountDown.sprite = countDownSprites[i - 1];
            i--;
            yield return new WaitForSeconds(1);
            if (i <= 0)
            {
                break;
            }
        }
        Hide();
        SendEvent(Const.E_ContinueGame);
    }
}