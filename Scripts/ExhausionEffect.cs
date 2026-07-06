using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ExhaustionEffect : MonoBehaviour
{
    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;

    void Start()
    {
        if (globalVolume.profile != null)
        {
            globalVolume.profile.TryGet(out colorAdjustments);
        }
    }

    public void UpdateVisuals(float tirednessPercent)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value = Mathf.Lerp(0, -100, tirednessPercent);
            colorAdjustments.postExposure.value = Mathf.Lerp(0, -0.5f, tirednessPercent);
        }
    }
}