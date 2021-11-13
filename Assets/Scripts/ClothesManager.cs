using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSWTest.Gameplay.Entities;

public class ClothesManager : MonoBehaviour
{
    [SerializeField] private Animator[] animatorControllers=null;//0 FEET, 1 CHEST,2 LEGS,3 HAIR
    [SerializeField] private PlayerMovement player;
    [SerializeField] private UIManager UImanager = null;
    private void Start()
    {
        UImanager.OnChangingClothes += ChangeAnimator;
        player.OnMoveDown += SetAnimationDown;
        player.OnMoveLeft += SetAnimationLeft;
        player.OnMoveRight += SetAnimationRight;
        player.OnMoveUp += SetAnimationUp;
    }
    private void OnDestroy()
    {
        UImanager.OnChangingClothes -= ChangeAnimator;
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
}
