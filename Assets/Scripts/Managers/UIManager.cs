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



    [MenuItem("Western/NextArena")]
    public static void NextArena()
    {
        if (LevelManager.instance.currentArena < 4)
        {
            LevelManager.instance.currentArena++;
            LevelManager.instance.LoadArena();
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
