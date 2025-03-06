using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, GameObserver
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float textAnimDuration = 0.5f;

    private void Start()
    {
        gameManager.AddObserver(this);
        messageText.alpha = 0f;
    }
    public void OnGameEvent(string message)
    {
        messageText.DOFade(0f, textAnimDuration / 2).OnComplete(() =>
        {
            messageText.text = message;
            messageText.DOFade(1f, textAnimDuration / 2);
        });

    }

    private void OnDestroy()
    {
        gameManager.RemoveObserver(this);
        DOTween.Kill(messageText);
    }
}
