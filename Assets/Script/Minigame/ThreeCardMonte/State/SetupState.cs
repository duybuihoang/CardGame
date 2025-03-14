using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupState : GameState
{
    public SetupState(GameManager manager) : base(manager)
    {
        Debug.Log("SetupState");
    }

    public override void EnterState()
    {
        //gameManager.ShuffleCards();
        gameManager.SetTargetCards();
        gameManager.ShowAllCards();

        gameManager.StartCoroutine(gameManager.TransitionToState(new ShuffleState(gameManager), 2f));
    }

    public override void OnCardClicked(Card card)
    {
    }

    public override void UpdateState()
    {
    }
}
