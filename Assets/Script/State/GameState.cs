using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected GameManager gameManager;

    public GameState(GameManager manager)
    {
        this.gameManager = manager;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void OnCardClicked(Card card);

}
