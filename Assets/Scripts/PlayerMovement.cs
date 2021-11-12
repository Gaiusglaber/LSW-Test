using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace LSWTest.Gameplay.Entities
{
    public class PlayerMovement : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Range(1,50)][SerializeField] private float speed = 0;
        #endregion
        #region PRIVATE_FIELDS
        private Animator animator = null;
        private float horizontalMovement = 0;
        private float verticalMovement = 0;
        private bool CanTalkToNpc = false;
        public event Action OnNpcTalk = null;
        public event Action OnDeNpcTalk = null;
        #endregion
        #region UNITY_CALLS
        private void Awake()
        {
            NPC.OnPlayerGetCloseFromNpc += ActivateTalkInput;
            NPC.OnPlayerGetFarFromNpc += DeactivateTalkInput;
        }
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            InputManagment();
        }
        private void OnDestroy()
        {
            NPC.OnPlayerGetCloseFromNpc -= ActivateTalkInput;
            NPC.OnPlayerGetFarFromNpc -= DeactivateTalkInput;
        }
        #endregion
        #region PRIVATE_FIELDS
        private void InputManagment()
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");
            Vector2 positionToMove = new Vector2(horizontalMovement, verticalMovement);
            transform.Translate(positionToMove*speed*Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.E) && CanTalkToNpc)
            {
                OnNpcTalk?.Invoke();
                CanTalkToNpc = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnDeNpcTalk?.Invoke();
            }

            if (verticalMovement > 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
                animator.SetTrigger("Up");
            }else if (verticalMovement < 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
                animator.SetTrigger("Down");
            }
            if (horizontalMovement > 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(verticalMovement));
                animator.SetTrigger("Right");
            }else if (horizontalMovement < 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(verticalMovement));
                animator.SetTrigger("Left");
            }
        }
        private void ResetTriggers()
        {
            animator.ResetTrigger("Up");
            animator.ResetTrigger("Down");
            animator.ResetTrigger("Left");
            animator.ResetTrigger("Right");
        }
        private void ActivateTalkInput()
        {
            CanTalkToNpc = true;
        }
        private void DeactivateTalkInput()
        {
            CanTalkToNpc = false;
        }
        #endregion
    }
}
