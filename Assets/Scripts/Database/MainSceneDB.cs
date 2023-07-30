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

public class MainSceneDB : MonoBehaviour
{

    //************** Main Scene **************
    //top
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinText;
    // bottom
    public Slider likabilityBar;
    public Slider cleanlinessBar;
    public Slider depressionBar;
    public Slider hungerBar;


    //************** db **************
    string DBName = "test1.db";
    int userNum_one = 1;   // userNum is 1


    //************** table data **************
    // dog table
    int data_userNum;
    string data_dogName;
    int data_breed;
    int data_likability;
    int data_cleanliness;
    int data_depression;
    int data_hunger;
    int data_exp;
    int data_money;
    int data_level;
    int data_walkDistance;




    private void Start()
    {
        //DBConnectionCheck();
        DBMainSceneInitialize();
        
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
    public void DBRead(string query)
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
    public void DBMainSceneInitialize()
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand=dbConnection.CreateCommand();

        dbCommand.CommandText = "select * from dog where userNum="+userNum_one;
        IDataReader dataReader = dbCommand.ExecuteReader();
    
        while (dataReader.Read())
        {
            //Debug.Log($"{dataReader.GetInt32(0)}");

            ///////// dog table/////////
            //data_userNum;
            //data_dogName
            //data_breed=dataReader.GetInt32(2);

            // top
            data_level=dataReader.GetInt32(7);
            levelText.text=Convert.ToString(data_level);
            data_money=dataReader.GetInt32(8);
            coinText.text=Convert.ToString(data_money);

            // bottom
            data_likability=dataReader.GetInt32(3);
            likabilityBar.value=data_likability;
            data_cleanliness=dataReader.GetInt32(4);
            cleanlinessBar.value=data_cleanliness;
            data_depression=dataReader.GetInt32(5);
            depressionBar.value=data_depression;
            data_hunger=dataReader.GetInt32(6);
            hungerBar.value=data_hunger;
            
            // 상시 update와 scene 들어올때마다 update 구분?

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
    public void SettingLike()
    {
        data_likability = Convert.ToInt32(likabilityBar.value);
        DBInsert($"UPDATE dog SET likability={data_likability}");
    }
    // Update is called once per frame
    void Update()
    {
        // 계속 update?
    }
}
