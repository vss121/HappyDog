using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailySetting : MonoBehaviour
{
    public Slider FirstConnect;

    public bool is1Rewarded;
    public bool is2Rewarded;
    public bool is3Rewarded;
    public bool is4Rewarded;
    public bool is5Rewarded;
    public GameObject DailyReward1;
    public GameObject DailyReward2;
    public GameObject DailyReward3;
    public GameObject DailyReward4;
    public GameObject DailyReward5;

    private void Start()
    {
        SettingBool();
    }
    public void Reward1Clicked()
    {

    }
    public void Reward2Clicked()
    {

    }
    public void Reward3Clicked()
    {

    }
    public void Reward4Clicked()
    {

    }
    public void Reward5Clicked()
    {

    }
    public void SettingBool()
    {
        is1Rewarded = false;
        is2Rewarded = false;
        is3Rewarded = false;
        is4Rewarded = false;
        is5Rewarded = false;
    }
}