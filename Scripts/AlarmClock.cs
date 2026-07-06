using UnityEngine;
using System.Collections;

public class AlarmClock : MonoBehaviour, IInteractable
{
    [Header("Audio & UI")]
    public AudioSource alarmSound;
    public Animator blinkAnimator;

    [Header("Player Scripts")]
    public PlayerMovement movementScript;
    public MonoBehaviour cameraScript;

    [Header("Dialogue")]
    public GameObject dialogueTrigger;

    void Start()
    {
        movementScript.enabled = false;
        cameraScript.enabled = false;
        Invoke(nameof(UnlockLooking), 5.0f);
    }

    void UnlockLooking()
    {
        cameraScript.enabled = true;
    }

    public void Interact(Transform interactorTransform)
    {
        if (alarmSound.isPlaying)
        {
            alarmSound.Stop();

            blinkAnimator.enabled = false;
            blinkAnimator.gameObject.SetActive(false);

            dialogueTrigger.SetActive(true);
            StartCoroutine(WaitToUnlockMovement());
        }
    }

    IEnumerator WaitToUnlockMovement()
    {
        yield return new WaitForSeconds(1.0f); // Allow time for dialogue setup

        while (dialogueTrigger.activeSelf)
        {
            yield return null; // Wait for the next frame while active
        }

        movementScript.enabled = true;
        Debug.Log("Dialogue finished. Movement unlocked!");
    }

    public Transform GetTransform() => transform;
}