using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void GotoChat()
    {
        SceneManager.LoadScene("ChatScene");
    }
    public void GotoCollection()
    {
        SceneManager.LoadScene("CollectionScene");
    }
    public void GotoMain()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void GotoDepression()
    {
        SceneManager.LoadScene("DepressionScene");
    }
    public void GotoStore()
    {
        SceneManager.LoadScene("StoreScene");
    }
    public void GotoShower()
    {
        SceneManager.LoadScene("ShowerScene");
    }
    public void GotoFeed()
    {
        SceneManager.LoadScene("FeedScene");
    }
}
