using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System;
using System.Data;
using System.IO;
using UnityEngine.Networking;
using Mono.Data.Sqlite;

public class Setting2DB : MonoBehaviour
{
    string DBName = "test1.db";
    public string data_dogName;
    int userNum_one = 1;   // userNum is 1
    // Start is called before the first frame update
    void Start()
    {
        DBSecondSettingSceneInitialize();
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
    public void DBSecondSettingSceneInitialize()
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "select * from dog where userNum=" + userNum_one;
        IDataReader dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            //dog name
            data_dogName = dataReader.GetString(1);
            GameObject.Find("SettingManager").GetComponent<Rename>().InputNameText.text = Convert.ToString(data_dogName);
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
    public void DBSecondSettingSceneEscape()
    {
        DBInsert($"UPDATE dog SET dogName='{data_dogName}' where userNum={userNum_one}");
    }

    private void DBInsert(string query)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = query;
        dbCommand.ExecuteNonQuery();

        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;

    }
}