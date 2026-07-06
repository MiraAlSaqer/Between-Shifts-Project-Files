using UnityEngine;

public class MissionMarker : MonoBehaviour
{
    [Header("Movement Settings")]
    public float rotationSpeed = 100f;
    public float pulseSpeed = 2f;
    public float minScale = 2f;
    public float maxScale = 3f;

    [Header("Glow Settings")]
    public Light markerLight;
    public float minIntensity = 5f;
    public float maxIntensity = 15f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        float scalePulse = Mathf.PingPong(Time.time * pulseSpeed, maxScale - minScale) + minScale;
        transform.localScale = new Vector3(scalePulse, 0.1f, scalePulse);

        markerLight.intensity = Mathf.PingPong(Time.time * pulseSpeed, maxIntensity - minIntensity) + minIntensity;
    }
}