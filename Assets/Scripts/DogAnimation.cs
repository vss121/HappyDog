using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;
using Unity.UI;
using UnityEngine.UI;

public class DogAnimation : MonoBehaviour, IPointerClickHandler
{
    public Animator anim;
    public Vector2 mousePos;
    private void Start()
    {
        anim.SetBool("isTouched", false);
    }
    /*    public void Update()
        {
            if (Input.GetMouseButton(0))
            {
                mousePos = Input.mousePosition;
            }
            Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "Dog")
                {
                    anim.SetBool("isTouched", true);
                    StartCoroutine(WaitForSec());
                }
            }
        }*/
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
