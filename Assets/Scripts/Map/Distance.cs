using Mapbox.Unity.Location;
using Mapbox.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Distance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI distanceText;

    private AbstractLocationProvider _locationProvider = null;
    Location currLoc;

    private double totalDistance = 0d;   // ÃÑ »êÃ¥°Å¸®
    private double currDistance = 0d;
    private double myCurrLon = 0d;
    private double myCurrLat = 0d;
    private double myPrevLon = 0d;
    private double myPrevLat = 0d;

    // Start is called before the first frame update
    void Start()
    {
        if (null == _locationProvider)
        {
            _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
        }
    }

    void Update()
    {
        if (Time.frameCount % 15 == 0)
        {
            currLoc = _locationProvider.CurrentLocation;


            if (!currLoc.IsLocationServiceInitializing && currLoc.IsLocationServiceEnabled && !currLoc.LatitudeLongitude.Equals(Vector2d.zero))
            {
                if (!double.IsNaN(myCurrLat) && !double.IsNaN(myCurrLon) && !double.IsNaN(myPrevLat) && !double.IsNaN(myPrevLon))
                {


                    myCurrLat = currLoc.LatitudeLongitude[1];
                    myCurrLon = currLoc.LatitudeLongitude[0];

                    if ((myPrevLat != 0d) && (myPrevLon != 0d))
                    {

                        currDistance = DistanceTo(myCurrLat, myCurrLon, myPrevLat, myPrevLon);
                    }
                    myPrevLat = myCurrLat;
                    myPrevLon = myCurrLon;
                    totalDistance += currDistance;
                    //Debug.Log(currDistance);
                    distanceText.text = string.Format("{0:F2}", totalDistance);
                    //Debug.Log("totalDistance : " + totalDistance);
                }
                
            }



            //_statusText.text = string.Format("{0}", currLoc.LatitudeLongitude);


        }

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
        if (dist>=0.0d && dist<1.0d) return dist * 1.609344;
        return 0;
    }

    public double GetLocationLat()
    {
        return currLoc.LatitudeLongitude.x;
    }
    public double GetLocationLon()
    {
        return currLoc.LatitudeLongitude.y;
    }

}
