using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using System;
using System.Data;
using System.IO;
using UnityEngine.Networking;
using Mono.Data.Sqlite;

public class StartSceneDB : MonoBehaviour
{

    //**************DB**************
    string DBName = "test1.db";

    private void Awake()
    {
        StartCoroutine(DBCreate());
    }

    private void Start()
    {
        DBConnectionCheck();
    }

    IEnumerator DBCreate()
    {
        string filepath = string.Empty;
        if (Application.platform == RuntimePlatform.Android) // Android 
        {
            filepath = Application.persistentDataPath + "/"+ DBName; // Path for android
            if (!File.Exists(filepath))
            {
                UnityWebRequest unityWebRequest = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets/"+DBName);
                unityWebRequest.downloadedBytes.ToString();
                yield return unityWebRequest.SendWebRequest().isDone;
                File.WriteAllBytes(filepath, unityWebRequest.downloadHandler.data);
            }
        }
        else // pc
        {
            filepath = Application.dataPath + "/" + DBName; // assets 안
            if (!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + "/" + DBName, filepath);
            }
        }
        Debug.Log("DB Create: OK");


    }

    public string GetDBFilePath()
    {
        string str = string.Empty;
        if (Application.platform == RuntimePlatform.Android)
        {
            str = "URI=file:" + Application.persistentDataPath + "/" + DBName;

        }
        else
        {
            str = "URI=file:" + Application.dataPath + "/" + DBName;
        }
        return str;
    }

    public void DBConnectionCheck()
    {
        try
        {
            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
            dbConnection.Open();

            if (dbConnection.State == ConnectionState.Open)
            {
                Debug.Log("DB Conn: OK");
            }
            else
            {
                Debug.Log("DB Conn: Fail");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }


    public void btnClicked()
    {
        SceneManager.LoadScene("FirstSettingScene");
    }

}