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
    //************** dog Table **************
    int data_cleanliness;


    //************** UI **************
    public TextMeshProUGUI shampoo1Txt;
    public TextMeshProUGUI shampoo2Txt;
    public TextMeshProUGUI shampoo3Txt;
    public Slider cleanlinessBar;


    private void Start()
    {
        //DBConnectionCheck();
        DBShowerSceneInitialize();
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
    public void DBShowerSceneInitialize(){

        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand=dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT * FROM storage WHERE userNum={data_userNum}";
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            //shampoo
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

        dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        dbCommand=dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT * FROM dog WHERE userNum={data_userNum}";
        dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            //cleanliness
            cleanlinessBar.value=dataReader.GetInt32(4);
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

    public void DBShowerSceneEscape(){
        data_cleanliness=Convert.ToInt32(cleanlinessBar.value);
        DBInsert($"UPDATE dog SET cleanliness={data_cleanliness}");
        DBInsert($"UPDATE storage SET shampoo1={data_shampoo1}, shampoo2={data_shampoo2}, shampoo3={data_shampoo3} where userNum={data_userNum}");
    }

    // **************************************************************************************
    public void shampoo1Clicked()
    {
        if(cleanlinessBar.value<100 && data_shampoo1>0) {
        data_shampoo1-=1;
        shampoo1Txt.text=$"x{data_shampoo1}";
        }
    }

    public void shampoo2Clicked()
    {
        if(cleanlinessBar.value<100 && data_shampoo2>0) {
        data_shampoo2-=1;
        shampoo2Txt.text=$"x{data_shampoo2}";
        }
    }

    public void shampoo3Clicked()
    {
        if(cleanlinessBar.value<100 && data_shampoo3>0) {
        data_shampoo3-=1;
        shampoo3Txt.text=$"x{data_shampoo3}";
        }
    }

}
