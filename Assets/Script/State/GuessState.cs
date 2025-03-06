using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GuessState : GameState
{
    public GuessState(GameManager manager) : base(manager)
    {
        Debug.Log("GuessState");
    }

    public override void EnterState()
    {
        gameManager.EnableCardInteraction(true);
    }

    public override void OnCardClicked(Card card)
    {
        Debug.Log(card.name + " clicked!!!");
        gameManager.EnableCardInteraction(false);
        gameManager.RevealResult(card);

        gameManager.StartCoroutine(gameManager.TransitionToState(new SetupState(gameManager), 2f));
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
