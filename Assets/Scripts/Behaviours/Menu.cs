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

    public SubMenu HUD;
    public SubMenu MainMenu;
    public SubMenu PauseMenu;
    public SubMenu upgradeMenu;

    public RectTransform selectionButtonTransform;




    public bool gameRunning;

    public void Start()
    {
        gameRunning = true;

    }

    public void Update()
    {
        if (MainMenu.gameObject.activeSelf == true && Input.GetButtonDown("Start_Button"))
        {
            MainMenu.gameObject.SetActive(false);
            HUD.gameObject.SetActive(true);
        }

        if(MainMenu.gameObject.activeSelf == false && Input.GetButtonDown("Select_Button") )
        {
            StopGame();
        }
    }

    public void StopGame()
    {
        if (gameRunning == true)
        {
            PauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            gameRunning = false;
            return;
        }
        else
        {
            PauseMenu.gameObject.SetActive(false);
            gameRunning = true;
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
