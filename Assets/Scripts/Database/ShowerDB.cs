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

public class ShowerDB : MonoBehaviour
{
    string DBName = "test1.db";

    //************** storage Table **************
    int data_userNum = 1;
    int data_shampoo1;
    int data_shampoo2;
    int data_shampoo3;


    //************** UI **************
    public TextMeshProUGUI shampoo1Txt;
    public TextMeshProUGUI shampoo2Txt;
    public TextMeshProUGUI shampoo3Txt;


    private void Start()
    {
        //DBConnectionCheck();
        DBShowerSceneInitialize();
        data_userNum = 1;
        // Initialize

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




    // **************************************************************************************
    public void DBShowerSceneInitialize(){
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand=dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT * FROM storage WHERE userNum={data_userNum}";
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            data_shampoo1=dataReader.GetInt32(8);
            data_shampoo2=dataReader.GetInt32(9);
            data_shampoo3=dataReader.GetInt32(10);
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;

        // Modify text
        shampoo1Txt.text=$"x{data_shampoo1}";
        shampoo2Txt.text=$"x{data_shampoo2}";
        shampoo3Txt.text=$"x{data_shampoo3}";
    }
}
