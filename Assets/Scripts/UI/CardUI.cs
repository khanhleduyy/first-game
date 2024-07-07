using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CardUI : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string GAME_MANAGER = "GameManager";

    public event EventHandler OnCardPick;
    public Button button;
    public Image cardColor;
    public Color color;
    public TextMeshProUGUI cardText;
    public TextMeshProUGUI rarityText;
    public Image iconImage;
    public Sprite iconSprite;
    [SerializeField] private GameObject cardPrefabs;
    [SerializeField] private PhysicalPowerupItem physicalPowerupItem;
    [SerializeField] private AudioClipSO audioClipSO;

    private GameObject player;
    private GameObject gameManager;
    private CardManager cardManager;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(PLAYER);
        gameManager = GameObject.FindGameObjectWithTag(GAME_MANAGER);
        cardManager = gameManager.GetComponent<CardManager>();
        color = cardColor.GetComponent<Image>().color;
        iconSprite = iconImage.GetComponent<Image>().sprite;
        physicalPowerupItem = cardManager.cardChoose;
        button.onClick.AddListener(PlayClick);
    }

    private void Update()
    {
        
    }

    public void PlayClick()
    {
        physicalPowerupItem.Apply(player);
        AudioSource.PlayClipAtPoint(audioClipSO.cardChoose, Camera.main.transform.position, .5f);
        OnCardPick?.Invoke(this, EventArgs.Empty);
    }
    

}
