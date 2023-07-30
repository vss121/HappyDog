using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBlock : MonoBehaviour
{
    public Slider slide;
    private void Awake()
    {
        slide.interactable = false;
    }
}