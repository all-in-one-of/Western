using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject inputManagerPrefab;
    public GameObject uiManagerPrefab;
    public GameObject enemyManagerPrefab;
    public GameObject saveManagerPrefab;
    public GameObject levelManagerPrefab;
    public GameObject playerManagerPrefab;


    public ControllerBehaviour Player1;
    public ControllerBehaviour Player2;

    [System.NonSerialized] public int playerCount = 2;



    private void Start()
    {
        GameObject saveManager = Instantiate(saveManagerPrefab, transform);
        saveManager.name = "SaveManager";

        GameObject levelManager = Instantiate(levelManagerPrefab, transform);

        GameObject inputManager = Instantiate(inputManagerPrefab, transform);
        inputManager.name = "InputManager";

        GameObject uiManager = Instantiate(uiManagerPrefab, transform);
        uiManager.name = "UIManager";

        GameObject enemyManager = Instantiate(enemyManagerPrefab, transform);
        enemyManager.name = "EnemyManager";

        GameObject playerManager = Instantiate(playerManagerPrefab, transform);
        playerManager.name = "PlayerManager";

        DontDestroyOnLoad(gameObject);

        LoadGame();
    }

    public void Update()
    {
        /*if (Player1.data.state == ControllerData.PlayerStates.Dead && Player2.data.state == ControllerData.PlayerStates.Dead)
        {
            //Game finished
            Debug.Log("Game Finished");
        }*/
    }


    public void LoadGame()
    {
        SceneLoader.LoadScene("Game", InitGame);
    }

    public void InitGame()
    {
        for (int i = 0; i < playerCount; i++)
        {
            SaveManager.instance.LoadPlayerData(i);
        }

        if (playerCount <= 0)
        {
            Debug.Log("no player connected");
            return;
        }

        if (SaveManager.instance.playerDatas[0].fromFile)
        {
            LevelManager.instance.currentLevel = SaveManager.instance.playerDatas[0].currentLevel;
        }
        else
        {
            LevelManager.instance.currentLevel = 0;
        }

        LevelManager.instance.LoadLevel(delegate
        {
            //Spawn player
            for (int i = 0; i < playerCount; i++)
            {
                PlayerManager.instance.SpawnPlayer(i);
            }

            //spawn enemies
            EnemyManager.instance.Init();

        });
    }
}