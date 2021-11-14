using System.Collections;
using System.Collections.Generic;
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
    private Vignette vignette = null;
    private FloatLerper DepthLerper = null;
    private FloatLerper VignetteLerper = null;
    private float InitialBlurValue = 0;

    #endregion
    private void Awake()
    {
        EndScene.OnExitScene += VignetteIn;
        if (player)
        {
            player.OnNpcTalk += Blur;
            player.OnDeNpcTalk += UnBlur;
        }
    }
    void Start()
    {
        VignetteLerper = new FloatLerper(Time.deltaTime, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
        DepthLerper = new FloatLerper(Time.deltaTime, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
        postProcessProfile.profile.TryGetSettings(out depthOfField);
        postProcessProfile.profile.TryGetSettings(out vignette);
        InitialBlurValue = depthOfField.focusDistance.value;
        StartCoroutine(LerpVignette(1, 0, 2));
    }
    private void OnDestroy()
    {
        EndScene.OnExitScene -= VignetteIn;
        if (player)
        {
            player.OnNpcTalk -= Blur;
            player.OnDeNpcTalk -= UnBlur;
        }
    }
    private void VignetteIn()
    {
        StartCoroutine(LerpVignette(0, 1, 2));
    }
    private void Blur(List<Clothing> NPCList)
    {
        StartCoroutine(Lerp(InitialBlurValue, amountOfBlur, speedLerper));
    }
    private void UnBlur()
    {
        StartCoroutine(Lerp(amountOfBlur, InitialBlurValue, speedLerper));
    }
    public IEnumerator LerpVignette(float firstPos, float endPos, float speed)
    {
        VignetteLerper.SetValues(firstPos, endPos, speed, true);
        while (VignetteLerper.On)
        {
            if (VignetteLerper.Reached)
            {
                VignetteLerper.SwitchState(false);
            }
            else
            {
                VignetteLerper.Update();
                vignette.intensity.value = VignetteLerper.CurrentValue;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator Lerp(float firstPos, float endPos, float speed)
    {
        DepthLerper.SetValues(firstPos, endPos, speed, true);
        while (DepthLerper.On)
        {
            if (DepthLerper.Reached)
            {
                DepthLerper.SwitchState(false);
            }
            else
            {
                DepthLerper.Update();
                depthOfField.focusDistance.value = DepthLerper.CurrentValue;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
