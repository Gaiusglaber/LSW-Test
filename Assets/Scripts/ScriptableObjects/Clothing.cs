using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Clothing", menuName = "Clothes/New Clothing")]
public class Clothing : ScriptableObject
{
    public enum TYPE
    {
        FEET,
        LEG,
        CHEST,
        HAIR
    }
    public string name = string.Empty;
    public AnimatorOverrideController animator = null;
    public Sprite image = null;
    public int price = 0;
    public TYPE type;
    public int id = 0;
}
