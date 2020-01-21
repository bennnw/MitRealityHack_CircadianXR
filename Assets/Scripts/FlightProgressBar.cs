using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    private TimeController timeController;
    public GameObject plane, startPos, endPos, smCity1, smCity2;
    private float t, t0, t1, l, tDusk, tDawn;
    Vector3 sm1, sm2, planePos;
    float start, end;
    void Start()
    {
        t = 13;
        t0 = 4;
        t1 = 9;
        start = startPos.transform.localPosition.x;
        end = endPos.transform.localPosition.x;
        l = (end - start) / 2;
        tDusk = 2 * t0 * l / t;
        tDawn = 2 * t1 / t - 1;
        // Debug.Log("Flight: " + l);
        // Debug.Log("Dusk: " + tDusk);
        // Debug.Log("Dawn: " + tDawn);
        sm1 = smCity1.transform.localScale;
        sm1.x = (l - tDusk) * 2;
        smCity1.transform.localScale = sm1;
        sm2 = smCity2.transform.localScale;
        sm2.x = (tDawn - l) * 2;
        smCity2.transform.localScale = sm2;
        timeController = plane.AddComponent<TimeController>();
        planePos = plane.transform.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
        float simHrs = timeController.SimHrs;
        float d = end - start;
        if (planePos.x < end)
        {
            planePos.x += simHrs * d * 2 / 26;
            plane.transform.localPosition = planePos; 
            Debug.Log(planePos.x);
        }
    }

    private void Reset()
    {
        planePos.x = start;
    }
}

    
