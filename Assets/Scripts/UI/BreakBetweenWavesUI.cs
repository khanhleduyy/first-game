using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBetweenWavesUI : MonoBehaviour
{
    [SerializeField] private Transform cardContainer;
    [SerializeField] private Transform card;

    private void Awake()
    {
        //card.gameObject.SetActive(false);
    }

    private void Start()
    {
        platformGameManager.Instance.OnStateChanged += platformGameManager_OnStateChanged;

        Hide();
    }

    private void platformGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (platformGameManager.Instance.IsBreakBetweenWaves())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        //scoreText.text = 
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        
    }
}
