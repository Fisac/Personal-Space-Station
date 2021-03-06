﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Interactable[] stationRooms;
    public Flurp flurp;

    public bool gameHasEnded = false;

    public float restartDelay = 1f;

    public Text enterName;
    public Text yourScore;
    public Text failureText;
    private Text failedStation;

    public GameObject GameOverPanel;

    // Update is called once per frame;
    void Update()
    {
        int numberOfAliveRooms = 0;

        foreach (var room in stationRooms)
        {
            //If the rooms have more than 0 health they are alive;
            if (room.stationHealth > 0)
            {
                numberOfAliveRooms++;
            }
        }
        // End game if one station breaks
        if ((numberOfAliveRooms < stationRooms.Length))
        {
            EndGame();
        }
    }
    //Ends the game and restarts the game.
    public void EndGame()
    {
        if (gameHasEnded == false)
        {

            Time.timeScale = 0f;
            GameManager.instance.gameIsPaused = true;

            ShowGameOverScreen();
        }
    }

    public void ShowGameOverScreen()
    {
        GameOverPanel.SetActive(true);
        if (flurp.flurpState==FlurpState.Dead)
        {
            failureText.text = "Flurp died!";
        }

        for (int i = 0; i < stationRooms.Length; i++)
        {
            if (stationRooms[i].stationHealth <= 0)
            {
                failureText.text = stationRooms[i].stationName;
                failureText.text = failureText.text +" has failed!";
            }
        }
        yourScore.text = FindObjectOfType<Scoring>().totalScore.ToString("000000");
    }

    public void OnNameEntered()
    {

        GameOverPanel.SetActive(false);

        FindObjectOfType<Scoring>().SortHighscores(enterName.text);
        gameHasEnded = true;

        GoBackToMainMenu();
        Time.timeScale = 1f;
    }

    void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu 1");
    }

}
