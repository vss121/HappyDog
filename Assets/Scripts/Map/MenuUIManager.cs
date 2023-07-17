using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject eventPanelUserInRange;
    [SerializeField] private GameObject eventPanelUserNotInRange;
    [SerializeField] private GameObject chestPanel;
    [SerializeField] private GameObject clickedPanel;
    bool isUIPanelActive;

    // Start is called before the first frame update
    void Start()
    {
        
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
