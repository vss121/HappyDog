using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DepressScore : MonoBehaviour
{
    public Slider[] slider = new Slider[21];
    float score; 

    void Start()
    {
        score = 0;
    }

    public void Submit()
    {
        for(int i = 0; i<slider.Length;i++){
            score += slider[i].value * 10;   
        }

        score = score/210 * 100;
        
        Debug.Log(score); 
        GameObject.Find("Database").GetComponent<DepressionTestDB>().InsertDepressionScore((int)Math.Ceiling(score));
        score=0;
    }
    

}
