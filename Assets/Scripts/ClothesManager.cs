using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSWTest.Gameplay.Entities;

public class ClothesManager : MonoBehaviour
{
    [SerializeField] private Animator[] animatorControllers=null;
    [SerializeField] private PlayerMovement player;
    private void Start()
    {
        player.OnMoveDown += SetAnimationDown;
        player.OnMoveLeft += SetAnimationLeft;
        player.OnMoveRight += SetAnimationRight;
        player.OnMoveUp += SetAnimationUp;
    }
    private void OnDestroy()
    {
        player.OnMoveDown -= SetAnimationDown;
        player.OnMoveLeft -= SetAnimationLeft;
        player.OnMoveRight -= SetAnimationRight;
        player.OnMoveUp -= SetAnimationUp;
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
