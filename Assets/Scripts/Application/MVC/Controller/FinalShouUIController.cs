using System;
using UnityEngine;

public class FinalShouUIController : Controller
{
    public override void Excute(object data)
    {
        GameModel gm = GetModel<GameModel>();

        UIBoard board = GetView<UIBoard>();
        board.Hide();
        UIDead dead = GetView<UIDead>();
        dead.Hide();
        UIFinalScore finalScore = GetView<UIFinalScore>();
        finalScore.Show();
        //更新exp
        gm.Exp = board.Distance * (board.Score + 1) + board.Coin;
        //更新UI
        finalScore.UpdateUI(board.Distance, board.Coin, board.Score, gm.Exp, gm.Grade);
        gm.Coin += board.Coin;
    }
}