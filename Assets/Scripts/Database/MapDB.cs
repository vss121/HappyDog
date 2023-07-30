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

public class MapDB : MonoBehaviour
{
    string DBName = "test1.db";

    //************** storage Table **************
    int data_userNum = 1;

    //************** dog Table **************
    int data_cleanliness;
    int data_hunger;
    int data_money;
    int data_exp;
    int data_walkDistance;
    int data_collection;
    

    //************** walk Table **************
    int data_distance;
    string data_time;
    string data_date;


    //************** UI **************
    public TextMeshProUGUI quoteText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI distanceTxt;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI panelText;

    //************** temp data **************
    int walkMoney;
    int walkExp;
    bool hasColl;
    string quote;

    int aquiredTrophys;
    MenuUIManager gm;

    string[] quotesArray = new string[] {"You are stronger than you believe, braver than you know.", "In the midst of darkness, remember that stars can't shine without it.", "When life knocks you down, it's a chance to see things from a new perspective.", "Embrace the journey, for it is the path to self-discovery and growth.", "Your worth is not defined by your achievements but by the kindness you show.", "The greatest strength lies in the ability to rise after every fall.", "When the world feels heavy, find solace in the beauty of nature.", "Every storm eventually passes, leaving behind a clearer sky.", "Rainbows appear after the darkest rains; hope is never truly lost.", "Take one step at a time, and even the longest journey becomes achievable.", "In the stillness of the present moment, you'll find tranquility.", "Your uniqueness is your superpower; embrace it.", "Let go of what you can't control and focus on what you can change.", "Sometimes, the best thing you can do is give yourself permission to rest.", "Your scars tell a story of resilience and survival; wear them proudly.", "Be the reason someone believes in the goodness of people.", "Kindness costs nothing but enriches everything it touches.", "Be the light that brightens someone's day; it costs nothing but can mean everything.", "The smallest acts of love can have the most significant impact on others.", "The world needs your gifts; don't shy away from sharing them."};


    private void Start()
    {
        //DBConnectionCheck();
        data_userNum = 1;
        DBMapSceneInitialize();
        //getQuote();
        
        walkMoney=0;
        walkExp=0;
        hasColl=false;

        gm =  GameObject.Find("MiddlePanel").GetComponent<MenuUIManager>();
        aquiredTrophys=gm.aquiredTrophy;
        Debug.Log("aquiredTrophys   "+aquiredTrophys);

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
    public void DBMapSceneInitialize(){
        // get ; 청결 배고픔 돈 exp coll 

        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand=dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT * FROM dog WHERE userNum={data_userNum}";
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            data_cleanliness=dataReader.GetInt32(4);    // cleanliness
            data_hunger=dataReader.GetInt32(6);    // hunger
            data_money=dataReader.GetInt32(8);    // money
            data_exp=dataReader.GetInt32(9);    // exp
            data_collection=dataReader.GetInt32(11);    // collection
            Debug.Log("data_collection"+data_collection); 
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;


        // Modify text
        moneyText.text=$"{walkMoney}";
        expText.text=$"{walkExp}";

    }

    public void DBMapSceneEscape(){

        // 거리 update
        data_distance=Convert.ToInt32(distanceTxt.text);
        // 시간 update
        data_time='"'+timeText.text+'"';
        // 날짜 update
        data_date='"'+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+'"';
        // 배고픔 update : data_hunger-30
        if (data_hunger>30) data_hunger-=30; else data_hunger=0;
        // 청결 update : data_cleanliness-30
        if (data_cleanliness>30) data_cleanliness-=30; else data_cleanliness=0;
        // 돈 update : data_money+walkMoney
        // exp update : data_exp+walkExp
        // coll update : data_collection+1
        if(hasColl) data_collection+=1;
        // walk distance 나중에 수정
        Debug.Log($"INSERT INTO walk VALUES ({data_userNum}, {data_distance}, {data_time}, {data_date} )");
        DBInsert($"INSERT INTO walk VALUES ({data_userNum}, {data_distance}, {data_time}, {data_date} )");
        Debug.Log($"UPDATE dog SET hunger={data_hunger}, cleanliness={data_cleanliness}, money={data_money+walkMoney}, exp={data_exp+walkExp}, collection={data_collection} WHERE userNum={data_userNum}");
        DBInsert($"UPDATE dog SET hunger={data_hunger}, cleanliness={data_cleanliness}, money={data_money+walkMoney}, exp={data_exp+walkExp}, collection={data_collection} WHERE userNum={data_userNum}");

    }

    // **************************************************************************************
    public void getQuote()
    {
        if (data_collection<20){    // 20개
            quote=quotesArray[data_collection];
            // text 변경
            quoteText.text=quote;
            panelText.text='"'+quote+'"';
            hasColl=true;
        } else {
            Debug.Log("collection 개수 관련 error");
        }
    }

    public void getMoney()
    {
        walkMoney+=300; // 한번에 300씩 획득
        moneyText.text=walkMoney.ToString();
        panelText.fontSize=73;
        panelText.text="Aquired +300G!";
    }
    public void getExp()
    {
        walkExp+=15;    // 한번에 경험치 15씩 획득
        expText.text=walkExp.ToString();
        panelText.fontSize=73;
        panelText.text="Aquired +15EXP!";
    }




}
