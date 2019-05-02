using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : Singleton<Menu>
{
    public Image player1lifebar;
    public Image player2lifebar;
    public Text scoreText;
    public Text timerBeforeLoosingComboText;

    public GameObject HUD;
    public GameObject MainMenu;
    public GameObject PauseMenu;
    public GameObject upgradeMenu;

    public CameraBehaviour mainCam;

    public bool gameRunning;

    public void Start()
    {
        gameRunning = true;

    }

    public void Update()
    {
        if (MainMenu.activeSelf == true && Input.GetButtonDown("Start_Button"))
        {
            MainMenu.SetActive(false);
            HUD.SetActive(true);
        }

        if(MainMenu.activeSelf == false && Input.GetButtonDown("Select_Button") )
        {
            StopGame();
        }
    }

    public void StopGame()
    {
        if (gameRunning == true)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            mainCam.gameObject.transform.position = mainCam.camPosition;
            gameRunning = false;
            return;
        }
        else
        {
            PauseMenu.SetActive(false);
            gameRunning = true;
            Time.timeScale = 1;
            return;
        }
    }
}
