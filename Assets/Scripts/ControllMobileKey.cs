using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllMobileKey : MonoBehaviour
{
    void Update()
    {
        /*        if (Application.platform == RuntimePlatform.Android)
                {
                    if (Input.GetKey(KeyCode.Home)) // 홈 키
                    {

                    }
                    else if (Input.GetKey(KeyCode.Escape)) // 뒤로가기 키
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                    else if (Input.GetKey(KeyCode.Menu)) // 메뉴 키
                    {

                    }
                }*/
        if (Input.GetKey(KeyCode.Tab))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
