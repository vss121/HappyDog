using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class testShower : MonoBehaviour
{
    public Slider slide;
    public GameObject First; // 기본 사진(청결도 50이상)
    public GameObject Second; // 샤워중인 사진(거품있음)
    public GameObject Third; // 뽀송뽀송 사진(if 청결도 50이상 && 샤워완료)=> 샤워후 
    public GameObject Fourth; // 지저분한 사진(청결도 50미만)
    public GameObject Animobj;
    public GameObject Waterobj;
    public Button WaterBtn;
    public int score, count;
    private void Start()
    {
        WaterBtn = Waterobj.GetComponent<Button>();
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
        count = 0;
        Animobj = GameObject.Find("BackGroundCanvus").transform.Find("Shower").gameObject;
        Animobj.SetActive(false);
    }
    public void One_Shampoo() // 버튼 클릭 이벤트
    {
        count++;
        SettingImg();
        score = 10;
        slide.value += score;
        // DB재료 - 1
    }
    public void Two_Shampoo()
    {
        count++;
        SettingImg();
        score = 20;
        slide.value += score;
        // DB재료 - 1
    }
    public void Three_Shampoo()
    {
        count++;
        SettingImg();
        score = 30;
        slide.value += score;
        // DB재료 - 1
    }
    public void Watering()
    {
        StartCoroutine(mulbangowl(3.0f));
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
        WaterBtn.enabled = false;
        Animobj.SetActive(true);
        while (coolTime > 1.0f)
        {
            coolTime -= Time.deltaTime;
            Debug.Log(coolTime);
            yield return new WaitForFixedUpdate();
        }
        CheckValue();
        Animobj.SetActive(false);
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
        WaterBtn.enabled = true;
    }
}