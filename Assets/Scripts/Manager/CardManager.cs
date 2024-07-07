using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<PhysicalPowerupItem> cardList = new List<PhysicalPowerupItem>();
    [SerializeField] private GameObject cardPrefabs;
    [SerializeField] private Transform cardContainer;
    private int cardMax = 5;
    private int card;
    public PhysicalPowerupItem cardChoose;

    private void Update()
    {
        if (platformGameManager.Instance.IsBreakBetweenWaves())
        {
            InstantiateCard();
            
        }
        else
        {
            DestroyCard();

        }
    }

    public PhysicalPowerupItem DroppedCard()
    {
        int randomNumber = UnityEngine.Random.Range(1, 100);
        List<PhysicalPowerupItem> possibleCards = new List<PhysicalPowerupItem>();
        foreach (PhysicalPowerupItem item in cardList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleCards.Add(item);
            }
        }
        if (possibleCards.Count > 0)
        {
            PhysicalPowerupItem cardPick = possibleCards[UnityEngine.Random.Range(0, possibleCards.Count)];
            return cardPick;
        }
        
        return null;
    }

    public void InstantiateCard()
    {
        cardChoose = DroppedCard();

        card = cardContainer.childCount;
        if (cardChoose != null)
        {
            
            if(card < cardMax)
            {
                Debug.Log(cardChoose);
                GameObject cardGameObject = Instantiate(cardPrefabs, cardContainer.transform.position, cardContainer.transform.rotation, cardContainer);          
                cardGameObject.GetComponent<CardUI>().cardColor.color = cardChoose.color;
                cardGameObject.GetComponent<CardUI>().cardText.text = cardChoose.ItemName;
                cardGameObject.GetComponent<CardUI>().rarityText.text = cardChoose.rarity.ToString();
                cardGameObject.GetComponent<CardUI>().iconSprite = cardChoose.iconSprite;

            }
        }
    }

    private void DestroyCard()
    {
        for (int i = 0; i < cardContainer.childCount; i++)
        {
            Destroy(cardContainer.GetChild(i).gameObject);
        }
        
    }
}
