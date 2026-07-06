using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DoorExit : MonoBehaviour, IInteractable
{
    public GameObject exitUIPanel;

    [Header("Player Parts to Lock")]
    public GameObject fatherObject;
    public GameObject orientation;
    public GameObject cameraPos;
    public GameObject playerCam;

    [Header("Transition Settings")]
    public Image fadeOverlay;
    public AudioSource doorCloseSound;
    public float fadeDuration = 1.0f;
    public string carSceneName = "Car Scene";

    private bool isTransitioning = false;

    void Start()
    {
        doorCloseSound.Stop();
        doorCloseSound.playOnAwake = false;
        fadeOverlay.gameObject.SetActive(false);
        exitUIPanel.SetActive(false);
    }

    public void Interact(Transform interactorTransform)
    {
        if (!isTransitioning)
        {
            SetScriptsEnabled(fatherObject, false);
            SetScriptsEnabled(orientation, false);
            SetScriptsEnabled(cameraPos, false);
            SetScriptsEnabled(playerCam, false);

            exitUIPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CancelExit()
    {
        if (isTransitioning) return;

        exitUIPanel.SetActive(false);
        SetScriptsEnabled(fatherObject, true);
        SetScriptsEnabled(orientation, true);
        SetScriptsEnabled(cameraPos, true);
        SetScriptsEnabled(playerCam, true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SetScriptsEnabled(GameObject obj, bool state)
    {
        MonoBehaviour script = obj.GetComponent<MonoBehaviour>();
        if (script != null) script.enabled = state;
    }

    public void ConfirmExit()
    {
        if (!isTransitioning)
        {
            StartCoroutine(ExitSequence());
        }
    }

    IEnumerator ExitSequence()
    {
        isTransitioning = true;
        exitUIPanel.SetActive(false);

        float timer = 0;
        fadeOverlay.gameObject.SetActive(true);
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeOverlay.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }

        doorCloseSound.Play();
        yield return new WaitForSeconds(doorCloseSound.clip.length);

        SceneManager.LoadScene(carSceneName);
    }

    public Transform GetTransform() => transform;
}