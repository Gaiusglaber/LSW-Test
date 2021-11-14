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
        [SerializeField] private List<NPC> NPCList = new List<NPC>();
        #endregion
        #region PRIVATE_FIELDS
        private Rigidbody2D rigidbody;
        private Animator animator = null;
        private float horizontalMovement = 0;
        private float verticalMovement = 0;
        private bool CanTalkToNpc = false;
        private bool talkingToNpc = false;
        public event Action<List<Clothing>> OnNpcTalk = null;
        public event Action OnDeNpcTalk = null;
        public event Action OnMoveUp = null;
        public event Action OnMoveDown = null;
        public event Action OnMoveRight = null;
        public event Action OnMoveLeft = null;
        private List<Clothing> listToPass = new List<Clothing>();
        #endregion
        #region UNITY_CALLS
        private void Awake()
        {
            foreach (var NPC in NPCList){
                if (NPC)
                {
                    NPC.OnPlayerGetCloseFromNpc += ActivateTalkInput;
                    NPC.OnPlayerGetFarFromNpc += DeactivateTalkInput;
                }
            }
            
        }
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            InputManagment();
        }
        private void OnDestroy()
        {
            foreach (var NPC in NPCList)
            {
                if (NPC)
                {
                    NPC.OnPlayerGetCloseFromNpc -= ActivateTalkInput;
                    NPC.OnPlayerGetFarFromNpc -= DeactivateTalkInput;
                }
            }
        }
        #endregion
        #region PRIVATE_FIELDS
        private void InputManagment()
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");
            Vector2 positionToMove = new Vector2(horizontalMovement, verticalMovement);
            rigidbody.velocity = positionToMove * speed;
            //transform.Translate(positionToMove*speed*Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.E) && CanTalkToNpc&& !talkingToNpc)
            {
                OnNpcTalk?.Invoke(listToPass);
                talkingToNpc = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape) && CanTalkToNpc&& talkingToNpc)
            {
                OnDeNpcTalk?.Invoke();
                talkingToNpc = false;
            }
            if (verticalMovement > 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
                OnMoveUp?.Invoke();
                animator.SetTrigger("Up");

            }
            else if (verticalMovement < 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
                OnMoveDown?.Invoke();
                animator.SetTrigger("Down");
            }
            else if (horizontalMovement > 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(verticalMovement));
                OnMoveRight?.Invoke();
                animator.SetTrigger("Right");
            }
            else if (horizontalMovement < 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(verticalMovement));
                OnMoveLeft?.Invoke();
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
        private void ActivateTalkInput(List<Clothing> NPCLIST)
        {
            listToPass = NPCLIST;
            CanTalkToNpc = true;
        }
        private void DeactivateTalkInput()
        {
            CanTalkToNpc = false;
            if (talkingToNpc)
            {
                OnDeNpcTalk?.Invoke();
                talkingToNpc = false;
            }
        }
        #endregion
    }
}
