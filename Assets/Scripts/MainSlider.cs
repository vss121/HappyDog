using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSlider : MonoBehaviour
{
    public Slider likabilityBar;
    public Slider cleanlinessBar;
    public Slider depressionBar;
    public Slider hungerBar;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(likabilityBar.value);
    }

    // Update is called once per frame
    void Update()
    {
        //if(likabilityBar.value <=0 ) transform.Find("Fill Area").gameObject.SetActive(false);
        //else transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
