using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    public GameObject straightRoad;
    public GameObject crossing;
    public GameObject simulationScreen;

    public GameObject road1;
    public GameObject road2;
    public GameObject road3;
    private bool started = false;

    [SerializeField] int roadSpeed = 6;
    [SerializeField] int timescale = 1;
    private float frametime;
    private int hours;
    private int timer;

    public void Start() 
    {
        Time.timeScale = timescale;
        road1.GetComponent<Road>().maxSpeed = roadSpeed;
        road2.GetComponent<Road>().maxSpeed = roadSpeed;
        road3.GetComponent<Road>().maxSpeed = roadSpeed;
    }

    public void StartSimulation(int inputHours)
    {
        straightRoad.SetActive(true);
        simulationScreen.SetActive(true);

        hours = inputHours;
        frametime = hours * 180000;
        timer = 0;
        started = true;
    }

    public void FixedUpdate()
    {
        if (started)
        {
            timer++;
            if (timer > frametime)
            {
                Time.timeScale = 0;
            }
        }
    }
}
