using System;
using UnityEngine;

public class EnterSceneController : Controller
{
    public override void Excute(object data)
    {
        SceneArgs args = data as SceneArgs;
        switch (args.scenesIndex)
        {
            case 1:
                RegisterView(GameObject.Find("UIMainMenu").GetComponent<UIMainMenu>());
                Game.Instance.sound.PlayBgAudio("Bgm_JieMian");
                break;
            case 2:
                RegisterView(GameObject.Find("UIShop").GetComponent<UIShop>());
                Game.Instance.sound.PlayBgAudio("Bgm_JieMian");
                break;
            case 3:
                RegisterView(GameObject.Find("Canvas").transform.Find("UIBuyTools").GetComponent<UIBuyTools>());
                Game.Instance.sound.PlayBgAudio("Bgm_JieMian");
                break;
            case 4:
                Game.Instance.sound.PlayBgAudio("Bgm_ZhanDou");
                RegisterView(GameObject.FindWithTag(Tag.Player).GetComponent<PlayerMove>());
                RegisterView(GameObject.FindWithTag(Tag.Player).GetComponent<PlayerAnim>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIPause").GetComponent<UIPause>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIResume").GetComponent<UIResume>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIDead").GetComponent<UIDead>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIFinalScore").GetComponent<UIFinalScore>());

                GameModel gm = GetModel<GameModel>();
                gm.IsPause = false;
                gm.IsPlay = true;
                break;
            default:
                break;
        }
    }
}