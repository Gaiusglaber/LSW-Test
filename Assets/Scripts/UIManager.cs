using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LSWTest.Gameplay.Entities;

public class UIManager : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private PlayerMovement player = null;
    [SerializeField] private NPCPanel NPCPanel = null;
    #endregion
    private void Awake()
    {
        player.OnNpcTalk += NPCPanel.ShowPanel;
        player.OnDeNpcTalk += NPCPanel.HidePanel;
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnDestroy()
    {
        player.OnNpcTalk -= NPCPanel.ShowPanel;
        player.OnDeNpcTalk -= NPCPanel.HidePanel;
    }
}
