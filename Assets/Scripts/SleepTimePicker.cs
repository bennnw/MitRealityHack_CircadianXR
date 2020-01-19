using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class SleepTimePicker : MonoBehaviour
{
    private GameObject startPickTime;
    private GameObject noSelection;
    private GameObject startButton;
    private GameObject confirmButton;

    private static float seconds = 0;
    private static float hours = 0;

    private float sleepStartTime;
    private float sleepEndTime;
    private float flightStartTime = 7;
    private float flightEndTime = 8 + 12;

    public float SleepStartTime
    {
        get { return sleepStartTime; }
        set { sleepStartTime = value; }
    }

    public float SleepEndTime
    {
        get { return sleepEndTime; }
        set { sleepEndTime = value; }
    }

    public float FlightStartTime
    {
        get { return flightStartTime; }
        set { flightStartTime = value; }
    }

    public float FlightEndTime
    {
        get { return flightEndTime; }
        set { flightEndTime = value; }
    }

    private List<string> imgNameList = new List<string>();
    private List<GameObject> imgObjectList = new List<GameObject>();
    private List<string> imgBtnNameList = new List<string>();
    private List<GameObject> imgBtnObjectList = new List<GameObject>();
    private List<string> selectedList = new List<string>();

    private void Awake()
    {
        this.startPickTime = GameObject.Find(Constants.START_PICK_TIME);
        this.noSelection = GameObject.Find(Constants.NO_SELECTION);
        this.startButton = GameObject.Find(Constants.START_BUTTON);
        this.confirmButton = GameObject.Find(Constants.CONFIRM_BUTTON);

        InitializeImgNameList();
        InitializeImgObjList();
        InitializeImgBtnNameList();
        InitializeImgBtnObjList();
    }

    private void InitializeImgNameList()
    {
        imgNameList.Add(Constants.IMG_0_1);
        imgNameList.Add(Constants.IMG_1_2);
        imgNameList.Add(Constants.IMG_2_3);
        imgNameList.Add(Constants.IMG_3_4);
        imgNameList.Add(Constants.IMG_4_5);
        imgNameList.Add(Constants.IMG_5_6);
        imgNameList.Add(Constants.IMG_6_7);
        imgNameList.Add(Constants.IMG_7_8);
        imgNameList.Add(Constants.IMG_8_9);
        imgNameList.Add(Constants.IMG_9_10);
        imgNameList.Add(Constants.IMG_10_11);
        imgNameList.Add(Constants.IMG_11_12);
    }

    private void InitializeImgObjList()
    {
        for (int i = 0; i < imgNameList.Count; i++)
        {
            GameObject imgObj = GameObject.Find(imgNameList[i]);
            imgObj.SetActive(false);
            imgObjectList.Add(imgObj);
        }
    }

    private void InitializeImgBtnNameList()
    {
        imgBtnNameList.Add(Constants.BTN_0_1);
        imgBtnNameList.Add(Constants.BTN_1_2);
        imgBtnNameList.Add(Constants.BTN_2_3);
        imgBtnNameList.Add(Constants.BTN_3_4);
        imgBtnNameList.Add(Constants.BTN_4_5);
        imgBtnNameList.Add(Constants.BTN_5_6);
        imgBtnNameList.Add(Constants.BTN_6_7);
        imgBtnNameList.Add(Constants.BTN_7_8);
        imgBtnNameList.Add(Constants.BTN_8_9);
        imgBtnNameList.Add(Constants.BTN_9_10);
        imgBtnNameList.Add(Constants.BTN_10_11);
        imgBtnNameList.Add(Constants.BTN_11_12);
    }

    private void InitializeImgBtnObjList()
    {
        for (int i = 0; i < imgBtnNameList.Count; i++)
        {
            GameObject btnObj = GameObject.Find(imgBtnNameList[i]);
            btnObj.SetActive(false);
            imgBtnObjectList.Add(btnObj);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.startPickTime.SetActive(true);
        this.noSelection.SetActive(false);

        AddStartButtonEvents(startButton);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        
    }

    private void UpdateTimer()
    {
        seconds += Time.deltaTime;
        if (seconds >= 1)
        {
            seconds = seconds - 1;
            hours += (float)0.5;
        }
    }

    void AddStartButtonEvents(GameObject buttonObj)
    {
        EventTrigger eventTrigger = buttonObj.AddComponent<EventTrigger>();
        EventTrigger.Entry clickEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        clickEntry.callback.AddListener(TriggerStart);
        eventTrigger.triggers.Add(clickEntry);
    }

    private void TriggerStart(BaseEventData eventData)
    {
        startPickTime.SetActive(false);
        startButton.SetActive(false);
        Debug.Log("Start picking your regular sleeping time now ...");
        noSelection.SetActive(true);
        SetDefaultHours();
        SetupConfirmButton();
    }

    private void SetDefaultHours()
    {
        selectedList.Add(Constants.IMG_11_12);
        selectedList.Add(Constants.IMG_0_1);
        selectedList.Add(Constants.IMG_1_2);
        selectedList.Add(Constants.IMG_2_3);
        selectedList.Add(Constants.IMG_3_4);
        selectedList.Add(Constants.IMG_4_5);
        selectedList.Add(Constants.IMG_5_6);
        selectedList.Add(Constants.IMG_6_7);

        for (int i = 0; i < selectedList.Count; i++)
        {
            int index = imgNameList.IndexOf(selectedList[i]);
            Debug.Log(selectedList[i]);
            imgObjectList[index].SetActive(true);
        }

        foreach (GameObject btnObj in imgBtnObjectList)
        {
            btnObj.SetActive(true);
            AddImgButtonEvents(btnObj);
        }
    }

    private void AddImgButtonEvents(GameObject buttonObj)
    {
        EventTrigger eventTrigger = buttonObj.AddComponent<EventTrigger>();
        EventTrigger.Entry clickEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        clickEntry.callback.AddListener( delegate { FlipSelected(buttonObj); });
        eventTrigger.triggers.Add(clickEntry);
    }

    private void FlipSelected(GameObject btnObj)
    {
        // Unity mirrors the index so I have to flip it
        int index = 11 - imgBtnNameList.IndexOf(btnObj.name);
        string imgName = imgNameList[index];
        if (selectedList.Contains(imgName))
        {
            selectedList.Remove(imgName);
            imgObjectList[index].SetActive(false);
            Debug.Log(imgName + " is deselected");
        } 
        else
        {
            selectedList.Add(imgName);
            imgObjectList[index].SetActive(true);
            Debug.Log(imgName + " is selected");
        }
    }

    private void SetupConfirmButton()
    {
        EventTrigger eventTrigger = confirmButton.AddComponent<EventTrigger>();
        EventTrigger.Entry clickEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        clickEntry.callback.AddListener(delegate { getSleepTime(); });
        eventTrigger.triggers.Add(clickEntry);
    }

    private void getSleepTime()
    {
        List<int> timeList = new List<int>();
        foreach (string img in selectedList)
        {
            int number = int.Parse(img.Replace("Img_", "")) - 1;
            if (number < flightStartTime)
                number = number + 12;
            timeList.Add(number);
        }
        timeList.Sort();
        foreach (int t in timeList)
        {
            if (t > flightStartTime)
            {
                sleepStartTime = t;
                break;
            }
        }
        timeList.Reverse();
        foreach (int t in timeList)
        {
            if (t < flightEndTime)
            {
                sleepEndTime = t;
                break;
            }
        }
        sleepEndTime = sleepStartTime + (sleepEndTime - sleepStartTime) / 4 * 3;
        sleepStartTime = sleepStartTime - flightStartTime;
        sleepEndTime = sleepEndTime - flightStartTime;

        Debug.Log(sleepStartTime);
        Debug.Log(sleepEndTime);
        WriteUserFile();
    }

    private void WriteUserFile()
    {
        string path = Constants.USER_FILE;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        try
        {
            using (StreamWriter writer = File.CreateText(path))
            {
                writer.WriteLine("[SleepStartTime] " + sleepStartTime);
                writer.WriteLine("[SleepEndTime] " + sleepEndTime);
            }
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void ReadFlightFile()
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
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
