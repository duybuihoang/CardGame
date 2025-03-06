using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] public Sprite hiddenSprite;
    [SerializeField] public Sprite revealedSprite;
    private SpriteRenderer currentSprite;

    private bool isTarget = false;
    private Vector3 initialPosition;

    [SerializeField] private float flipDuration = 0.3f;

    public bool IsTarget { get => isTarget; set => isTarget = value; }
    public Vector3 InitialPosition { get => initialPosition; set => initialPosition = value; }


    private void Awake()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        currentSprite.sprite = hiddenSprite;
    }

   public void FlipCard(bool faceUp)
   {
        transform.DORotate(new Vector3( faceUp ? -135f : -45f,0, 0), flipDuration).SetEase(Ease.InOutQuad);
   }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}
