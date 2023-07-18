using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;
using Unity.UI;
using UnityEngine.UI;

public class DogAnimation : MonoBehaviour
{
    public GameObject Dog1;
    public GameObject Dog2;
    public bool otherAnim;
    private void Awake()
    {
        otherAnim = false;
        Dog1.SetActive(true);
        Dog2.SetActive(false);
    }
    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(PlayOtherAnimation());
        }
    }

    private IEnumerator PlayOtherAnimation()
    {
        otherAnim = true;
        Dog2.SetActive(true);
        Dog1.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        Dog1.SetActive(true);
        Dog2.SetActive(false);
        otherAnim = false;
    }
}
