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

public class ShopDB : MonoBehaviour
{
    string DBName = "test1.db";

    //************** storage Table **************
    public int data_userNum = 1;
    int data_clothes1;
    int data_clothes2;
    int data_clothes3;
    int data_clothes4;
    int data_feed1;
    int data_feed2;
    int data_feed3;
    int data_feed4;
    int data_shampoo1;
    int data_shampoo2;
    int data_shampoo3;
    //************** dog Table **************
    int data_money;
    int data_clothes1On;
    int data_clothes2On;
    int data_clothes3On;
    int data_clothes4On;


    //************** UI **************
    public Text clothes1Txt;
    public Text clothes2Txt;
    public Text clothes3Txt;
    public Text clothes4Txt;
    public Text feed1Txt;
    public Text feed2Txt;
    public Text feed3Txt;
    public Text feed4Txt;
    public Text shampoo1Txt;
    public Text shampoo2Txt;
    public Text shampoo3Txt;
    public TextMeshProUGUI moneyTxt;
    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;
    public Toggle toggle4;

    public AudioSource Clickaudio;

    private void Start()
    {
        //DBConnectionCheck();
        data_userNum = 1;
        DBStoreInitialize();
        //DBStoreUpdate();
        Clickaudio = GameObject.Find("Audio").GetComponent<AudioSource>();

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
        IDbCommand dbCommand = dbConnection.CreateCommand();

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
    public void DBStoreInitialize()
    {

        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT * FROM storage WHERE userNum={data_userNum}";
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            data_clothes1 = dataReader.GetInt32(1);
            data_clothes2 = dataReader.GetInt32(2);
            data_clothes3 = dataReader.GetInt32(3);
            data_clothes4 = dataReader.GetInt32(4);
            data_feed1 = dataReader.GetInt32(5);
            data_feed2 = dataReader.GetInt32(6);
            data_feed3 = dataReader.GetInt32(7);
            data_feed4 = dataReader.GetInt32(8);
            data_shampoo1 = dataReader.GetInt32(9);
            data_shampoo2 = dataReader.GetInt32(10);
            data_shampoo3 = dataReader.GetInt32(11);


        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;

        dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT * FROM dog WHERE userNum={data_userNum}";
        dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            //money
            data_money = dataReader.GetInt32(8);
            data_clothes1On = dataReader.GetInt32(12);
            data_clothes2On = dataReader.GetInt32(13);
            data_clothes3On = dataReader.GetInt32(14);
            data_clothes4On = dataReader.GetInt32(15);

        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;

        // Modify text
        clothes1Txt.text = $"x{data_clothes1}";
        clothes2Txt.text = $"x{data_clothes2}";
        clothes3Txt.text = $"x{data_clothes3}";
        clothes4Txt.text = $"x{data_clothes4}";
        feed1Txt.text = $"x{data_feed1}";
        feed2Txt.text = $"x{data_feed2}";
        feed3Txt.text = $"x{data_feed3}";
        feed4Txt.text = $"x{data_feed4}";
        shampoo1Txt.text = $"x{data_shampoo1}";
        shampoo2Txt.text = $"x{data_shampoo2}";
        shampoo3Txt.text = $"x{data_shampoo3}";
        moneyTxt.text = $"{data_money}G";
        if (data_clothes1On == 1) toggle1.isOn = true; else toggle1.isOn = false;
        if (data_clothes2On == 1) toggle2.isOn = true; else toggle2.isOn = false;
        if (data_clothes3On == 1) toggle3.isOn = true; else toggle3.isOn = false;
        if (data_clothes4On == 1) toggle4.isOn = true; else toggle4.isOn = false;


    }

    public void DBStoreUpdate()
    {
        DBInsert($"UPDATE dog SET clothes1On={data_clothes1On}, clothes2On={data_clothes2On}, clothes3On={data_clothes3On}, clothes4On={data_clothes4On}, money={data_money} WHERE userNum={data_userNum}");
        DBInsert($"UPDATE storage SET clothes1={data_clothes1}, clothes2={data_clothes2}, clothes3={data_clothes3}, clothes4={data_clothes4}, feed1={data_feed1}, feed2={data_feed2}, feed3={data_feed3}, feed4={data_feed4}, shampoo1={data_shampoo1}, shampoo2={data_shampoo2}, shampoo3={data_shampoo3} WHERE userNum={data_userNum}");
        Debug.Log($"UPDATE storage SET clothes1={data_clothes1}, clothes2={data_clothes2}, clothes3={data_clothes3}, clothes4={data_clothes4}, feed1={data_feed1}, feed2={data_feed2}, feed3={data_feed3}, feed4={data_feed4}, shampoo1={data_shampoo1}, shampoo2={data_shampoo2}, shampoo3={data_shampoo3} WHERE userNum={data_userNum}");
    }

    public void DBShowerSceneEscape()
    {
        //data_cleanliness=Convert.ToInt32(cleanlinessBar.value);
        //DBInsert($"UPDATE dog SET cleanliness={data_cleanliness}");
        //DBInsert($"UPDATE storage SET shampoo1={data_shampoo1}, shampoo2={data_shampoo2}, shampoo3={data_shampoo3} where userNum={data_userNum}");
    }

    // **************************************************************************************

    public void feed1Clicked()
    {
        int price = 75;   //
        if (data_money >= price)
        {
            data_feed1 += 1;   // 수정
            feed1Txt.text = $"x{data_feed1}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void feed2Clicked()
    {
        int price = 100;   //
        if (data_money >= price)
        {
            data_feed2 += 1;   // 수정
            feed2Txt.text = $"x{data_feed2}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void feed3Clicked()
    {
        int price = 45;   //
        if (data_money >= price)
        {
            data_feed3 += 1;   // 수정
            feed3Txt.text = $"x{data_feed3}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void feed4Clicked()
    {
        int price = 80;   //
        if (data_money >= price)
        {
            data_feed4 += 1;   // 수정
            feed4Txt.text = $"x{data_feed4}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void shampoo1Clicked()
    {
        int price = 70;   //
        if (data_money >= price)
        {
            data_shampoo1 += 1;   // 수정
            shampoo1Txt.text = $"x{data_shampoo1}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void shampoo2Clicked()
    {
        int price = 90;   //
        if (data_money >= price)
        {
            data_shampoo2 += 1;   // 수정
            shampoo2Txt.text = $"x{data_shampoo2}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void shampoo3Clicked()
    {
        int price = 120;   //
        if (data_money >= price)
        {
            data_shampoo3 += 1;   // 수정
            shampoo3Txt.text = $"x{data_shampoo3}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void clothes1Clicked()
    {
        int price = 500;   //
        if (data_money >= price)
        {
            data_clothes1 += 1;   // 수정
            clothes1Txt.text = $"x{data_clothes1}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void clothes2Clicked()
    {
        int price = 700;   //
        if (data_money >= price)
        {
            data_clothes2 += 1;   // 수정
            clothes2Txt.text = $"x{data_clothes2}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void clothes3Clicked()
    {
        int price = 500;   //
        if (data_money >= price)
        {
            data_clothes3 += 1;   // 수정
            clothes3Txt.text = $"x{data_clothes3}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void clothes4Clicked()
    {
        int price = 600;   //
        if (data_money >= price)
        {
            data_clothes4 += 1;   // 수정
            clothes4Txt.text = $"x{data_clothes4}";   //
            data_money -= price;
            moneyTxt.text = $"{data_money}G";
            Clickaudio.Play();
            DBStoreUpdate();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void toggle1Clicked()
    {
        if (data_clothes1 <= 0) toggle1.isOn = false;
        else
        {
            if (toggle1.isOn == true) data_clothes1On = 1; else if (toggle1.isOn == false) data_clothes1On = 0;
            DBStoreUpdate();
        }
    }
    public void toggle2Clicked()
    {
        if (data_clothes2 <= 0) toggle2.isOn = false;
        else
        {
            if (toggle2.isOn == true) data_clothes2On = 1; else if (toggle2.isOn == false) data_clothes2On = 0;
            DBStoreUpdate();
        }
    }
    public void toggle3Clicked()
    {
        if (data_clothes3 <= 0) toggle3.isOn = false;
        else
        {
            if (toggle3.isOn == true) data_clothes3On = 1; else if (toggle3.isOn == false) data_clothes3On = 0;
            DBStoreUpdate();
        }
    }
    public void toggle4Clicked()
    {
        if (data_clothes4 <= 0) toggle4.isOn = false;
        else
        {
            if (toggle4.isOn == true) data_clothes4On = 1; else if (toggle4.isOn == false) data_clothes4On = 0;
            DBStoreUpdate();
        }
    }
}
