using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public List<Card> cards;
    private Card correctCard;

    private void Start()
    {
        Shuffle();
    }
    private void Shuffle()
    {
        int correctIndex = Random.Range(0, cards.Count);
        correctCard = cards[correctIndex];
        

        StartCoroutine(ShuffleAnimation());

    }
    private IEnumerator ShuffleAnimation()
    {
        int shuffleTimes = 10;
        float shuffleDuration = 0.5f;
        DOTween.Init();

        for (int i = 0; i < shuffleTimes; i++)
        {
            int indexA = Random.Range(0, cards.Count);
            int indexB = Random.Range(0, cards.Count);

            if (indexA != indexB)
            {
                Transform cardA = cards[indexA].transform;
                Transform cardB = cards[indexB].transform;

                Vector3 posA = cardA.position;
                Vector3 posB = cardB.position;
                

                cardA.DOMove(posB, shuffleDuration);
                cardB.DOMove(posA, shuffleDuration);

                yield return new WaitForSeconds(shuffleDuration);
            }
        }
    }
}
