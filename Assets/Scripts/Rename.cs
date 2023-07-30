using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rename : MonoBehaviour
{
    public TMP_InputField InputNameText;
    
    private void Start()
    {
        InputNameText.text = GameObject.Find("Database").GetComponent<Setting2DB>().data_dogName;
        Debug.Log(GameObject.Find("Database").GetComponent<Setting2DB>().data_dogName);
    }
    public void OKBtnClick()
    {
        string userinputName = InputNameText.text;
        GameObject.Find("Database").GetComponent<Setting2DB>().data_dogName = userinputName;
        GameObject.Find("Database").GetComponent<Setting2DB>().DBSecondSettingSceneEscape();
    }
}