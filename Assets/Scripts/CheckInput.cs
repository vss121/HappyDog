using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckInput : MonoBehaviour
{
    public TMP_InputField InputNameText;
    public TMP_InputField InputAgeText;
    public MoveScene scene;
    private void Start()
    {
        GameObject.Find("PopUpCanvus").transform.Find("Panel").gameObject.SetActive(false);
    }
    public void DoneClicked()
    {
        string userinputName = InputNameText.text;
        string userinputAge = InputAgeText.text;
        if (string.IsNullOrEmpty(userinputName) || string.IsNullOrWhiteSpace(userinputName) ||
            string.IsNullOrEmpty(userinputAge) || string.IsNullOrWhiteSpace(userinputAge)) 
        {
            GameObject.Find("PopUpCanvus").transform.Find("Panel").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("Database").GetComponent<SettingDB>().data_dogName = userinputName;
            GameObject.Find("Database").GetComponent<SettingDB>().DBFirstSettingSceneEscape();
            scene.GotoFirstDepress();
        }
    }

    public void BackBtn()
    {
        GameObject.Find("PopUpCanvus").transform.Find("Panel").gameObject.SetActive(false);
    }
}