using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Data;
using System.IO;
using UnityEngine.Networking;
using Mono.Data.Sqlite;

public class UserDB : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinText;


    string DBName = "test1.db";
    int userNumU = 1;   // userNum is 1

    // dog table
    int data_userNum;
    string data_dogName;
    int data_breed;
    int data_money;
    int data_health;
    int data_cleanliness;
    int data_hunger;
    int data_exp;
    int data_level;
    int data_walkDistance;



    private void Awake()
    {
        StartCoroutine(DBCreate());
    }

    private void Start()
    {
        DBConnectionCheck();
        DBMainSceneInitialize();
        
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
    public void DBRead(string query)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand=dbConnection.CreateCommand();

        dbCommand.CommandText = query;
        IDataReader dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            Debug.Log($"{dataReader.GetInt32(0)}");
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }

    // 사용자정의
    public void DBMainSceneInitialize()
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand=dbConnection.CreateCommand();

        dbCommand.CommandText = "select * from dog where userNum=1";
        IDataReader dataReader = dbCommand.ExecuteReader();
    
        while (dataReader.Read())
        {
            //Debug.Log($"{dataReader.GetInt32(0)}");

            ///////// dog table/////////
            //data_userNum;
            //data_dogName
            //data_breed=dataReader.GetInt32(2);
            data_money=dataReader.GetInt32(3);
            coinText.text=Convert.ToString(data_money);
            // data_health=dataReader.GetInt32(4);
            // data_cleanliness=dataReader.GetInt32(5);
            // data_hunger=dataReader.GetInt32(6);
            // data_exp=dataReader.GetInt32(7);
            data_level=dataReader.GetInt32(8);
            levelText.text=Convert.ToString(data_level);
            // data_walkDistance=dataReader.GetInt32(9);

        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
        //levelText.text = 
        //DBInsert("Insert into list(name, age) values('" + NameText.text + "','"+AgeText.text+"')");
        //Update test Set NICKNAME = "멍청한토끼" Where ID = "user1"


        // Update UI text

        

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
