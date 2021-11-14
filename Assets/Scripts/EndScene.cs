using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EndScene : MonoBehaviour
{
    public static event Action OnExitScene = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(GameManager.Get().ChangeScene(tag));
            OnExitScene?.Invoke();
        }
    }
}
