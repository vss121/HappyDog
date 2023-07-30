using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testfeeder : MonoBehaviour
{
    public FeedDB FeedDb;
    public Slider slide;
    public GameObject First;
    public GameObject Second;
    public GameObject Third;
    public GameObject Fourth;
    /*public GameObject Animobj;*/
    public int score, count;
    /* 버튼 오브젝트들 */
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
        FeedDb = GameObject.Find("Database").GetComponent<FeedDB>();
        FeedDb.DBFeedSceneInitialize();
        if (slide.value < 60) //허기 비율 60미만이면
        {
            First.SetActive(true); //1번 배고픈모습만 ON
            Second.SetActive(false);
            Third.SetActive(false);
            Fourth.SetActive(false);
            count = 0;
        }
        else //허기 비율 60이상
        {
            First.SetActive(false);
            Second.SetActive(false);
            Third.SetActive(false);
            Fourth.SetActive(true);
            count = 0;
        }
    }
    private void Start()
    {
        FirstBtn = FirstObj.GetComponent<Button>();
        SecondBtn = SecondObj.GetComponent<Button>();
        ThirdBtn = ThirdObj.GetComponent<Button>();
        FourthBtn = FourthObj.GetComponent<Button>();
        FirstCo = Firstimg.color;
        SecondCo = Secondimg.color;
        ThirdCo = Thirdimg.color;
        FourthCo = Fourthimg.color;
    }

    public void One_Food() // 버튼 클릭 이벤트
    {
        count++;
        First.SetActive(false);
        Second.SetActive(true);
        Third.SetActive(false);
        Fourth.SetActive(false);
        score = 10;
        StartCoroutine(Wait(3.0f));
        slide.value += score;

        // DB재료 - 1
        FeedDb.feed1Clicked();
    }

    public void Two_Food()
    {
        count++;
        First.SetActive(false);
        Second.SetActive(true);
        Third.SetActive(false);
        Fourth.SetActive(false);
        score = 20;
        StartCoroutine(Wait(3.0f));
        slide.value += score;
        // DB재료 - 1
        FeedDb.feed2Clicked();
    }

    public void One_Snack()
    {
        count++;
        First.SetActive(false);
        Second.SetActive(true);
        Third.SetActive(false);
        Fourth.SetActive(false);
        score = 5;
        StartCoroutine(Wait(3.0f));
        slide.value += score;

        // DB재료 - 1
        FeedDb.feed3Clicked();

    }

    public void Two_Snack() // 버튼 클릭 이벤트
    {
        count++;
        First.SetActive(false);
        Second.SetActive(true);
        Third.SetActive(false);
        Fourth.SetActive(false);
        score = 10;
        StartCoroutine(Wait(3.0f));
        slide.value += score;
        // DB재료 - 1
        FeedDb.feed4Clicked();

    }
    IEnumerator Wait(float coolTime)
    {
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
        valuecheck();
    }

    public void valuecheck() // 현재 슬라이더값 체킹
    {
        if (slide.value < 60)
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
        ableBtn();
    }
    public void disableBtn()
    {
        SetInvisable();
        FirstBtn.enabled = false;
        SecondBtn.enabled = false;
        ThirdBtn.enabled = false;
        FourthBtn.enabled = false;
    }
    public void ableBtn()
    {
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

