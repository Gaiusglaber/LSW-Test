using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LSWTest.Gameplay.Entities;

public class UIManager : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private PlayerMovement player = null;
    [SerializeField] private NPCPanel NPCPanel = null;
    [SerializeField] private TMPro.TMP_Text coinText = null;
    [SerializeField] private int cantCoins = 0;
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private float ImageSize = 0;
    #endregion
    #region PRIVATE_FIELDS
    public event Action<Item> OnChangingClothes = null;
    private float InitialImageSize = 0;
    #endregion
    private void Awake()
    {
        InitialImageSize = ImageSize;
        foreach (var item in items)
        {
            item.OnBuyItem += BuyItem;
        }
        player.OnNpcTalk += NPCPanel.ShowPanel;
        player.OnNpcTalk += AddItemsToShop;
        player.OnDeNpcTalk += NPCPanel.HidePanel;
    }
    void Start()
    {
        coinText.text = cantCoins.ToString();
    }
    private void OnDestroy()
    {
        foreach (var item in items)
        {
            item.OnBuyItem -= BuyItem;
        }
        player.OnNpcTalk -= NPCPanel.ShowPanel;
        player.OnNpcTalk -= AddItemsToShop;
        player.OnDeNpcTalk -= NPCPanel.HidePanel;
    }
    private void BuyItem(Item itemToBuy)
    {
        if (int.Parse(itemToBuy.coinText.text)<= cantCoins)
        {
            cantCoins -= int.Parse(itemToBuy.coinText.text);
            coinText.text = cantCoins.ToString();
            OnChangingClothes?.Invoke(itemToBuy);
        }
    }
    private void AddItemsToShop(List<Clothing> NPCList)
    {
        for (short i = 0; i < NPCList.Count; i++)
        {
            items[i].ItemType = (Item.TYPE)NPCList[i].type;
            switch (items[i].ItemType)
            {
                case Item.TYPE.CHEST:
                    items[i].clothImage.rectTransform.anchoredPosition = new Vector2(items[i].clothImage.rectTransform.anchoredPosition.x, -20);
                    ImageSize = InitialImageSize;
                    break;
                case Item.TYPE.LEG:
                    items[i].clothImage.rectTransform.anchoredPosition = new Vector2(items[i].clothImage.rectTransform.anchoredPosition.x, 10);
                    ImageSize = InitialImageSize;
                    break;
                case Item.TYPE.FEET:
                    items[i].clothImage.rectTransform.anchoredPosition = new Vector2(items[i].clothImage.rectTransform.anchoredPosition.x, 20);
                    ImageSize = InitialImageSize;
                    break;
                case Item.TYPE.HAIR:
                    items[i].clothImage.rectTransform.anchoredPosition = new Vector2(items[i].clothImage.rectTransform.anchoredPosition.x, -30);
                    ImageSize = InitialImageSize/1.2f;
                    break;
            }
            items[i].clothImage.sprite = NPCList[i].image;
            items[i].clothImage = IncreaseImageSize(items[i].clothImage);
            items[i].coinText.text = NPCList[i].price.ToString();
            items[i].Title.text = NPCList[i].name;
            items[i].AnimatorController = NPCList[i].animator;
            items[i].clothing = NPCList[i];
        }
    }
    Image IncreaseImageSize(Image ImageToIncrease)
    {
        ImageToIncrease.rectTransform.localScale = new Vector2(ImageSize, ImageSize);
        return ImageToIncrease;
    }
}
