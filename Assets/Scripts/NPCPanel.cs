using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Numetry.Tools.Lerper;

public class NPCPanel : MonoBehaviour, ILerpeable
{
    #region EXPOSED_FIELDS
    [SerializeField] private float destXPos = 0;
    [SerializeField] private float lerperSpeed = 0;
    #endregion
    #region PRIVATE_FIELDS
    private FloatLerper floatLerper = null;
    private float initialXPos = 0;
    #endregion
    #region UNITY_CALLS
    private void Awake()
    {
        
    }
    private void Start()
    {
        initialXPos = GetComponent<RectTransform>().anchoredPosition.x;
        floatLerper = new FloatLerper(Time.deltaTime, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
    }
    private void OnDestroy()
    {
        
    }
    public void ShowPanel()
    {
        StartCoroutine(Lerp(initialXPos, destXPos, lerperSpeed));
    }
    public void HidePanel()
    {
        StartCoroutine(Lerp(destXPos, initialXPos, lerperSpeed));
    }
    #endregion
    public IEnumerator Lerp(float firstPos, float endPos, float speed)
    {
        float xPos = 0;
        RectTransform pos = GetComponent<RectTransform>();
        floatLerper.SetValues(firstPos, endPos, speed, true);
        while (floatLerper.On)
        {
            if (floatLerper.Reached)
            {
                floatLerper.SwitchState(false);
            }
            else
            {
                floatLerper.Update();
                xPos = floatLerper.CurrentValue;
                pos.anchoredPosition = new Vector2(xPos, pos.anchoredPosition.y);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
