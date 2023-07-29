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
    public TMP_InputField inputField;  // 문자 입력창
    AreaScript LastArea;
    

    int collNum;

    string[] quotesArray = new string[] {"You are stronger than you believe, braver than you know.", "In the midst of darkness, remember that stars can't shine without it.", "When life knocks you down, it's a chance to see things from a new perspective.", "Embrace the journey, for it is the path to self-discovery and growth.", "Your worth is not defined by your achievements but by the kindness you show.", "The greatest strength lies in the ability to rise after every fall.", "When the world feels heavy, find solace in the beauty of nature.", "Every storm eventually passes, leaving behind a clearer sky.", "Rainbows appear after the darkest rains; hope is never truly lost.", "Take one step at a time, and even the longest journey becomes achievable.", "In the stillness of the present moment, you'll find tranquility.", "Your uniqueness is your superpower; embrace it.", "Let go of what you can't control and focus on what you can change.", "Sometimes, the best thing you can do is give yourself permission to rest.", "Your scars tell a story of resilience and survival; wear them proudly.", "Be the reason someone believes in the goodness of people.", "Kindness costs nothing but enriches everything it touches.", "Be the light that brightens someone's day; it costs nothing but can mean everything.", "The smallest acts of love can have the most significant impact on others.", "The world needs your gifts; don't shy away from sharing them."};

    void Start()
    {
        int i=0;
        CollectionDB gm= GameObject.Find("Database").GetComponent<CollectionDB>();
        gm.DBCollectionInitialize();
        collNum=gm.data_collection;

        if (collNum<=20) {  // 조사한 collection 개수가 20개라서
            for(i=0; i<collNum; i++)
                Chat(quotesArray[i]);

        } else {
            Debug.Log("collections error");
        }
    }



    public void Chat( string text)
    {
        if (text.Trim() == "") return;


        // 채팅 박스 영역을 만들고 텍스트 대입
        AreaScript Area = Instantiate(YellowArea).GetComponent<AreaScript>();
        Area.transform.SetParent(ContentRect.transform, false);
        Area.BoxRect.sizeDelta = new Vector2(900, 300);    // 박스 최대 크기
        Area.TextRect.GetComponent<Text>().text = text;
        Fit(Area.BoxRect);


        // 두 줄 이상이면 크기를 줄이고, 한 줄이 아래로 내려가면 바로 전 크기를 대입 



        // 시간 가져오는 부분
        DateTime t = DateTime.Now;
        Area.Time = t.ToString("yyyy-MM-dd-HH-mm");


        // 시간 보여주는 부분
        int hour = t.Hour;
        if (t.Hour == 0) hour = 12;
        else if (t.Hour > 12) hour -= 12;
        //Area.TimeText.text =  hour + ":" + t.Minute.ToString("D2") + (t.Hour > 12 ? " PM " : " AM ") ;

        Fit(Area.BoxRect);
        Fit(Area.AreaRect);
        Fit(ContentRect);
        LastArea = Area;


        // 스크롤바 맨 아래로 내리기
        //Invoke("ScrollDelay", 0.03f);
    }


    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);


    //************************


}
