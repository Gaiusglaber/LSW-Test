using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LSWTest.Gameplay.Entities
{
    public class NPC : MonoBehaviour
    {
        public static event Action OnPlayerGetCloseFromNpc = null;
        public static event Action OnPlayerGetFarFromNpc = null;
        [SerializeField] private GameObject questObject = null;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                questObject.SetActive(true);
            }
            OnPlayerGetCloseFromNpc?.Invoke();
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                questObject.SetActive(false);
            }
            OnPlayerGetFarFromNpc?.Invoke();
        }
    }
}
