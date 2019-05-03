using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject canvasPrefab;
    

    [System.NonSerialized] public Menu menu;

    public void SpawnCanvas()
    {
        menu=Instantiate(canvasPrefab).GetComponent<Menu>();
        
    }

    public void GameOver()
    {
        menu.HUD.SetActive(false);
        menu.GameOverMenu.SetActive(true);

    }

    private IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(5);
        menu.GameOverMenu.SetActive(false);
        menu.MainMenu.SetActive(true);
    }



    [MenuItem("Western/NextArena")]
    public static void NextArena()
    {
        Destroy(LevelManager.instance.currentStand);
        GameManager.instance.levelFinished = false;
        if (LevelManager.instance.currentArena < LevelManager.instance.levels[LevelManager.instance.currentLevel].arenas.Count-1)
        {
            LevelManager.instance.currentArena++;
            LevelManager.instance.LoadArena();
        }
        else
        {
            NextLevel();
        }
        
    }


    [MenuItem("Western/NextLevel")]
    public static void NextLevel()
    {
        LevelManager.instance.currentArena = 0;
        LevelManager.instance.DestroyLevel();
        if (LevelManager.instance.currentLevel < LevelManager.instance.levels.Count-1)
        {
            LevelManager.instance.currentLevel++;
        }
        else
        {
            LevelManager.instance.currentLevel = 0;
        }
        LevelManager.instance.LoadLevel(LevelManager.instance.LoadArena);
    }
    
}
