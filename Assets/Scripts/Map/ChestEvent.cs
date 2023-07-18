using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEvent : MonoBehaviour
{
    

    MenuUIManager menuUIManager;
    private void Start()
    {
        menuUIManager = GameObject.Find("Canvas").GetComponent<MenuUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        ListenInput();
    }




    private void ListenInput()  // touch시와 click시 
    {

        if (Input.touchCount > 0)
        {
            // 터치 입력 시,
            Touch touch = Input.GetTouch(0);       // only touch 0 is used

            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider == GetComponent<BoxCollider>())
                    {
                        // 실행할 event
                        menuUIManager.DisplayChestPanel();

                    }
                }
            }


        }
        else if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == GetComponent<BoxCollider>())
                {
                    // 실행할 event
                    menuUIManager.DisplayChestPanel();

                }

            }
        }






    }
}
