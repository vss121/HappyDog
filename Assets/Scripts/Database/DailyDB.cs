using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Mono.Data.Sqlite;
using TMPro;
using System.Data.Common;

public class DailyDB : MonoBehaviour
{
    string DBName = "test1.db";

    public int data_userNum = 1;
    // Daily Count Data
    int data_AttendCount;
    int data_ShowerCount;
    int data_FeedCount;
    int data_LikeCount;
    int data_WalkDistance;

    // Increase Daily Reward 
    int Reward1_Scale;
    int Reward2_Scale;
    int Reward3_Scale;
    int Reward4_Scale;
    int Reward5_Scale;


    int data_TotalExp;
    int data_Money;

    // visible Count Text
    public TMP_Text ConnectText;
    public TMP_Text ShowerCountText;
    public TMP_Text FeedCountText;
    public TMP_Text LickCountText;
    public TMP_Text WalkDistanceText;

    // Slider
    public Slider Daily1Slider;
    public Slider Daily2Slider;
    public Slider Daily3Slider;
    public Slider Daily4Slider;
    public Slider Daily5Slider;

    // Rewards GameObject
    public GameObject DailyReward1;
    public GameObject DailyReward2;
    public GameObject DailyReward3;
    public GameObject DailyReward4;
    public GameObject DailyReward5;
    Color RewardedColor;

    //panel
    public GameObject eventPanel1;
    public GameObject eventPanel2;
    public GameObject eventPanel3;
    public GameObject eventPanel4;
    public GameObject eventPanel5;
    bool isPanelOn;

    private void Awake()
    {
        ComparePlayerPrefs();
        SettingDefalut();
        DBDailySceneInitialize();
    }
    private void Start()
    {
        data_userNum = 1;
        RewardedColor = new Color(0, 255, 255);
        isPanelOn=false;
    }

    private void Update()
    {
        CehckingDaily();
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


    public void DBDailySceneInitialize()
    {

        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT * FROM dog WHERE userNum={data_userNum}";
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            // Count Data

            data_ShowerCount = dataReader.GetInt32(16);
            data_FeedCount = dataReader.GetInt32(17);
            data_LikeCount = dataReader.GetInt32(18);
            data_AttendCount = dataReader.GetInt32(19);
            data_WalkDistance = dataReader.GetInt32(10);

            // Slider Value
            Daily1Slider.value = data_AttendCount; // 출석
            Daily2Slider.value = data_FeedCount; // 먹이
            Daily3Slider.value = data_ShowerCount; // 샴푸
            Daily4Slider.value = data_LikeCount; // 호감도
            Daily5Slider.value = data_WalkDistance; // 산책거리

            // Rewards
            data_Money = dataReader.GetInt32(8);
            data_TotalExp = dataReader.GetInt32(9);
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;

        // Modify text
        ConnectText.text = $"{data_AttendCount}/{PlayerPrefs.GetInt("one_SliderValue")}";
        ShowerCountText.text = $"{data_ShowerCount}/{PlayerPrefs.GetInt("two_SliderValue")}";
        FeedCountText.text = $"{data_FeedCount}/{PlayerPrefs.GetInt("three_SliderValue")}";
        LickCountText.text = $"{data_LikeCount}/{PlayerPrefs.GetInt("four_SliderValue")}";
        WalkDistanceText.text = $"{data_WalkDistance}/{PlayerPrefs.GetInt("five_SliderValue")}";

    }

    public void CehckingDaily()
    {
        if (data_AttendCount >= Daily1Slider.maxValue)
        {
            DailyReward1.SetActive(true);
        }
        if (data_FeedCount >= Daily2Slider.maxValue)
        {
            DailyReward2.SetActive(true);
        }
        if (data_ShowerCount >= Daily3Slider.maxValue)
        {
            DailyReward3.SetActive(true);
        }
        if (data_LikeCount >= Daily4Slider.maxValue)
        {
            DailyReward4.SetActive(true);
        }
        if (data_WalkDistance >= Daily5Slider.maxValue)
        {
            DailyReward5.SetActive(true);
        }
    }


    /*
    int data_ShowerCount;
    int data_FeedCount;
    int data_LikeCount;
    int data_WalkDistance;
     */
    public void Daily1Clicked() // 경험치
    {
        Rewarded(DailyReward1);
        GiveReward(0, 30);
        Reward1_Scale += 1;
        Daily1Slider.maxValue = Reward1_Scale; // 최대치 증가
        ConnectText.text = $"{data_AttendCount}/{Daily1Slider.maxValue}";
        Daily1Slider.value = data_AttendCount;
        PlayerPrefs.SetInt("one_SliderValue", (int)Daily1Slider.maxValue);
        SettingDoneTextColor(ConnectText);
    }
    public void Daily2Clicked() // 돈
    {
        Rewarded(DailyReward2); // 선물 오브젝트 삭제
        GiveReward(500, 0); // 보상
        Reward2_Scale += 4; // Slider MaxValue 증가
        // Daily2Slider.maxValue = 4
        Daily2Slider.maxValue = Reward2_Scale; // 최대치 증가
        FeedCountText.text = $"{data_FeedCount}/{Daily2Slider.maxValue}";
        Daily2Slider.value = data_FeedCount;
        PlayerPrefs.SetInt("two_SliderValue", (int)Daily2Slider.maxValue);
        SettingDoneTextColor(FeedCountText);
    }

    public void Daily3Clicked() // 돈
    {
        Rewarded(DailyReward3);
        GiveReward(600, 0);
        Reward3_Scale += 4;
        Daily3Slider.maxValue = Reward3_Scale; // 최대치 증가
        ShowerCountText.text = $"{data_ShowerCount}/{Daily3Slider.maxValue}";
        Daily3Slider.value = data_ShowerCount;
        PlayerPrefs.SetInt("three_SliderValue", (int)Daily3Slider.maxValue);
        SettingDoneTextColor(ShowerCountText);
    }
    public void Daily4Clicked() // 돈 + 경험치
    {
        Rewarded(DailyReward4);
        GiveReward(250, 50);
        Reward4_Scale += 5;
        Daily4Slider.maxValue = Reward4_Scale; // 최대치 증가
        LickCountText.text = $"{data_LikeCount}/{Daily4Slider.maxValue}";
        Daily4Slider.value = data_LikeCount;
        PlayerPrefs.SetInt("four_SliderValue", (int)Daily4Slider.maxValue);
        SettingDoneTextColor(LickCountText);
    }
    public void Daily5Clicked() // 경험치 + 돈
    {
        Rewarded(DailyReward5);
        GiveReward(1500, 150);
        Reward5_Scale += 1000;
        Daily5Slider.maxValue = Reward5_Scale; // 최대치 증가
        WalkDistanceText.text = $"{data_WalkDistance}/{Daily5Slider.maxValue}";
        Daily5Slider.value = data_WalkDistance;
        PlayerPrefs.SetInt("five_SliderValue", (int)Daily5Slider.maxValue);
        SettingDoneTextColor(WalkDistanceText);
    }

    public void SettingDefalut()
    {
        DailyReward1.SetActive(false);
        DailyReward2.SetActive(false);
        DailyReward3.SetActive(false);
        DailyReward4.SetActive(false);
        DailyReward5.SetActive(false);

        Reward1_Scale = (int)Daily1Slider.maxValue;
        Reward2_Scale = (int)Daily2Slider.maxValue;
        Reward3_Scale = (int)Daily3Slider.maxValue;
        Reward4_Scale = (int)Daily4Slider.maxValue;
        Reward5_Scale = (int)Daily5Slider.maxValue;
        Daily1Slider.maxValue = Reward1_Scale;
        Daily2Slider.maxValue = Reward2_Scale;
        Daily3Slider.maxValue = Reward3_Scale;
        Daily4Slider.maxValue = Reward4_Scale;
        Daily5Slider.maxValue = Reward5_Scale;

    }
    public void Rewarded(GameObject RewardObj)
    {
        // 보상 안내 팝업창 띄우기
        RewardObj.SetActive(false);
    }

    public void SettingDoneTextColor(TMP_Text text)
    {
        text.color = RewardedColor;
    }
    public void GiveReward(int money = 0, int exp = 0)
    {
        data_Money += money;
        data_TotalExp += exp;
        DBInsert($"UPDATE dog SET money={data_Money}, exp={data_TotalExp}");
        Debug.Log(data_Money + ",     " + data_TotalExp);
    } 

    public void ComparePlayerPrefs()
    {
        if (PlayerPrefs.HasKey("one_SliderValue"))
        {
            Daily1Slider.maxValue = PlayerPrefs.GetInt("One_SliderValue");
        }
        else
        {
            PlayerPrefs.SetInt("one_SliderValue", (int)Daily1Slider.maxValue);
        }

        if (PlayerPrefs.HasKey("two_SliderValue"))
        {
            Daily2Slider.maxValue = PlayerPrefs.GetInt("two_SliderValue");
        }
        else
        {
            PlayerPrefs.SetInt("two_SliderValue", (int)Daily2Slider.maxValue);
        }

        if (PlayerPrefs.HasKey("three_SliderValue"))
        {
            Daily3Slider.maxValue = PlayerPrefs.GetInt("three_SliderValue");
        }
        else
        {
            PlayerPrefs.SetInt("three_SliderValue", (int)Daily3Slider.maxValue);
            Daily3Slider.maxValue = PlayerPrefs.GetInt("three_SliderValue");
        }

        if (PlayerPrefs.HasKey("four_SliderValue"))
        {
            Daily4Slider.maxValue = PlayerPrefs.GetInt("four_SliderValue");
        }
        else
        {
            PlayerPrefs.SetInt("four_SliderValue", (int)Daily4Slider.maxValue);
        }

        if (PlayerPrefs.HasKey("five_SliderValue"))
        {
            Daily5Slider.maxValue = PlayerPrefs.GetInt("five_SliderValue");
        }
        else
        {
            PlayerPrefs.SetInt("five_SliderValue", (int)Daily5Slider.maxValue);
        }
    }

    public void ShowPanel1()
    {
        if(!isPanelOn) {
            Daily1Clicked();
            eventPanel1.SetActive(true);
            isPanelOn=true;
        }
    }
        public void ShowPanel2()
    {
        if(!isPanelOn) {
            Daily2Clicked();
            eventPanel2.SetActive(true);
            isPanelOn=true;
        }
    }
        public void ShowPanel3()
    {
        if(!isPanelOn) {
            Daily3Clicked();
            eventPanel3.SetActive(true);
            isPanelOn=true;
        }
    }
        public void ShowPanel4()
    {
        if(!isPanelOn) {
            Daily4Clicked();
            eventPanel4.SetActive(true);
            isPanelOn=true;
        }
    }
        public void ShowPanel5()
    {
        if(!isPanelOn) {
            Daily5Clicked();
            eventPanel5.SetActive(true);
            isPanelOn=true;
        }
    }

    public void disablePanel()
    {
        eventPanel1.SetActive(false);
        eventPanel2.SetActive(false);
        eventPanel3.SetActive(false);
        eventPanel4.SetActive(false);
        eventPanel5.SetActive(false);
        isPanelOn=false;
    }
}