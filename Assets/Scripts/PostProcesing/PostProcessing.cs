using System.Collections;
using UnityEngine;
using Numetry.Tools.Lerper;
using UnityEngine.Rendering.PostProcessing;
using LSWTest.Gameplay.Entities;

public class PostProcessing : MonoBehaviour,ILerpeable
{
    #region EXPOSED_FIELDS
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float amountOfBlur = 0;
    [SerializeField] private float speedLerper = 0;
    [SerializeField] private PostProcessVolume postProcessProfile;
    #endregion
    #region PRIVATE_FIELDS
    private DepthOfField depthOfField = null;
    private FloatLerper floatLerper = null;
    private float InitialBlurValue = 0;

    #endregion
    private void Awake()
    {
        player.OnNpcTalk += Blur;
        player.OnDeNpcTalk += UnBlur;
    }
    void Start()
    {
        floatLerper = new FloatLerper(Time.deltaTime, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
        postProcessProfile.profile.TryGetSettings(out depthOfField);
        InitialBlurValue = depthOfField.focusDistance.value;
    }
    private void OnDestroy()
    {
        player.OnNpcTalk -= Blur;
        player.OnDeNpcTalk -= UnBlur;
    }
    private void Blur()
    {
        StartCoroutine(Lerp(InitialBlurValue, amountOfBlur, speedLerper));
    }
    private void UnBlur()
    {
        StartCoroutine(Lerp(amountOfBlur, InitialBlurValue, speedLerper));
    }
    public IEnumerator Lerp(float firstPos, float endPos, float speed)
    {
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
                depthOfField.focusDistance.value = floatLerper.CurrentValue;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
