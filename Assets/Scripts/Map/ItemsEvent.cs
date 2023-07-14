using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using Mapbox.Utils;
namespace System.Device
{

    public class ItemsEvent : MonoBehaviour
    {
        [SerializeField] float rotationSpeed = 50f;
        // [SerializeField] float amplitude = 2.0f;
        // [SerializeField] float frequency = 0.50f;

        LocationStatus playerLocation;
        public Vector2d eventPos;

        // Update is called once per frame
        void Update()
        {
            FloatAndRotatePointer();
        }

        private void FloatAndRotatePointer()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            //transform.position= new Vector3(transform.position.x,(Mathf.Sin(Time.fixedTime*Mathf.PI*frequency)*amplitude)+15, transform.position.z );
        }

        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
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

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }


        private void OnMouseDown()
        {
            playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
            //var currentPlayerLocation = new GeoCoordinate(playerLocation.GetLocationLat(), playerLocation.GetLocationLon());
            //var eventLocation = new GeoCoordinate(eventPos[0], eventPos[1]);
            var distance = DistanceTo(playerLocation.GetLocationLat(), playerLocation.GetLocationLon(), eventPos[0], eventPos[1]);  // distance calculation
            Debug.Log(distance);
            Debug.Log("Clicked");
        }
    }
}