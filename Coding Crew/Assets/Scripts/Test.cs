﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;



public class Test : MonoBehaviour
{

    public GameObject allTest;

    public bool TestActive;
    public GameObject pointer;
    public Camera mainCam;

    public posterVeiw posVeiw;

	public GameObject timeTillObject;
    public Slider timerSlider;
    public int waitTime;
    public int timeForTest;
    private float timeTillTest;
    public Text timeTillText;

    bool pauseTest;
    bool TestStarted;

    public Button TurnIn;

	public WakeUpManager wakeUpManager;

    void Start()
    {
        pointer.SetActive(true);
        mainCam.transform.rotation = Quaternion.Euler(new Vector3(35f, 0f, 0f));
        mainCam.transform.position = new Vector3(-3.2f, 1.48f, -2f);
        mainCam.fieldOfView = 75f;
        
        timerSlider.maxValue = waitTime;
    }

    void Update()
    {
		if(wakeUpManager.wakeUp)
		{
			timeTillObject.SetActive(true);

			if (!TestStarted)
			{
				BeforeStart();
			}
			else if (TestStarted)
			{
			 AfterStart();
			}
		}

        if (TestActive)
        {
            allTest.SetActive(true);
        }
        else if (!TestActive)
        {
            allTest.SetActive(false);
        }

    }

    public void TestOver()
    {
        SceneManager.LoadScene("GradeCheck");
    }

    void AfterStart()
    {
        timeTillTest += Time.deltaTime;

        timeTillTest = Mathf.Clamp(timeTillTest, 0f, timeForTest);

        timerSlider.value = timeForTest - timeTillTest;

        if(timerSlider.value == 0)
        {
            TestOver();
        }
    }

    void BeforeStart()
    {
        timeTillTest += Time.deltaTime;

        timeTillTest = Mathf.Clamp(timeTillTest, 0f, waitTime);

        timerSlider.value = waitTime - timeTillTest;

        if (timeTillTest == waitTime && TestActive == false)
        {
            TestActive = true;
            TestStarted = true;
            timeTillText.text = "TEST OVER IN";
            timerSlider.value = timeForTest;
            timerSlider.maxValue = timeForTest;
            timeTillTest = 0;
            TestTime();
        }
    }

    public void TestPause()
    {
        TestActive = false;
        pointer.SetActive(true);
        mainCam.transform.rotation = Quaternion.Euler(new Vector3(35f, 0f, 0f));
        mainCam.transform.position = new Vector3(-3.2f, 1.48f, -2f);
        mainCam.fieldOfView = 75f;

        Cursor.lockState = CursorLockMode.Locked;
        
        Cursor.visible = false;
    }

    public void TestTime()
    {
        pointer.SetActive(false);
        TestActive = true;
        mainCam.transform.rotation = Quaternion.Euler(new Vector3(90f,0f,0f));
        mainCam.transform.position = new Vector3(-3.345f, 1.35f, -1.54f);
        //mainCam.transform.position = new Vector3(-3.319f, 1.819f, -1.542f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
