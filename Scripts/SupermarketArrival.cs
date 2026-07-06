using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SupermarketArrival : MonoBehaviour
{
    [Header("Player References")]
    public GameObject carObject;
    public MonoBehaviour carScript;
    public GameObject walkingFather;
    public Transform spawnPoint;

    [Header("Transition Settings")]
    public Image fadeOverlay;
    public AudioSource doorOpenSound;
    public float fadeDuration = 1.0f;
    public float initialWaitTime = 1.5f;

    [Header("Audio Listener Management")]
    public AudioListener carListener;
    public AudioListener fatherListener;

    private bool hasArrived = false;

    void Start()
    {
        StartCoroutine(SceneEntryFade());
    }

    IEnumerator SceneEntryFade()
    {
        fadeOverlay.gameObject.SetActive(true);
        fadeOverlay.color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(initialWaitTime);

        float timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            fadeOverlay.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }

        fadeOverlay.gameObject.SetActive(false);
    }

    public void StartArrivalSequence()
    {
        if (!hasArrived)
        {
            hasArrived = true;
            StartCoroutine(ExitCarSequence());
        }
    }

    IEnumerator ExitCarSequence()
    {
        carScript.enabled = false;

        Rigidbody rb = carObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        float timer = 0;
        fadeOverlay.gameObject.SetActive(true);
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeOverlay.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }

        Camera carCam = carObject.GetComponentInChildren<Camera>();
        if (carCam != null) carCam.gameObject.SetActive(false);
        carListener.enabled = false;

        walkingFather.transform.position = spawnPoint.position;
        walkingFather.SetActive(true);
        fatherListener.enabled = true;

        AudioSource fatherAudio = walkingFather.GetComponent<AudioSource>();
        if (fatherAudio != null) fatherAudio.mute = true;

        doorOpenSound.Play();
        yield return new WaitForSeconds(1.0f);

        timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            fadeOverlay.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }

        if (fatherAudio != null) fatherAudio.mute = false;
        fadeOverlay.gameObject.SetActive(false);
    }
}