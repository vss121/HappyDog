
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heartinstantiate : MonoBehaviour
{
    public GameObject prefab;
    public GameObject HeartObj;
    public GameObject PosObj;

    public Slider LikeSlide;
    public MainSceneDB MainDB;
    GameObject instantiateB;
    GameObject instantiateC;

    public float movespeed = 2.0f;
    public Image HeartImg;
    public int LikeVal;
    Color HeartCo;

    Button HeartBtn;

    public Vector3 Pos;
    public string CoolTimeKey = "CoolTime";
    public float CoolDuration = 60f;
    private void Start()
    {
        HeartCo = HeartImg.color;
        HeartBtn = HeartObj.GetComponent<Button>();
        Pos = PosObj.transform.position;
        MainDB = GameObject.Find("Database").GetComponent<MainSceneDB>();
        LikeVal = 10;
        if (PlayerPrefs.HasKey(CoolTimeKey))
        {
            float CoolEndTime = PlayerPrefs.GetFloat(CoolTimeKey);
            float CurrentTime = Time.time;
            if (CoolEndTime > CurrentTime)
            {
                float remainingTime = CoolEndTime - CurrentTime;
                if(remainingTime >= 60f)
                {
                    remainingTime = 60f;
                }
                StartCoroutine(StartCool(remainingTime));
            }
            else
            {
                PlayerPrefs.DeleteKey(CoolTimeKey);
            }
        }
    }
    public void LikeBtnClick()
    {
        instantiateB = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity, GameObject.Find("BackGroundCanvus").transform);
        instantiateC = Instantiate(prefab, new Vector3(Pos.x, Pos.y, Pos.z), Quaternion.identity, GameObject.Find("BackGroundCanvus").transform);
        LikeSlide.value += LikeVal;
        MainDB.SettingLike();
        StartCoroutine(Instance(0.3f));
        if (!IsCoolTimeRunning())
        {
            Debug.Log("asdasd");
            float CoolEndTime = Time.time + CoolDuration;
            PlayerPrefs.SetFloat(CoolTimeKey, CoolEndTime);
            StartCoroutine(StartCool(CoolDuration));
        }
    }
    public bool IsCoolTimeRunning()
    {
        return PlayerPrefs.HasKey(CoolTimeKey) && PlayerPrefs.GetFloat(CoolTimeKey) > Time.time;
    }
    IEnumerator Instance(float coolTime)
    {
        Vector3 newPos = instantiateB.transform.position;
        Vector3 newPos2 = instantiateC.transform.position;
        float filledTime = 0f;
        HeartCo.a = 0.5f;
        HeartImg.color = HeartCo;
        HeartBtn.enabled = false;
        while (filledTime <= coolTime)
        {
            yield return new WaitForFixedUpdate();
            newPos.y += movespeed * Time.deltaTime;
            instantiateB.transform.position = newPos;
            newPos2.y += movespeed * Time.deltaTime;
            instantiateC.transform.position = newPos2;
            filledTime += Time.deltaTime;
            //HeartImg.fillAmount = filledTime / coolTime;
        }
        HeartCo.a = 1.0f;
        HeartImg.color = HeartCo;
        HeartBtn.enabled = true;
        Destroy(instantiateB);
        Destroy(instantiateC);
    }
    /*     HeartCo.a = 0.5f;
            HeartImg.color = HeartCo;
            HeartBtn.enabled = false;
            Vector3 newPos = instantiateB.transform.position;
            Vector3 newPos2 = instantiateC.transform.position;
            float filledTime = 0f;
            while (filledTime <= coolTime)
            {
                yield return new WaitForFixedUpdate();
                newPos.y += movespeed * Time.deltaTime;
                instantiateB.transform.position = newPos;
                newPos2.y += movespeed * Time.deltaTime;
                instantiateC.transform.position = newPos2;
                filledTime += Time.deltaTime;
                HeartImg.fillAmount = filledTime / coolTime;
            }
            HeartCo.a = 1.0f;
            HeartImg.color = HeartCo;
            HeartBtn.enabled = true;
            Destroy(instantiateB);
            Destroy(instantiateC);*/
    IEnumerator StartCool(float duration)
    {
        Debug.Log(duration);
        HeartCo.a = 0.5f;
        HeartImg.color = HeartCo;
        HeartBtn.enabled = false;
        float filledTime = 60f - duration;
        while (filledTime <= duration)
        {
            yield return new WaitForFixedUpdate();
            filledTime += Time.deltaTime;
            HeartImg.fillAmount = filledTime / duration;
        }
        HeartCo.a = 1.0f;
        HeartImg.color = HeartCo;
        HeartBtn.enabled = true;
        PlayerPrefs.DeleteKey(CoolTimeKey);
    }
}