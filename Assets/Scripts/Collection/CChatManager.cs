using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class CChatManager : MonoBehaviour
{
    public GameObject YellowArea, WhiteArea;
    public RectTransform ContentRect;
    public Scrollbar scrollBar;
    public TMP_InputField inputField;  // 문자 입력창
    AreaScript LastArea;
  

    // Update is called once per frame
    void Update()
    {
        // 입력창이 포커스되어있지 않을 때 Enter키 누르면
        if (Input.GetKeyDown(KeyCode.Return) && inputField.isFocused == false)
            // 입력창 포커스 활성화
            inputField.ActivateInputField();
    }


    public void OnEndEditEventMethod()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UpdateChat();
        }
    }

    // enter키나 버튼으로 전송
    public async void UpdateChat()
    {
        string text = inputField.text;

        if (text.Equals("")) return;

        Chat(true, text, "me", null);
        inputField.text = "";   // inputField 내용 초기화

        // text가!!!!!!!!!!!!!!!!! 중요

    }



    public void Chat(bool isSend, string text, string user, Texture2D picture)
    {
        if (text.Trim() == "") return;

        bool isBottom = scrollBar.value <= 0.00001f;


        // 채팅 박스 영역을 만들고 텍스트 대입
        AreaScript Area = Instantiate(isSend ? YellowArea : WhiteArea).GetComponent<AreaScript>();
        Area.transform.SetParent(ContentRect.transform, false);
        Area.BoxRect.sizeDelta = new Vector2(1000, Area.BoxRect.sizeDelta.y);    // 박스 최대 크기
        Area.TextRect.GetComponent<Text>().text = text;
        Fit(Area.BoxRect);


        // 두 줄 이상이면 크기를 줄이고, 한 줄이 아래로 내려가면 바로 전 크기를 대입 



        // 시간 가져오는 부분
        DateTime t = DateTime.Now;
        Area.Time = t.ToString("yyyy-MM-dd-HH-mm");
        Area.User = user;


        // 시간 보여주는 부분
        int hour = t.Hour;
        if (t.Hour == 0) hour = 12;
        else if (t.Hour > 12) hour -= 12;
        Area.TimeText.text =  hour + ":" + t.Minute.ToString("D2") + (t.Hour > 12 ? " PM " : " AM ") ;






        Fit(Area.BoxRect);
        Fit(Area.AreaRect);
        Fit(ContentRect);
        LastArea = Area;


        // 스크롤바 맨 아래로 내리기
        Invoke("ScrollDelay", 0.03f);
    }


    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);


    void ScrollDelay() => scrollBar.value = 0;
}
