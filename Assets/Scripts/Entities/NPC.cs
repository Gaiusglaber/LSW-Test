using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LSWTest.Gameplay.Entities
{
    public class NPC : MonoBehaviour
    {
        public enum TYPE
        {
            FEET,
            LEG,
            CHEST,
            HAIR
        }
        #region EXPOSED_FIELDS
        [SerializeField] private GameObject questObject = null;
        [SerializeField] private TYPE NPCType;
        [SerializeField] private ClothesList clothesList = null;
        #endregion
        #region PRIVATE_FIELDS
        public event Action<List<Clothing>> OnPlayerGetCloseFromNpc = null;
        public event Action OnPlayerGetFarFromNpc = null;
        private List<Clothing> NPCSellerList = new List<Clothing>();
        #endregion

        #region UNITY_CALLS
        private void Start()
        {
            foreach (var clothing in clothesList.clothesList)
            {
                if ((int)clothing.type== (int)NPCType)
                {
                    NPCSellerList.Add(clothing);
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                questObject.SetActive(true);
            }
            OnPlayerGetCloseFromNpc?.Invoke(NPCSellerList);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                questObject.SetActive(false);
            }
            OnPlayerGetFarFromNpc?.Invoke();
        }
        #endregion
    }
}
