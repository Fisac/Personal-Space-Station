﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPointChecker : MonoBehaviour
{

    public bool stationedFirst;
    public bool stationedSecond;

    public bool pressedFirst;
    public bool pressedSecond;

    public Station sendingStation;
    public Station recievingStation;

    public bool repaired;

    private Light warningLight;

    public GameObject repairSquare1, repairSquare2;
    public GameObject particleEffect1, particleEffect2;

    public float counter;

    private float target;

    private void Start()
    {
        target = UnityEngine.Random.Range(75, 150);
        repaired = true;
        warningLight = GetComponent<Light>();

        ToggleSquares(true);
    }

    void Update()
    {

        if (stationedFirst &&
            stationedSecond &&
            pressedFirst &&
            pressedSecond)
        {
            repaired = true;
        }

        if (repaired)
        {
            warningLight.intensity = 0;
            ToggleSquares(false);
        }
        else
        {
            warningLight.intensity = 4.7f;
            ToggleSquares(true);
        }

        TimeCounter();
    }

    private void TimeCounter()
    {
        counter += Time.deltaTime;

        if (counter > target && repaired)
        {
            target += UnityEngine.Random.Range(75, 150);
            repaired = false;
        }
    }

    private void ToggleSquares(bool repaired)
    {
        if (repairSquare1 == null || repairSquare2 == null)
            return;

        if (this.repaired)
        {
            repairSquare1.SetActive(false);
            repairSquare2.SetActive(false);

            if (particleEffect1 != null)
                particleEffect1.SetActive(false);
            if (particleEffect2 != null)
                particleEffect2.SetActive(false);
        }
        else
        {
            repairSquare1.SetActive(true);
            repairSquare2.SetActive(true);

            if (particleEffect1 != null)
                particleEffect1.SetActive(true);
            if (particleEffect2 != null)
                particleEffect2.SetActive(true);
        }
    }
}