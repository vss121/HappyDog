using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class ChatManager : MonoBehaviour
{
    public GameObject YellowArea, WhiteArea, DateArea;
    public RectTransform ContentRect;
    public Scrollbar scrollBar;
    public Toggle MineToggle;
    public TMP_InputField inputField;  // 문자 입력창
    AreaScript LastArea;

    TouchScreenKeyboard keyboard;   

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
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UpdateChat();
        }
    }

    // enter키나 버튼으로 전송
    public void UpdateChat()
    {
        string text = inputField.text;

        if (text.Equals("")) return;

        Chat(true, text, "me", null);
        // Chat(false, text, "타인", null);

        inputField.text = "";   // inputField 내용 초기화
    }



    public void Chat(bool isSend, string text, string user, Texture2D picture)
    {
        if (text.Trim() == "") return;

        bool isBottom = scrollBar.value <= 0.00001f;


        // 노랑, 흰색영역을 만들고 텍스트 대입
        AreaScript Area = Instantiate(isSend ? YellowArea : WhiteArea).GetComponent<AreaScript>();
        Area.transform.SetParent(ContentRect.transform, false);
        Area.BoxRect.sizeDelta = new Vector2(800, Area.BoxRect.sizeDelta.y);    // 박스 최대 크기
        Area.TextRect.GetComponent<Text>().text = text;
        Fit(Area.BoxRect);


        // 두 줄 이상이면 크기를 줄이고, 한 줄이 아래로 내려가면 바로 전 크기를 대입 
        float X = Area.TextRect.sizeDelta.x + 42;
        float Y = Area.TextRect.sizeDelta.y;
        if (Y > 49)
        {
            for (int i = 0; i < 200; i++)
            {
                Area.BoxRect.sizeDelta = new Vector2(X - i * 2, Area.BoxRect.sizeDelta.y);
                Fit(Area.BoxRect);

                if (Y != Area.TextRect.sizeDelta.y) { Area.BoxRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y); break; }
            }
        }
        else Area.BoxRect.sizeDelta = new Vector2(X, Y);


        // 시간 가져오는 부분
        DateTime t = DateTime.Now;
        Area.Time = t.ToString("yyyy-MM-dd-HH-mm");
        Area.User = user;


        // 시간 보여주는 부분
        int hour = t.Hour;
        if (t.Hour == 0) hour = 12;
        else if (t.Hour > 12) hour -= 12;
        Area.TimeText.text =  hour + ":" + t.Minute.ToString("D2") + (t.Hour > 12 ? " PM " : " AM ") ;


        // 이전과 같을 경우
        bool isSame = LastArea != null && LastArea.Time == Area.Time && LastArea.User == Area.User;
        if (isSame) LastArea.TimeText.text = "";
        Area.Tail.SetActive(!isSame);


        // 이전과 같을 경우 (상대)
        if (!isSend)
        {
            Area.UserImage.gameObject.SetActive(!isSame);
            Area.UserText.gameObject.SetActive(!isSame);
            Area.UserText.text = Area.User;
            if (picture != null) Area.UserImage.sprite = Sprite.Create(picture, new Rect(0, 0, picture.width, picture.height), new Vector2(0.5f, 0.5f));
        }



        Fit(Area.BoxRect);
        Fit(Area.AreaRect);
        Fit(ContentRect);
        LastArea = Area;


        // 스크롤바가 맨 아래로 내리기
        Invoke("ScrollDelay", 0.03f);
    }


    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);


    void ScrollDelay() => scrollBar.value = 0;
}
