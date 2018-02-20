﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FlurpState { Dead, Recharging, Unhappy}

[RequireComponent(typeof(FlurpMovement))]
public class Flurp : MonoBehaviour {

    [Header("Station Control")]
    public Station currentStation;
    public Station targetStation;
    //public bool canBeMoved;

    [Header("Graphics")]
    public Sprite engineRoomIcon;
    public Sprite atmoRoomIcon;
    public Sprite plantRoomIcon;
    public Sprite pumpRoomIcon;

    public Image targetRoomSprite;
    public Image speechBubbleSprite;

    public Animator flurpUI;

    [Header("Settings")]
    public float timerToReachNextStation = 20f;
    public float baseTimeToReachHappiness = 20f;
    public float tickLength = 1f;
    public TextMesh health;

    private FlurpState flurpState = FlurpState.Unhappy;
    private float lastTick;

    float currentHappinessValue;
    float targetHappinessValue;
    float timeToReachTarget;


    void Start()
    {
        lastTick = Time.time;
        targetRoomSprite.gameObject.SetActive(false);
        speechBubbleSprite.gameObject.SetActive(false);

        NewRandomRoom();
    }

    void Update()
    {
        Tick();
    }

    void NewRandomRoom()
    {
        // Pick a new room randomly amongst the rooms flurp isn't currently in
        do
        {
            targetStation = (Station)Random.Range(0, System.Enum.GetValues(typeof(Station)).Length - 1);
        }
        while (currentStation == targetStation);

        flurpState = FlurpState.Unhappy;

        timeToReachTarget = timerToReachNextStation;
        //targetHappinessValue = timerToReachNextStation;

        health.text = currentHappinessValue.ToString();
        DisplayNewObjective();

        //canBeMoved = true;
    }

    void DisplayNewObjective()
    {

        if (targetRoomSprite == null)
            return;

        switch (targetStation)
        {
            case Station.EngineRoom:
                targetRoomSprite.sprite = engineRoomIcon;
                break;
            case Station.AtmoRoom:
                targetRoomSprite.sprite = atmoRoomIcon;
                break;
            case Station.PlantRoom:
                targetRoomSprite.sprite = plantRoomIcon;
                break;
            case Station.WaterPumps:
                targetRoomSprite.sprite = pumpRoomIcon;
                break;
            case Station.None:
                break;
            default:
                break;
        }

        targetRoomSprite.gameObject.SetActive(true);
        speechBubbleSprite.gameObject.SetActive(true);
    }

    public void SetCurrentStation(Station station)
    {
        if (flurpState == FlurpState.Dead)
            return;

        currentStation = station;

        if(currentStation == targetStation && flurpState == FlurpState.Unhappy)
        {
            flurpState = FlurpState.Recharging;
            currentHappinessValue = 0f;
            targetHappinessValue = Mathf.RoundToInt(Random.Range(baseTimeToReachHappiness * .75f, baseTimeToReachHappiness * 1.25f));

            flurpUI.SetBool("Flashing", false);

            //canBeMoved = false;
            targetRoomSprite.gameObject.SetActive(false);
            speechBubbleSprite.gameObject.SetActive(false);
        }
        else if(currentStation == targetStation && flurpState == FlurpState.Recharging)
        {
            targetRoomSprite.gameObject.SetActive(false);
            speechBubbleSprite.gameObject.SetActive(false);
        }
        else
        {
            DisplayNewObjective();
        }
    }

    // Only do the operations at set intervals
    void Tick()
    {
        if (Time.time - lastTick > tickLength)
        {
            lastTick = Time.time;
            WellBeeingControl(); 
        }
    }

    void EndGame()
    {
        flurpState = FlurpState.Dead;

        GameManager.instance.GameOver();
    }

    // Update Flurps current happiness and take appropriate action
    void WellBeeingControl()
    {
        if(flurpState == FlurpState.Unhappy)
        {
            timeToReachTarget -= tickLength;

            if(timeToReachTarget <= 0)
            {
                EndGame();
            }

            if(timeToReachTarget <= 10)
            {
                flurpUI.SetBool("Flashing", true);
            }
        }

        if(currentStation == targetStation)
        {
            currentHappinessValue++;
        }
        else
        {
            currentHappinessValue--;
            DisplayNewObjective();
        }

        if(currentHappinessValue >= targetHappinessValue)
        {
            //canBeMoved = true;
            NewRandomRoom();
        }

        if(currentHappinessValue <= 0 && flurpState == FlurpState.Recharging)
        {
            EndGame();
        }

        health.text = currentHappinessValue.ToString();
    }
}
