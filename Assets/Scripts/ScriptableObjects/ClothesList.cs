using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothesList", menuName = "Clothes")]
public class ClothesList : ScriptableObject
{
    List<Clothes> clothesList = new List<Clothes>();

}
