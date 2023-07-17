    using Mapbox.Examples;
using Mapbox.Utils;
using System;
using UnityEngine;

public class ItemsEvent : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f;
    // [SerializeField] float amplitude = 2.0f;
    // [SerializeField] float frequency = 0.50f;

    LocationStatus playerLocation;
    public Vector2d eventPos;

    MenuUIManager menuUIManager;

    public bool isClicked;

    private void Start()
    {
        isClicked = false;
        menuUIManager = GameObject.Find("MiddlePanel").GetComponent<MenuUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        FloatAndRotatePointer();
        ListenInput();
    }

    private void FloatAndRotatePointer()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        //transform.position= new Vector3(transform.position.x,(Mathf.Sin(Time.fixedTime*Mathf.PI*frequency)*amplitude)+15, transform.position.z );
    }

    public static double DistanceTo(double lat1, double lon1, double lat2, double lon2)
    {
        double rlat1 = Math.PI * lat1 / 180;
        double rlat2 = Math.PI * lat2 / 180;
        double theta = lon1 - lon2;
        double rtheta = Math.PI * theta / 180;
        double dist =
            Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;

        return dist * 1.609344;
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

                        playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
                        //var currentPlayerLocation = new GeoCoordinate(playerLocation.GetLocationLat(), playerLocation.GetLocationLon());
                        //var eventLocation = new GeoCoordinate(eventPos[0], eventPos[1]);
                        var distance = DistanceTo(playerLocation.GetLocationLat(), playerLocation.GetLocationLon(), eventPos[0], eventPos[1]);  // distance calculation
                                                                                                                                                //Debug.Log(distance + "km");


                        if (distance < 0.05)    // 50m 이내라면
                        {
                            if(!isClicked)
                            {
                                menuUIManager.DisplayUserInRangePanel();
                                isClicked = true;
                            }
                            else
                            {
                                menuUIManager.DisplayClickedPanel();
                            }
                            
                        }
                        else
                        {
                            menuUIManager.DisplayUserNotInRangePanel();
                        }
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

                    playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
                    //var currentPlayerLocation = new GeoCoordinate(playerLocation.GetLocationLat(), playerLocation.GetLocationLon());
                    //var eventLocation = new GeoCoordinate(eventPos[0], eventPos[1]);
                    var distance = DistanceTo(playerLocation.GetLocationLat(), playerLocation.GetLocationLon(), eventPos[0], eventPos[1]);  // distance calculation
                    Debug.Log(distance + "km");


                    if (distance < 0.05)    // 50m 이내라면
                    {
                        if (!isClicked)
                        {
                            menuUIManager.DisplayUserInRangePanel();
                            isClicked = true;
                        }
                        else
                        {
                            menuUIManager.DisplayClickedPanel();
                        }

                    }
                    else
                    {
                        menuUIManager.DisplayUserNotInRangePanel();
                    }
                }

            }
        }






    }
}