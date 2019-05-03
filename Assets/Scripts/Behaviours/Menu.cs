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

    public RectTransform selectionButtonTransform;

    public Sprite arrowPleine;
    public Sprite arrowEmpty;

    public GameObject lotFlechePlayer1;
    public GameObject lotFlechePlayer2;
    public bool gameRunning;

    public void Start()
    {
        gameRunning = false;
        Time.timeScale = 0;
    }

    public void OnClickStartGame()
    {
        gameRunning = true;
        Time.timeScale = 1;
        MainMenu.SetActive(false);
        HUD.SetActive(true);
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }

    public void Update()
    {
        if(MainMenu.activeSelf == false && Input.GetButtonDown("Select_Button") )
        {
            StopGame();
        }
    }

    public void StopGame()
    {
        if (Menu.instance.gameRunning == true)
        {
            PauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            mainCam.gameObject.transform.position = mainCam.camPosition;
            Menu.instance.gameRunning = false;
            return;
        }
        else
        {
            PauseMenu.SetActive(false);
            Menu.instance.gameRunning = true;
            Time.timeScale = 1;
            return;
        }
    }

    public void StartGame()
    {
        GameManager.instance.LoadArenas();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
