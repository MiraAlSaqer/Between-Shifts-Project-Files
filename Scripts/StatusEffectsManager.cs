using System.Collections;
using UnityEngine;

public class StatusEffectsManager : MonoBehaviour
{
    public GameObject tiredEffect;
    public float sceneDuration = 60f;
    public GameOverManager gameOverScript;

    void Start()
    {
        StartTiredEffect(sceneDuration);
    }

    public void StartTiredEffect(float duration)
    {
        tiredEffect.SetActive(true);

        CircularProgressBar progressBar = tiredEffect.GetComponentInChildren<CircularProgressBar>();
        progressBar.ActivateCountdown(duration);
    }

    public void OnTiredTimerFinished()
    {
        StartCoroutine(ExecuteGameOverSequence());
    }

    IEnumerator ExecuteGameOverSequence()
    {
        CanvasGroup group = tiredEffect.GetComponent<CanvasGroup>();
        if (group != null)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                group.alpha = 1 - t;
                yield return null;
            }
        }

        gameOverScript.StartGameOver();
    }
}