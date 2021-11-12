using System.Collections;
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
    private void Awake()
    {
        foreach (var item in items)
        {
            
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
        player.OnNpcTalk -= NPCPanel.ShowPanel;
        player.OnNpcTalk -= AddItemsToShop;
        player.OnDeNpcTalk -= NPCPanel.HidePanel;
    }
    private void UpdateCoins(int CoinsToDescount)
    {
        cantCoins -= CoinsToDescount;
        coinText.text = cantCoins.ToString();
    }
    private void AddItemsToShop(List<Clothing> NPCList)
    {
        for (short i = 0; i < NPCList.Count; i++)
        {
            items[i].clothImage.sprite = NPCList[i].image;
            items[i].clothImage = IncreaseImageSize(items[i].clothImage);
            items[i].coinText.text = NPCList[i].price.ToString();
            items[i].Title.text = NPCList[i].name;
        }
    }
    Image IncreaseImageSize(Image ImageToIncrease)
    {
        ImageToIncrease.rectTransform.sizeDelta = new Vector2(ImageToIncrease.rectTransform.sizeDelta.x+ImageSize, ImageToIncrease.rectTransform.sizeDelta.y + ImageSize);
        return ImageToIncrease;
    }
}
