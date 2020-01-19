using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private static float seconds;
    private static float simHrs;

    public float SimHrs
    {
        get { return simHrs; }
        set { simHrs = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds >= 1)
        {
            seconds = seconds - 1;
            simHrs += (float)0.5;
            seconds += Time.deltaTime;
        } 
        else
        {
            seconds += Time.deltaTime;
        }
    }
}
