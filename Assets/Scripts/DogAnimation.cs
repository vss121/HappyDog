using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;
using Unity.UI;
using UnityEngine.UI;

public class DogAnimation : MonoBehaviour
{
    public Animator anim;
    public Vector2 mousePos;
    private void Start()
    {
        anim.SetBool("isTouched", false);
    }
    public void Update()
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
                Debug.Log("µð¹ö±ë 00 " + anim.GetBool("isTouched"));
                anim.SetBool("isTouched", true);
                Debug.Log("µð¹ö±ë 11 " + anim.GetBool("isTouched"));
                StartCoroutine(WaitForSec());
            }
        }
    }

    private IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2.0f);
                Debug.Log("µð¹ö±ë 22 " + anim.GetBool("isTouched"));
        anim.SetBool("isTouched", false);
    }
}
