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
}
