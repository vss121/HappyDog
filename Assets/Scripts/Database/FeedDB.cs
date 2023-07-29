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

public class FeedDB : MonoBehaviour
{
    string DBName = "test1.db";

    //************** storage Table **************
    int data_userNum = 1;
    int data_feed1;
    int data_feed2;
    int data_feed3;
    int data_feed4;
    //************** dog Table **************
    int data_hunger;


    //************** UI **************
    public TextMeshProUGUI feed1Txt;
    public TextMeshProUGUI feed2Txt;
    public TextMeshProUGUI feed3Txt;
    public TextMeshProUGUI feed4Txt;
    public Slider hungerBar;


    private void Start()
    {
        //DBConnectionCheck();
        DBFeedSceneInitialize();
        data_userNum = 1;

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
    public void DBFeedSceneInitialize(){

        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand=dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT * FROM storage WHERE userNum={data_userNum}";
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            //shampoo
            data_feed1=dataReader.GetInt32(5);
            data_feed2=dataReader.GetInt32(6);
            data_feed3=dataReader.GetInt32(7);
            data_feed4=dataReader.GetInt32(8);
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;

        dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        dbCommand=dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT * FROM dog WHERE userNum={data_userNum}";
        dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            //hunger
            data_hunger=dataReader.GetInt32(6);
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;

        // Modify text
        feed1Txt.text=$"x{data_feed1}";
        feed2Txt.text=$"x{data_feed2}";
        feed3Txt.text=$"x{data_feed3}";
        feed4Txt.text=$"x{data_feed4}";
        hungerBar.value=data_hunger;

    }

    public void DBFeedSceneEscape(){
        data_hunger=Convert.ToInt32(hungerBar.value);
        DBInsert($"UPDATE dog SET hunger={data_hunger}");
        DBInsert($"UPDATE storage SET feed1={data_feed1}, feed2={data_feed2}, feed3={data_feed3}, feed4={data_feed4} where userNum={data_userNum}");
    }

    // **************************************************************************************
    public void feed1Clicked()
    {
        if(hungerBar.value<100 && data_feed1>0) {
        data_feed1-=1;
        feed1Txt.text=$"x{data_feed1}";
        //Debug.Log(data_feed1);
        }
    }

    public void feed2Clicked()
    {
        if(hungerBar.value<100 && data_feed2>0) {
        data_feed2-=1;
        feed2Txt.text=$"x{data_feed2}";
        //Debug.Log(data_feed2);
        }
    }

    public void feed3Clicked()
    {
        if(hungerBar.value<100 && data_feed3>0) {
        data_feed3-=1;
        feed3Txt.text=$"x{data_feed3}";
        }
    }

    public void feed4Clicked()
    {
        if(hungerBar.value<100 && data_feed4>0) {
        data_feed4-=1;
        feed4Txt.text=$"x{data_feed4}";
        }
    }


}
