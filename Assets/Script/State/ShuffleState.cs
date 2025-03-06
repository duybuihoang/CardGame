using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleState : GameState
{
    private int shuffleCount = 0;
    private const int TOTAL_SHUFFLES = 5;
    public ShuffleState(GameManager manager) : base(manager)
    {
        Debug.Log("ShuffleState");
    }

    public override void EnterState()
    {
        gameManager.HideAllCards();
        DoShuffle();

    }

    private void DoShuffle()
    {
        if(shuffleCount >= TOTAL_SHUFFLES)
        {
            gameManager.ChangeState(new GuessState(gameManager));
            return;
        }

        int cardA = Random.Range(0, 3);
        int cardB;
        do
        {
            cardB = Random.Range(0, 3);
        } while (cardA == cardB);

        shuffleCount++;
        gameManager.SwapCards(cardA, cardB);

        gameManager.StartCoroutine(gameManager.DelayedAction(DoShuffle, 0.5f));

    }

    public override void OnCardClicked(Card card)
    {
    }

    public override void UpdateState()
    {
    }
}
