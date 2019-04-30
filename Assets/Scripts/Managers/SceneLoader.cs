using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    private static UnityAction callback;
    private static float arenaOffset=0;
    private static int arenasLeftToSpawn = 0;
    private static int currentArenaIndex;
    private static List<Arena> arenas;


    public static void LoadScene(string sceneName,UnityAction callback=null)
    {
        SceneLoader.callback = callback;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (callback != null)
        {
            callback.Invoke();
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void AddArenas(List<Arena> arenas,int arenasToSpawnCount)
    {
        SceneLoader.arenas = arenas;
        arenasLeftToSpawn = arenasToSpawnCount;
        SceneManager.sceneLoaded += OnSceneAdded;
        currentArenaIndex = Random.Range(0, arenas.Count-1);
        SceneManager.LoadScene(arenas[currentArenaIndex].sceneName, LoadSceneMode.Additive);
    }

    private static void OnSceneAdded(Scene arg0, LoadSceneMode arg1)
    {
        arg0.GetRootGameObjects()[0].transform.position=new Vector3(arenaOffset,0,0);
        arenasLeftToSpawn--;
        if (arenasLeftToSpawn > 0)
        {
            arenaOffset += arenas[currentArenaIndex].sceneWidth;
            currentArenaIndex = Random.Range(0, arenas.Count);
            SceneManager.LoadScene(arenas[currentArenaIndex].sceneName, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.sceneLoaded -= OnSceneAdded;
            arenaOffset = 0;
        }
    }
}
