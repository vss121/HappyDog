using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
public class testShower : MonoBehaviour
{
    public ShowerDB Showerdb;
    public Slider slide;
    public GameObject First; // 기본 사진(청결도 50이상)
    public GameObject Second; // 샤워중인 사진(거품있음)
    public GameObject Third; // 뽀송뽀송 사진(if 청결도 50이상 && 샤워완료)=> 샤워후 
    public GameObject Fourth; // 지저분한 사진(청결도 50미만)
    public GameObject Animobj;
    public int score, count;
    // 버튼 obj
    public GameObject FirstObj;
    public GameObject SecondObj;
    public GameObject ThirdObj;
    public GameObject FourthObj;
    // 이미지 투명도 조절을 위한 이미지 obj
    public Image Firstimg;
    public Image Secondimg;
    public Image Thirdimg;
    public Image Fourthimg;

    Button FirstBtn, SecondBtn, ThirdBtn, FourthBtn;

    //  이미지 투명도 조절을 위한 Color 객체
    Color FirstCo, SecondCo, ThirdCo, FourthCo;
    private void Awake()
    {
        // Script 가져오기
        Showerdb = GameObject.Find("Database").GetComponent<ShowerDB>();
        Showerdb.DBShowerSceneInitialize();
        if (slide.value >= 50)
        {
            First.SetActive(true);
            Second.SetActive(false);
            Third.SetActive(false);
            Fourth.SetActive(false);
        }
        else
        {
            First.SetActive(false);
            Second.SetActive(false);
            Third.SetActive(false);
            Fourth.SetActive(true);
        }
    }
    private void Start()
    {

        // 버튼obj 불러오기
        FirstBtn = FirstObj.GetComponent<Button>();
        SecondBtn = SecondObj.GetComponent<Button>();
        ThirdBtn = ThirdObj.GetComponent<Button>();
        FourthBtn = FourthObj.GetComponent<Button>();
        // Color 불러오기
        FirstCo = Firstimg.color;
        SecondCo = Secondimg.color;
        ThirdCo = Thirdimg.color;
        FourthCo = Fourthimg.color;
        count = 0;
        Animobj = GameObject.Find("BackGroundCanvus").transform.Find("Shower").gameObject;
        Animobj.SetActive(false);
    }
    public void One_Shampoo() // 버튼 클릭 이벤트
    {
        count++;
        SettingImg();
        StartCoroutine(Wait(3.0f));
        score = 10;
        slide.value += score;
        
        Showerdb.shampoo1Clicked();
    }
    public void Two_Shampoo()
    {
        count++;
        SettingImg();
        StartCoroutine(Wait(3.0f));
        score = 20;
        slide.value += score;
        // DB재료 - 1
        Showerdb.shampoo2Clicked();
    }
    public void Three_Shampoo()
    {
        count++;
        SettingImg();
        StartCoroutine(Wait(3.0f));
        score = 30;
        slide.value += score;
        // DB재료 - 1
        Showerdb.shampoo3Clicked();
    }
    public void Watering()
    {
        StartCoroutine(mulbangowl(4.0f));
    }
    public void SettingImg() // 샤워중
    {
        if (count >= 1)
        {
            First.SetActive(false);
            Second.SetActive(true);
            Third.SetActive(false);
            Fourth.SetActive(false);
        }
    }
    IEnumerator mulbangowl(float coolTime)
    {
        disableBtn();
        Animobj.SetActive(true);
        float filledTime = 0f;
        while(filledTime <= coolTime)
        {
            yield return new WaitForFixedUpdate();
            filledTime += Time.deltaTime;
            Firstimg.fillAmount = filledTime / coolTime;
            Secondimg.fillAmount = filledTime / coolTime;
            Thirdimg.fillAmount = filledTime / coolTime;
            Fourthimg.fillAmount= filledTime / coolTime;
        }
        CheckValue();
        Animobj.SetActive(false);
    }
    IEnumerator Wait(float coolTime){
        disableBtn();
        float filledTime = 0f;
        while (filledTime <= coolTime)
        {
            yield return new WaitForFixedUpdate();
            filledTime += Time.deltaTime;
            Firstimg.fillAmount = filledTime / coolTime;
            Secondimg.fillAmount = filledTime / coolTime;
            Thirdimg.fillAmount = filledTime / coolTime;
            Fourthimg.fillAmount = filledTime / coolTime;
        }
        ableBtn();
    }
    public void CheckValue()
    {
        if (slide.value < 50) // 샤워 후 청결도 50미만
        {
            First.SetActive(false);
            Second.SetActive(false);
            Third.SetActive(false);
            Fourth.SetActive(true);
        }
        else // 샤워 후 청결도 50이상
        {
            First.SetActive(false);
            Second.SetActive(false);
            Third.SetActive(true);
            Fourth.SetActive(false);
            count = 0;
        }
        ableBtn();
    }
    public void disableBtn() {
        SetInvisable();
        FirstBtn.enabled = false;
        SecondBtn.enabled = false;
        ThirdBtn.enabled = false;
        FourthBtn.enabled = false;
    }
    public void ableBtn(){
        Setvisable();
        FirstBtn.enabled = true;
        SecondBtn.enabled = true;
        ThirdBtn.enabled = true;
        FourthBtn.enabled = true;
    }

    public void SetInvisable()
    {
        // 비활성화 투명도 설정
        FirstCo.a = 0.5f;
        SecondCo.a = 0.5f;
        ThirdCo.a = 0.5f;
        FourthCo.a = 0.5f;
        Firstimg.color = FirstCo;
        Secondimg.color = SecondCo;
        Thirdimg.color = ThirdCo;
        Fourthimg.color = FourthCo;
    }
    public void Setvisable()
    {
        // 활성화 투명도 설정
        FirstCo.a = 1.0f;
        SecondCo.a = 1.0f;
        ThirdCo.a = 1.0f;
        FourthCo.a = 1.0f;
        Firstimg.color = FirstCo;
        Secondimg.color = SecondCo;
        Thirdimg.color = ThirdCo;
        Fourthimg.color = FourthCo;
    }

}