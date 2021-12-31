using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossing : MonoBehaviour
{
    public Transform trafficLightNorth;
    public Transform trafficLightSouth;
    public Transform trafficLightWest;
    public Transform trafficLightEast;

    private bool switching = false;
    private bool northSouth = true;
    private bool westEast = false;
    private int timer = 0;
    public int switchIntervalSec = 6;

    // Start is called before the first frame update
    void Start()
    {
        //trafficLightNorth.GetComponent<TrafficLight>().inCrossing = true;
        //trafficLightSouth.GetComponent<TrafficLight>().inCrossing = true;
        //trafficLightWest.GetComponent<TrafficLight>().inCrossing = true;
        //trafficLightEast.GetComponent<TrafficLight>().inCrossing = true;

        trafficLightNorth.GetComponent<TrafficLight>().green = true;
        trafficLightSouth.GetComponent<TrafficLight>().green = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!switching)
        {
            timer++;
            if (timer > switchIntervalSec * 100)
            {
                switching = true;
                StartCoroutine(Switch());
            }
        }
    }

    private IEnumerator Switch()
    {
        if (northSouth)
        {
            trafficLightNorth.GetComponent<TrafficLight>().green = false;
            trafficLightSouth.GetComponent<TrafficLight>().green = false;
            yield return new WaitForSeconds(2);
            trafficLightEast.GetComponent<TrafficLight>().green = true;
            trafficLightWest.GetComponent<TrafficLight>().green = true;
        } else
        {
            trafficLightEast.GetComponent<TrafficLight>().green = false;
            trafficLightWest.GetComponent<TrafficLight>().green = false;
            yield return new WaitForSeconds(2);
            trafficLightNorth.GetComponent<TrafficLight>().green = true;
            trafficLightSouth.GetComponent<TrafficLight>().green = true;
        }
        northSouth = !northSouth;
        westEast = !westEast;
        switching = false;

    }
}
