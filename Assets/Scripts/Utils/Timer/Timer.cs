using UnityEngine;

public class Timer
{
    #region PRIVATE FIELDS
    private float totalTime = 0f;
    #endregion

    #region PROPERTIES
    public bool Reached { get; private set; }
    public bool On { get; private set; }
    public float CurrTime { get; private set; }
    #endregion

    #region CONSTRUCTORS
    public Timer(float time, bool autoStart = false)
    {
        totalTime = time;
        CurrTime = time;
        SwitchTimer(autoStart);
    }
    #endregion

    #region PUBLIC METHODS
    public void UpdateTimer(float multiplier = 1)
    {
        if (!On)
        {
            return;
        }

        CurrTime -= Time.deltaTime * multiplier;

        if (CurrTime <= 0.0f)
        {
            Reached = true;
            CurrTime = 0.0f;
        }
    }

    public void SetTimer(float time, bool autoStart = false)
    {
        totalTime = time;
        ResetTimer(autoStart);
    }

    public void ResetTimer(bool autoStart = false)
    {
        CurrTime = totalTime;
        Reached = false;
        SwitchTimer(autoStart);
    }

    public void SwitchTimer(bool state)
    {
        On = state;
    }
    #endregion
}