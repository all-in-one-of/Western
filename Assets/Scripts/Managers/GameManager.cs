using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject inputManagerPrefab;
    public GameObject uiManagerPrefab;
    public GameObject enemyManagerPrefab;

    [System.NonSerialized] public int score;



    private void Start()
    {
        GameObject inputManager = Instantiate(inputManagerPrefab,transform);
        inputManager.name = "InputManager";

        GameObject uiManager = Instantiate(uiManagerPrefab, transform);
        uiManager.name = "UIManager";

        GameObject enemyManager = Instantiate(enemyManagerPrefab, transform);
        enemyManager.name = "EnemyManager";

        DontDestroyOnLoad(gameObject);
    }


    public void LoadGame()
    {
        SceneLoader.LoadScene("Game", InitManagers);
    }

    public void InitManagers()
    {
        
    }
}
