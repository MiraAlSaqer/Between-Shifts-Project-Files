using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SchoolArrival : MonoBehaviour
{
    [Header("Target Location")]
    public Vector3 schoolCoordinates = new Vector3(475f, 145f, 700f);
    public float arrivalRadius = 10f;

    [Header("References")]
    public GameObject goodbyeDialogue;
    public GameObject daughterModel;
    public CarController carScript;

    [Header("Transition Settings")]
    public Image fadeOverlay;
    public AudioSource doorSound;
    public float fadeDuration = 1.0f;

    [Header("Final Scene Transition")]
    public string nextSceneName = "Supermarket Scene";
    public float freeDriveTime = 30.0f;

    private bool hasArrived = false;
    private bool dialogueFinished = false;

    [Header("GTA Marker")]
    public GameObject missionMarker;

    void Start()
    {
        fadeOverlay.gameObject.SetActive(true);
        fadeOverlay.color = new Color(0, 0, 0, 1);

        StartCoroutine(StartLevelSequence());
    }

    IEnumerator StartLevelSequence()
    {
        carScript.startEngineSource.Play();
        yield return new WaitForSeconds(carScript.startEngineSource.clip.length);

        carScript.engineSource.Play();

        float timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            fadeOverlay.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }
        fadeOverlay.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!hasArrived)
        {
            float distance = Vector3.Distance(carScript.transform.position, schoolCoordinates);
            if (distance <= arrivalRadius)
            {
                TriggerArrival();
            }
        }
        else if (!dialogueFinished && !goodbyeDialogue.activeInHierarchy)
        {
            dialogueFinished = true;
            StartCoroutine(TransitionSequence());
        }
    }

    public void StartTheFadeOut()
    {
        if (!dialogueFinished)
        {
            dialogueFinished = true;
            StartCoroutine(TransitionSequence());
        }
    }

    void TriggerArrival()
    {
        hasArrived = true;
        missionMarker.SetActive(false);

        Rigidbody rb = carScript.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        carScript.enabled = false;
        goodbyeDialogue.SetActive(true);
    }

    IEnumerator TransitionSequence()
    {
        goodbyeDialogue.SetActive(false);

        float timer = 0;
        fadeOverlay.gameObject.SetActive(true);
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeOverlay.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }

        doorSound.Play();
        yield return new WaitForSeconds(1.5f);
        daughterModel.SetActive(false);

        timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            fadeOverlay.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }
        fadeOverlay.gameObject.SetActive(false);

        Rigidbody rb = carScript.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        carScript.enabled = true;

        yield return new WaitForSeconds(freeDriveTime);

        timer = 0;
        fadeOverlay.gameObject.SetActive(true);
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeOverlay.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(schoolCoordinates, arrivalRadius);
    }
}