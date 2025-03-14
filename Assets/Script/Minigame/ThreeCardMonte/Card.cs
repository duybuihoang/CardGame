using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] public Sprite hiddenSprite;
    private Sprite revealedSprite;
    private SpriteRenderer currentSprite;

    public bool isTarget = false;
    private Vector3 initialPosition;

    [SerializeField] private float flipDuration = 0.3f;

    public bool IsTarget { get => isTarget; set => isTarget = value; }
    public Vector3 InitialPosition { get => initialPosition; set => initialPosition = value; }

    public event Action<Card> onCardSelected;


    private void Awake()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        currentSprite.sprite = hiddenSprite;
    }

    public void FlipCard(bool faceUp)
    {
        currentSprite.sprite = faceUp ? revealedSprite : hiddenSprite;
    }

    public void SetRevealedCardSprite(Sprite sprite)
    {
        revealedSprite = sprite;
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }

    private void OnMouseEnter()
    {
        Debug.Log(this.name);
    }

    private void OnMouseDown()
    {
        onCardSelected?.Invoke(this);
    }
}
