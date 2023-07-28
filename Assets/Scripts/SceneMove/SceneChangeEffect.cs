using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeEffect : MonoBehaviour
{
    public GameObject ImgObject;
    Image image;
    float time;
    public string nextScene;
    void Start()
    {
        ImgObject = transform.GetChild(0).gameObject;
        image = ImgObject.GetComponent<Image>();
    }
    public void StartFade()
    {
        StartCoroutine(Fading());
    }
    public IEnumerator Fading()
    {
        image.gameObject.SetActive(true);
        time = 0f;
        Color color = image.color;
        while (color.a < 1.0f)  
        {
            time += Time.deltaTime;
            color.a += time;
            image.color = color;
            yield return null;
        }
        if (nextScene != null)
        {
            SceneManager.LoadScene(nextScene);
        }
        time = 0f;
        while (color.a > 1.0f)
        {
            time += Time.deltaTime;
            color.a -= time;
            image.color = color;
            yield return null;
        }
        image.gameObject.SetActive(false);
        yield return null;
    }
}