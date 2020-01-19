using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LightController : MonoBehaviour
{
    private GameObject source;
    private TimeController timeController;
    private float flightStartTime;
    private float flightEndTime;
    private float sleepStartTime;
    private float sleepEndTime;

    public float FlightStartTime
    {
        get { return this.flightStartTime; }
        set { this.flightStartTime = value; }
    }

    public float FlightEndTime
    {
        get { return this.flightEndTime; }
        set { this.flightEndTime = value; }
    }

    public float SleepStartTime
    {
        get { return this.sleepStartTime; }
        set { this.sleepStartTime = value; }
    }

    public float SleepEndTime
    {
        get { return this.sleepEndTime; }
        set { this.sleepEndTime = value; }
    }

    private void Awake()
    {
        source = GameObject.Find(Constants.LIGHT_SOURCE);
        source.SetActive(true);

        ReadSleepTime();
        ReadFlightTime();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeController = source.AddComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        float simHrs = timeController.SimHrs;
        Light light = source.GetComponent<Light>();
        UpdateLight(simHrs, light);
        Debug.Log(simHrs);
    }

    private void ReadSleepTime()
    {
        string path = Constants.USER_FILE;
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("[SleepStartTime]"))
                        sleepStartTime = int.Parse(line.Replace("[SleepStartTime]", "").Trim());
                    else if (line.Contains("[SleepEndTime]"))
                        sleepEndTime = int.Parse(line.Replace("[SleepEndTime]", "").Trim());
                }
            }
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void ReadFlightTime()
    {
        string path = Constants.FLIGHT_FILE;
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("[FlightStartTime]"))
                        flightStartTime = int.Parse(line.Replace("[FlightStartTime]", "").Trim());
                    else if (line.Contains("[FlightEndTime]"))
                        flightEndTime = int.Parse(line.Replace("[FlightEndTime]", "").Trim());
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    void UpdateLight(float simHrs, Light light)
    {
        float duration = (sleepEndTime - sleepStartTime)/2;

        if (simHrs < sleepStartTime)
            light.color = Constants.WAKEUP_COLOR;
        else if (simHrs < (sleepStartTime + duration))
        {
            float m_t = Mathf.PingPong((simHrs-sleepStartTime), duration) / duration;
            light.color = Color.Lerp(Constants.WAKEUP_COLOR, Constants.SLEEP_COLOR, m_t);
            float phi = (simHrs - sleepStartTime) / duration * Mathf.PI;
            float amplitude = (Mathf.Cos(phi) * 0.5F + 0.5F) * 4;
            light.intensity = amplitude;
        } 
        else if (simHrs < sleepEndTime)
        {
            light.intensity = 0;
        }
        else
        {
            light.color = Constants.WAKEUP_COLOR;
            light.intensity = 4;
        }
    }


}
