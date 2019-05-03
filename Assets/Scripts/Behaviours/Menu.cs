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
    public GameObject CreditMenu;


    public Sprite arrowPleine;
    public Sprite arrowEmpty;

    public GameObject lotFlechePlayer1;
    public GameObject lotFlechePlayer2;
    public bool gameRunning;

    public void Start()
    {
        gameRunning = false;
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
        if(MainMenu.activeSelf == false && Input.GetButtonDown("Select_Button") && CreditMenu.activeSelf==false )
        {
            StopGame();
        }

        if (MainMenu.activeSelf == false && Input.GetButtonDown("B_Button") && CreditMenu.activeSelf == true)
        {
            OnClickBackMenu();
        }
    }

    public void StopGame()
    {
        if (Menu.instance.gameRunning == true)
        {
            PauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
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

    public void OnClickCredits()
    {
        MainMenu.SetActive(false);
        CreditMenu.SetActive(true);
    }

    public void OnClickBackMenu()
    {
        MainMenu.SetActive(true);
        CreditMenu.SetActive(false);
    }
}
