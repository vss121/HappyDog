using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
public class dog1 : MonoBehaviour
{
    public MainSceneDB MainSceneDB;
    public Slider slide1; // 행복도
    public Slider slide2; // 더러움
    public Slider slide3; //산책
    public Slider slide4; //허기
    public GameObject First; // 기본 사진(행복도 25이상)
    public GameObject Second; // 슬픈사진
    public GameObject n1;
    public GameObject n2;
    public GameObject n4;

    private void Awake()
    {
        MainSceneDB = GameObject.Find("Database").GetComponent<MainSceneDB>();
        MainSceneDB.DBMainSceneInitialize();
        First.SetActive(false);
        Second.SetActive(false);
        n1.SetActive(false);
        n2.SetActive(false);
        n4.SetActive(false);
        if (slide1.value < 25) //비율 25미만
        {
            First.SetActive(false);
            Second.SetActive(true);
            n1.SetActive(true);
        }
        else if (slide2.value < 25)
        {
            First.SetActive(false);
            Second.SetActive(true);
            n2.SetActive(true);
        }
        else if (slide4.value < 25)
        {
            First.SetActive(false);
            Second.SetActive(true);
            n4.SetActive(true);
        }
        else 
        {
            First.SetActive(true);
            Second.SetActive(false);
        }
    }
}