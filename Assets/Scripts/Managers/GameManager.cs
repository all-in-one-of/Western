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

    [System.NonSerialized] public int credits;



    private void Start()
    {
        GameObject saveManager = Instantiate(saveManagerPrefab, transform);
        saveManager.name = "SaveManager";

        GameObject levelManager = Instantiate(levelManagerPrefab, transform);

        GameObject inputManager = Instantiate(inputManagerPrefab,transform);
        inputManager.name = "InputManager";

        GameObject uiManager = Instantiate(uiManagerPrefab, transform);
        uiManager.name = "UIManager";

        GameObject enemyManager = Instantiate(enemyManagerPrefab, transform);
        enemyManager.name = "EnemyManager";

        DontDestroyOnLoad(gameObject);

        LoadGame();
    }


    public void LoadGame()
    {
        SceneLoader.LoadScene("Game", InitGame);
    }

    public void InitGame()
    {
        SaveManager.instance.LoadData();
        if (SaveManager.instance.playerData != null) {
            LevelManager.instance.currentLevel = SaveManager.instance.playerData.currentLevel;
        }
        else
        {
            LevelManager.instance.currentLevel = 0;
        }
        
        LevelManager.instance.GenerateMap();
        //Spawn player

        EnemyManager.instance.Init();


    }
}
