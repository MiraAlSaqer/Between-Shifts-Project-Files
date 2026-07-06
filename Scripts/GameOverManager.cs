using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public CanvasGroup blackScreen;
    public TextMeshProUGUI endDialogueText;
    public GameObject restartButton;
    public float typeSpeed = 0.05f;

    [TextArea(5, 10)]
    public string burnoutStory = "Exhaustion finally took its toll...";

    public void StartGameOver()
    {
        gameObject.SetActive(true);
        StartCoroutine(EndingSequence());
    }

    IEnumerator EndingSequence()
    {
        endDialogueText.text = "";
        blackScreen.alpha = 0;
        restartButton.SetActive(false);

        float t = 0;
        float fadeDuration = 1.5f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            blackScreen.alpha = t / fadeDuration;
            yield return null;
        }
        blackScreen.alpha = 1;

        yield return new WaitForSecondsRealtime(1f);

        foreach (char letter in burnoutStory.ToCharArray())
        {
            endDialogueText.text += letter;
            float currentWait = typeSpeed;
            if (letter == '.' || letter == '?' || letter == '!') currentWait = typeSpeed * 12f;
            if (letter == ',') currentWait = typeSpeed * 6f;
            yield return new WaitForSecondsRealtime(currentWait);
        }

        yield return new WaitForSecondsRealtime(1.5f);
        restartButton.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartGame()
    {
        StartCoroutine(FadeToMenu());
    }

    IEnumerator FadeToMenu()
    {
        restartButton.SetActive(false);

        float t = 1;
        float fadeOutDuration = 4.0f;

        while (t > 0)
        {
            t -= Time.unscaledDeltaTime;
            endDialogueText.alpha = t;
            yield return null;
        }

        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}