using System;
using UnityEngine;

public class PlayerAnim : View
{
	private Animation anim;
    private Action PlayAction;
    private GameModel gm;

    public override string Name {
        get => Const.V_PlayerAnim;
    }

    private void Awake()
    {
        anim = GetComponent<Animation>();
        gm = GetModel<GameModel>();
        PlayAction = PlayRun;
    }

    private void PlayRun()
    {
        anim.Play("run");
    }

    private void PlayRight()
    {
        anim.Play("right_jump");
        if (anim["right_jump"].normalizedTime > 0.95f)
        {
            PlayAction = PlayRun;
        }
    }

    private void PlayLeft()
    {
        anim.Play("left_jump");
        if (anim["left_jump"].normalizedTime > 0.95f)
        {
            PlayAction = PlayRun;
        }
    }

    private void PlayRoll()
    {
        anim.Play("roll");
        if (anim["roll"].normalizedTime > 0.95f)
        {
            PlayAction = PlayRun;
        }
    }

    private void PlayJump()
    {
        anim.Play("jump");
        if (anim["jump"].normalizedTime > 0.95f)
        {
            PlayAction = PlayRun;
        }
    }

    /// <summary>
    /// 播放射门动画
    /// </summary>
    private void PlayShoot()
    {
        anim.Play("Shoot01");
        if (anim["Shoot01"].normalizedTime > 0.95f)
        {
            PlayAction = PlayRun;
        }
    }

    public void NoticePlayShoot()
    {
        PlayAction = PlayShoot;
    }

    private void Update()
    {
        if (PlayAction != null)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                PlayAction();
            }
            else
            {
                anim.Stop();
            }
        }
    }

    public void AnimManager(InputDirection dir)
    {
        switch (dir)
        {
            case InputDirection.Null:
                break;
            case InputDirection.Left:
                PlayAction = PlayLeft;
                break;
            case InputDirection.Right:
                PlayAction = PlayRight;
                break;
            case InputDirection.Down:
                PlayAction = PlayRoll;
                break;
            case InputDirection.Up:
                PlayAction = PlayJump;
                break;
            default:
                break;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
        throw new NotImplementedException();
    }
}