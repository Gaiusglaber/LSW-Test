using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LSWTest.Gameplay.Entities;
using UnityEditor;

public class ClothesManager : MonoBehaviour
{
    [SerializeField] public Animator[] animatorControllers = null;//0 FEET, 1 CHEST,2 LEGS,3 HAIR
    [SerializeField] private PlayerMovement player;
    [SerializeField] private UIManager UImanager = null;
    [SerializeField] private ClothesList clothesList = null;
    private List<int> playerClothingID = new List<int>();
    private void Start()
    {
        for (int i=0;i< clothesList.clothesList.Count; i++)
        {
            int aux = 0;
            aux = PlayerPrefs.GetInt(i.ToString(),9999);
            if (aux != 9999)
            {
                playerClothingID.Add(aux);
            }
        }

        foreach (var id in playerClothingID)
        {
            foreach (var clothing in clothesList.clothesList)
            {
                if (clothing.id == id)
                {
                    switch (clothing.type)
                    {
                        case Clothing.TYPE.CHEST:
                            animatorControllers[1].runtimeAnimatorController = clothing.animator;
                            break;
                        case Clothing.TYPE.FEET:
                            animatorControllers[0].runtimeAnimatorController = clothing.animator;
                            break;
                        case Clothing.TYPE.HAIR:
                            animatorControllers[3].runtimeAnimatorController = clothing.animator;
                            break;
                        case Clothing.TYPE.LEG:
                            animatorControllers[2].runtimeAnimatorController = clothing.animator;
                            break;
                    }
                }
            }
        }
        if (UImanager)
        {
            UImanager.OnChangingClothes += ChangeAnimator;
        }
        player.OnMoveDown += SetAnimationDown;
        player.OnMoveLeft += SetAnimationLeft;
        player.OnMoveRight += SetAnimationRight;
        player.OnMoveUp += SetAnimationUp;
    }
    private void OnDestroy()
    {
        foreach (var id in playerClothingID)
        {
            PlayerPrefs.SetInt("" + id, id);
        }
        if (UImanager)
        {
            UImanager.OnChangingClothes -= ChangeAnimator;
        }
        player.OnMoveDown -= SetAnimationDown;
        player.OnMoveLeft -= SetAnimationLeft;
        player.OnMoveRight -= SetAnimationRight;
        player.OnMoveUp -= SetAnimationUp;
    }
    private void ChangeAnimator(Item AnimatorToChange)
    {
        if (AnimatorToChange.ItemType.Equals(Item.TYPE.FEET))
        {
            animatorControllers[0].runtimeAnimatorController = AnimatorToChange.AnimatorController;
        }else if (AnimatorToChange.ItemType.Equals(Item.TYPE.CHEST))
        {
            animatorControllers[1].runtimeAnimatorController = AnimatorToChange.AnimatorController;
        }else if (AnimatorToChange.ItemType.Equals(Item.TYPE.LEG))
        {
            animatorControllers[2].runtimeAnimatorController = AnimatorToChange.AnimatorController;
        }else if (AnimatorToChange.ItemType.Equals(Item.TYPE.HAIR))
        {
            animatorControllers[3].runtimeAnimatorController = AnimatorToChange.AnimatorController;
        }
        playerClothingID.Add(AnimatorToChange.clothing.id);
    }
    private void SetAnimationUp()
    {
        foreach (var animator in animatorControllers)
        {
            animator.SetTrigger("Up");
        }
    }
    private void SetAnimationDown()
    {
        foreach (var animator in animatorControllers)
        {
            animator.SetTrigger("Down");
        }
    }
    private void SetAnimationLeft()
    {
        foreach (var animator in animatorControllers)
        {
            animator.SetTrigger("Left");
        }
    }
    private void SetAnimationRight()
    {
        foreach (var animator in animatorControllers)
        {
            animator.SetTrigger("Right");
        }
    }
    public void ResetClothes()
    {
        PlayerPrefs.DeleteAll();
    }
}
