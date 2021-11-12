using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothe", menuName = "Clothes")]
public class Clothes : ScriptableObject
{
    public enum TYPE
    {
        FEET,
        LEG,
        CHEST
    }
    public Animator animator = null;
    public int price = 0;
    public TYPE type;
}
