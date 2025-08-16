using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance;

    private RawImage fadeImage;
    [SerializeField] float fadingTime = 0.5f;
    private bool loading = false;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            fadeImage = GetComponentInChildren<RawImage>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneAsync(string scene)
    {
        if (!loading)
        {
            StartCoroutine(LoadScene(scene));
        }
    }

    IEnumerator LoadScene(string scene)
    {
        loading = true;
        AsyncOperation load = SceneManager.LoadSceneAsync(scene);
        if (scene == "Bagarre")
        {
            SoundManager.PlayMusic(SoundType.BATTLE_MUSIC);
        }
        load.allowSceneActivation = false;
        yield return StartCoroutine(FadeScreen());
        while (load.progress! >= 0.9f && fadeImage.color.a != 1)
        {
            yield return null;
        }
        load.allowSceneActivation = true;
        yield return null;
        StartCoroutine(UnFadeScreen());
    }

    IEnumerator FadeScreen()
    {
        float timer = 0;

        while (fadeImage.color.a != 1)
        {
            timer += Time.deltaTime;

            Color color = fadeImage.color;
            color.a = Mathf.Clamp(timer / fadingTime, 0, 1);
            fadeImage.color = color;

            yield return null;
        }
    }

    IEnumerator UnFadeScreen()
    {
        float timer = fadingTime;

        while (fadeImage.color.a != 0)
        {
            timer -= Time.deltaTime;

            Color color = fadeImage.color;
            color.a = Mathf.Clamp(timer / fadingTime, 0, 1);
            fadeImage.color = color;

            yield return null;
        }
        loading = false;
    }
}
