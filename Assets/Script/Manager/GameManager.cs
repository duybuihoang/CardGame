using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Card> cards;

    [SerializeField] private float swapDuration = 0.5f;
    [SerializeField] private float moveDuration = 0.5f;

    [SerializeField] private Ease swapEase = Ease.InOutQuad;
    [SerializeField] private Ease moveEase = Ease.InSine;

    [SerializeField] private float cardMoveHeight = 0.1f;

    [SerializeField] private Sprite targetCardSprite;
    [SerializeField] private Sprite normalCardSprite;

    private GameState currentState;
    private List<GameObserver> observers = new List<GameObserver>();

    private void Start()
    {
        DOTween.SetTweensCapacity(500, 10);
        ChangeState(new SetupState(this));
    }

    public void ChangeState(GameState state)
    {
        currentState = state;
        currentState.EnterState();
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
    public void AddObserver(GameObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(GameObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObserver(string message)
    {
        foreach (var item in observers)
        {
            item.OnGameEvent(message);
        }
    }
    public void ShuffleCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, i - 1);
            SwapCards(0, randomIndex);
        }
    }

    public void SwapCards(int idx1, int idx2)
    {
        Card cardA = cards[idx1];
        Card cardB = cards[idx2];

        Vector3 posA = cardA.transform.position;
        Vector3 posB = cardB.transform.position;

        Vector3[] pathA = new Vector3[]
        {
            posA,
            posA + Vector3.up* cardMoveHeight, 
            posB + Vector3.up* cardMoveHeight, 
            posB
        };

        Vector3[] pathB = new Vector3[]
        {
            posB,
            posB + Vector3.up* -cardMoveHeight,
            posA + Vector3.up* -cardMoveHeight,
            posA
        };

        cardA.transform.DOPath(pathA, swapDuration, PathType.CatmullRom,PathMode.TopDown2D).SetEase(swapEase);

        cardB.transform.DOPath(pathB, swapDuration, PathType.CatmullRom, PathMode.TopDown2D).SetEase(swapEase);

    }
    public void BindCardClickedEvent(Action<Card> action)
    {
        foreach (var card in cards)
        {
            card.onCardSelected += action;
        }
    }    
    public void SetTargetCards()
    {
        int targetIndex = UnityEngine.Random.Range(0, cards.Count);
        for (int i = 0; i < cards.Count; i++)
        {
            if(i == targetIndex)
            {
                cards[i].IsTarget = (i == targetIndex);
                cards[i].SetRevealedCardSprite(targetCardSprite);
            }
            else
            {
                cards[i].SetRevealedCardSprite(normalCardSprite);
            }
        }
        MoveTargetCardUp(targetIndex);
    }

    public void MoveTargetCardUp(int targetIndex)
    {
        
        Vector3[] path = new Vector3[]
        {
            cards[targetIndex].transform.position + new Vector3(0, cardMoveHeight, 0),
            cards[targetIndex].transform.position
        };

        cards[targetIndex].transform.DOPath(path, moveDuration, PathType.CatmullRom, PathMode.TopDown2D).SetEase(moveEase);
    }

    public void ShowAllCards() {
        foreach (var item in cards)
        {
            item.FlipCard(true);
        }
    }

    public void HideAllCards()
    {
        foreach (var item in cards)
        {
           item.FlipCard(false);
        }
    }

    public void EnableCardInteraction(bool enable)
    {
        foreach (var card in cards)
        {
            card.GetComponent<Collider2D>().enabled = enable;
        }
    }

    public void RevealResult(Card selectedCard)
    {
        ShowAllCards();

        selectedCard.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 5, 0.5f)
            .OnComplete(() => {
                string result = selectedCard.IsTarget ? "correct!" : "wrong!";
                NotifyObserver(result);
            });
    }

    public IEnumerator TransitionToState(GameState newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(newState);
    }

    public IEnumerator DelayedAction(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
