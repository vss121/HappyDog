using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WalkTime : MonoBehaviour
{

    //[SerializeField] float timeStart;
    [SerializeField] TextMeshProUGUI timeText;
    // [SerializeField] TextMeshProUGUI StartPauseImg;

    bool timeActive = false;

    // 시간 text 관련
    private float secondsCount;
    private int minuteCount;

    // 버튼 이미지 관련
    public Sprite startImg;
    public Sprite pauseImg;
    Image imgLocation;

    // Start is called before the first frame update
    void Start()
    {
        //timeText.text = timeStart.ToString("F2");
        imgLocation = GameObject.Find("StartPauseButton").GetComponent<Image>();
        StartPauseBtn();
    }

    // Update is called once per frame
    void Update()
    {
        StartTime();
    }

    void StartTime()
    {
        if (timeActive)
        {
            secondsCount += Time.deltaTime;
            timeText.text =minuteCount+":"+(int)secondsCount;
            if (secondsCount >= 60)
            {
                minuteCount++;
                secondsCount = 0;
            }
        }
    }


    public void StartPauseBtn()
    {
        timeActive = !timeActive;

        imgLocation.sprite = timeActive ? pauseImg : startImg;
    }

    public void ResetBtn()
    {
        timeActive = false;
        if (!timeActive)
        {
            
            imgLocation.sprite = startImg;
        }
        if (secondsCount > 0)
        {
            secondsCount = 0;
            minuteCount = 0;
            timeText.text = minuteCount + ":" + (int)secondsCount;
            
        }
    }
}
