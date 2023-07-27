using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public GameObject SceneObject;
    public SceneChangeEffect SceneChange;
    public void GotoChat()
    {
        SceneObject.GetComponent<SceneChangeEffect>().nextScene = "ChatScene";
        SceneChange.StartFade();
    }
    public void GotoCollection()
    {
        SceneObject.GetComponent<SceneChangeEffect>().nextScene = "CollectionScene";
        SceneChange.StartFade();
    }
    public void GotoMain()
    {
        SceneObject.GetComponent<SceneChangeEffect>().nextScene = "MainScene";
        SceneChange.StartFade();
    }
    public void GotoDepression()
    {
        SceneObject.GetComponent<SceneChangeEffect>().nextScene = "DepressionScene";
        SceneChange.StartFade();
    }
    public void GotoStore()
    {
        SceneObject.GetComponent<SceneChangeEffect>().nextScene = "StoreScene";
        SceneChange.StartFade();
    }
    public void GotoShower()
    {
        SceneObject.GetComponent<SceneChangeEffect>().nextScene = "ShowerScene";
        SceneChange.StartFade();
    }
    public void GotoFeed()
    {
        SceneObject.GetComponent<SceneChangeEffect>().nextScene = "FeedScene";
        SceneChange.StartFade();
    }
    public void GotoMove()
    {
        SceneObject.GetComponent<SceneChangeEffect>().nextScene = "FeedScene";
        SceneChange.StartFade();
    }
}
