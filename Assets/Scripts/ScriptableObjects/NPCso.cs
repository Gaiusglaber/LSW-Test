using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPCs")]
public class NPCSO : ScriptableObject
{
    public enum NPCTYPES
    {
        FEET,
        LEG,
        CHEST
    }
    public NPCTYPES npcType;
    public RuntimeAnimatorController animatorController = null;
    public int ID = 0;
    public Sprite chest = null;
    public Sprite legs = null;
    public Sprite feet = null;

}
