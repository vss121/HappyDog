using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DogAnimation : MonoBehaviour, IPointerClickHandler
{
    public Animator anim;
    public Vector2 mousePos;
    private void Start()
    {
        anim.SetBool("isTouched", false);
    }
    private IEnumerator WaitForSec()
    {
        anim.SetBool("isTouched", true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isTouched", false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(WaitForSec());
    }
}
