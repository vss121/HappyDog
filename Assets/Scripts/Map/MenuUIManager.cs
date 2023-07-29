using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject eventPanelUserInRange;
    [SerializeField] private GameObject eventPanelUserNotInRange;
    [SerializeField] private GameObject chestPanel;
    [SerializeField] private GameObject clickedPanel;
    bool isUIPanelActive;
    
    public int aquiredTrophy;
    public int aquiredMoney;
    public int aquiredExp;
    public TextMeshProUGUI panelText;
    MapDB gm;

    // Start is called before the first frame update
    void Start()
    {
        aquiredTrophy=0;
        gm=GameObject.Find("Database").GetComponent<MapDB>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayUserInRangePanel()
    {
        if (!isUIPanelActive)
        {
            eventPanelUserInRange.SetActive(true);
            isUIPanelActive = true;
            aquiredTrophy+=1;

            //
            if (aquiredTrophy==1) {
                gm.getQuote();
                // quote이 뜨게
            } else if (aquiredTrophy%2==0) {
                aquiredMoney+=1;
                gm.getMoney();
                // 

            } else if (aquiredTrophy%2==1) {
                aquiredExp+=1;
                gm.getExp();
                //
            }
        }
        
    }

    public void DisplayUserNotInRangePanel()
    {
        if (!isUIPanelActive)
        {
            eventPanelUserNotInRange.SetActive(true);
            isUIPanelActive = true;
        }
    }

    public void CloseButtonClick()
    {
        eventPanelUserInRange.SetActive(false);
        eventPanelUserNotInRange.SetActive(false);
        chestPanel.SetActive(false);
        clickedPanel.SetActive(false);
        isUIPanelActive = false;
    }

    public void DisplayChestPanel()
    {
        if (!isUIPanelActive)
        {
            chestPanel.SetActive(true);
            isUIPanelActive = true;
        }
    }
    public void DisplayClickedPanel()
    {
        if (!isUIPanelActive)
        {
            clickedPanel.SetActive(true);
            isUIPanelActive = true;
        }
    }

    public void PlusBtnClick()
    {
        //map.flyTo({ center: e.features[0].geometry.coordinates, zoom: 10});
    }
}
