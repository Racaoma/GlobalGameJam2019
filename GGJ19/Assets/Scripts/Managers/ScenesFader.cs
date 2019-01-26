using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesFader : SingletonAwakePersistent<ScenesFader>
{
    public CanvasGroup canvasGroup;
    float timer;
    AsyncOperation async;
    string currentScene;
    bool transitioning = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ChangeScene(string newScene)
    {
        StartCoroutine(ChangeRoutine(newScene));
    }

    private IEnumerator ChangeRoutine(string newScene)
    {
        transitioning = true;
        currentScene = SceneManager.GetActiveScene().name;

        yield return StartCoroutine(FadeIn(1f));

        async = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            yield return null;
        }

        async = SceneManager.UnloadSceneAsync(currentScene);
        
        while (!async.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeOut(1f));

        transitioning = false;
    }

    private IEnumerator FadeIn(float duration)
    {
        timer = 0f;
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        while (timer < duration)
        {
            canvasGroup.alpha = timer / duration;
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
    private IEnumerator FadeOut(float duration)
    {
        timer = 0f;
        canvasGroup.alpha = 1f;
        while (timer < duration)
        {
            canvasGroup.alpha = 1f - timer / duration;
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }
}
