using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Item : MonoBehaviour
{
    public enum TYPE
    {
        FEET,
        LEG,
        CHEST,
        HAIR
    }
    #region EXPOSED_FIELDS
    [SerializeField] public TMPro.TMP_Text coinText = null;
    [SerializeField] public Image clothImage = null;
    [SerializeField] public TMPro.TMP_Text Title = null;
    #endregion
    public event Action<Item> OnBuyItem;
    [NonSerialized] public TYPE ItemType;
    [NonSerialized] public AnimatorOverrideController AnimatorController = null;
    #region PUBLIC_METHODS
    public void BuyItem()
    {
        OnBuyItem?.Invoke(this);
    }
    #endregion
}
