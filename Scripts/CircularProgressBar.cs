using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour
{
    private bool isActive = false;
    private float indicatorTimer;
    private float maxIndicatorTimer;
    private Image radialProgressBar;

    [Header("Visual Effects")]
    public ExhaustionEffect exhaustionVisuals;

    private void Awake()
    {
        radialProgressBar = GetComponent<Image>();
    }

    private void Update()
    {
        if (!isActive) return;

        indicatorTimer -= Time.deltaTime;
        float progress = indicatorTimer / maxIndicatorTimer;
        radialProgressBar.fillAmount = progress;

        // Higher exhaustion values pass down as time drains
        exhaustionVisuals.UpdateVisuals(1f - progress);

        if (indicatorTimer <= 0)
        {
            StopCountdown();
        }
    }

    public void ActivateCountdown(float countdownTime)
    {
        isActive = true;
        maxIndicatorTimer = countdownTime;
        indicatorTimer = maxIndicatorTimer;
    }

    public void StopCountdown()
    {
        isActive = false;
        SendMessageUpwards("OnTiredTimerFinished", SendMessageOptions.DontRequireReceiver);
    }
}