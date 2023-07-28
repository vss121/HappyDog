using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Data;
using System.IO;
using UnityEngine.Networking;
using Mono.Data.Sqlite;

public class DepressionTestDB : MonoBehaviour
{
    string DBName = "test1.db";

    //************** Depression Table **************
    int data_userNum = 1;
    string data_date;
    int data_score;




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
            filepath = Application.dataPath + "/" + DBName;
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

     //Insert To Database
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

    //Read DB
    private void DBRead(string query)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand=dbConnection.CreateCommand();

        dbCommand.CommandText = query;
        IDataReader dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            //Debug.Log($"{dataReader.GetInt32(0)}");
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }



    //************** User definition functions  **************
    public void InsertDepressionScore(int scoreResult)
    {
        data_userNum=1;
        data_date=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        data_score=scoreResult;
        //Debug.Log($"INSERT INTO depression VALUES ({data_userNum}, '{data_date}', {data_score})");
        DBInsert($"INSERT INTO depression VALUES ({data_userNum}, '{data_date}', {data_score})");
        DBInsert($"UPDATE dog SET depression='{data_score}' where userNum={data_userNum}");
    }
    
}
