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
    public TextMeshProUGUI expText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI nameText;
    // bottom
    public Slider likabilityBar;
    public Slider cleanlinessBar;
    public Slider depressionBar;
    public Slider hungerBar;
    public Slider walkBar;
    // clothes
    public GameObject clothes1Object;
    public GameObject clothes2Object;
    public GameObject clothes3Object;
    public GameObject clothes4Object;






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
    int data_clothes1On;
    int data_clothes2On;
    int data_clothes3On;
    int data_clothes4On;
    int data_lastDistance; // 산책시작에 위치한 슬라이더바

    public int data_likeCount;
    int totalExpFromDb;


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
    public void DBInsert(string query)
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

            ///////// dog table/////////
            // top
            //data_level = dataReader.GetInt32(7);
            //levelText.text=Convert.ToString(data_level);
            data_money=dataReader.GetInt32(8);
            coinText.text=Convert.ToString(data_money);
            totalExpFromDb = dataReader.GetInt32(9);
            //expText.text=Convert.ToString(data_exp);

            //dog name
            data_dogName = dataReader.GetString(1);
            nameText.text = Convert.ToString(data_dogName);

            // bottom
            data_likability =dataReader.GetInt32(3);
            likabilityBar.value=data_likability;
            data_cleanliness=dataReader.GetInt32(4);
            cleanlinessBar.value=data_cleanliness;
            data_depression=dataReader.GetInt32(5);
            depressionBar.value=data_depression;
            data_hunger=dataReader.GetInt32(6);
            hungerBar.value=data_hunger;
            data_lastDistance = dataReader.GetInt32(20); //최근 걸었던거리
            walkBar.value=data_lastDistance;

            // clothes
            data_clothes1On =dataReader.GetInt32(12);
            data_clothes2On=dataReader.GetInt32(13);
            data_clothes3On=dataReader.GetInt32(14);
            data_clothes4On=dataReader.GetInt32(15);

            data_likeCount = dataReader.GetInt32(18);


        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;

        // clothes 초기화
        setClothes();

        // level, exp 초기화
        setLevelExp();

    }

    public void setClothes() 
    {
        if(data_clothes1On==1) clothes1Object.SetActive(true); else clothes1Object.SetActive(false);
        if(data_clothes2On==1) clothes2Object.SetActive(true); else clothes2Object.SetActive(false);
        if(data_clothes3On==1) clothes3Object.SetActive(true); else clothes3Object.SetActive(false);
        if(data_clothes4On==1) clothes4Object.SetActive(true); else clothes4Object.SetActive(false);
    }
    public void setLevelExp()
    {
        data_level=totalExpFromDb/100;
        data_exp=totalExpFromDb%100;
        levelText.text="LV"+Convert.ToString(data_level);
        expText.text=Convert.ToString(data_exp)+"%";
    }
    public void SettingLike()
    {
        data_likability = Convert.ToInt32(likabilityBar.value);
        DBInsert($"UPDATE dog SET likability={data_likability}");
    }
    // Update is called once per frame
    
    void Update()
    {
        if (Time.frameCount % 1000 == 0) {
            //Debug.Log("data_likability "+data_likability);
            data_likability-=2;
            data_cleanliness-=1;
            data_hunger-=1;
            likabilityBar.value=data_likability;
            cleanlinessBar.value=data_cleanliness;
            hungerBar.value=data_hunger;


            DBInsert($"UPDATE dog SET likability={data_likability}, cleanliness={data_cleanliness}, hunger={data_hunger} ");
        }
        
    }


}
