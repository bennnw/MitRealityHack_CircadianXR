using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class LightController : MonoBehaviour
{
    private GameObject source;
    private TimeController timeController;
    private Time startTime;
    private Time endTime;
    private Time currTime;
    private float sleepStart;
    private float sleepEnd;
    private Color sleepColor = Color.red;
    private Color wakeUpColor = Color.blue;

    public Time StartTime
    {
        get { return this.startTime; }
        set { this.startTime = value; }
    }

    public Time EndTime
    {
        get { return this.endTime; }
        set { this.endTime = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("DirectionalLightSource");
        timeController = source.AddComponent<TimeController>();
        source.SetActive(true);
        sleepStart = 5;
        sleepEnd = 10;
    }

    // Update is called once per frame
    void Update()
    {
        float simHrs = timeController.SimHrs;
        Light light = source.GetComponent<Light>();
        UpdateLight(simHrs, light);
        Debug.Log(simHrs);
    }

    void UpdateLight(float simHrs, Light light)
    {
        float duration = (sleepEnd - sleepStart)/2;

        if (simHrs < sleepStart)
            light.color = wakeUpColor;
        else if (simHrs < (sleepStart + duration))
        {
            float m_t = Mathf.PingPong(Time.time, duration) / duration;
            light.color = Color.Lerp(wakeUpColor, sleepColor, m_t);
            float phi = (simHrs - sleepStart) / duration * Mathf.PI;
            float amplitude = (Mathf.Cos(phi) * 0.5F + 0.5F) * 4;
            light.intensity = amplitude;
        } 
        else if (simHrs < sleepEnd)
        {
            light.intensity = 0;
        }
        else
        {
            light.color = wakeUpColor;
            light.intensity = 4;
        }
    
    }


}
