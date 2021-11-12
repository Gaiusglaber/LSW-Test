using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothesList", menuName = "Clothes/Clothing List")]
public class ClothesList : ScriptableObject
{
    public List<Clothing> clothesList = new List<Clothing>();
}
