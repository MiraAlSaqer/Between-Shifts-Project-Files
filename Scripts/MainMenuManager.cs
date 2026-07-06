using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    public void PressStart()
    {
        StartCoroutine(FadeAndLoad());
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game Pressed");
        Application.Quit();
    }

    IEnumerator FadeAndLoad()
    {
        fadeImage.gameObject.SetActive(true);

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene("House Scene");
    }
}