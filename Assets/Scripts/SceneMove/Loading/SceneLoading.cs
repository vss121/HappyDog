using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class SceneLoading : MonoBehaviour
{

    public TMP_Text PercentText;
    public static string nextScene;
    double Rounds_02;
    public Slider slide;
    [SerializeField]
    public void Start()
    {
        StartCoroutine(LoadScene());
    }
    public static void NextSceneName(string NextScene)
    {
        nextScene = NextScene;
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0f; // 로딩 시간 
        while (!op.isDone) // 로딩중
        {
            yield return null; // 씬을 넘겨주지 않음
            if (op.progress < 0.9f)
            {
                slide.value = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                slide.value = Mathf.Lerp(0f, 1f, timer * 0.3f);
                Rounds_02 = Math.Truncate((slide.value * 100));
                PercentText.text = Rounds_02 + "%";

                if (slide.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}