using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels;
    public List<int> selectedArenaIndexes;
    private UnityAction callback;
    

    [System.NonSerialized] public int currentLevel;
    [System.NonSerialized] public int currentArena;

    private int arenasLeftToSpawn;
    private int currentArenaIndex;
    private float arenaOffset = 0;
    private bool useSave = false;


    public void LoadLevel(UnityAction callback=null) {
        this.callback = callback;

        //on considère toujours la sauvegarde du premier joueur connecté
        if (SaveManager.instance.playerDatas[0].fromFile)
        {
            useSave = true;
        }
        else
        {
            useSave = false;
        }
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        arenasLeftToSpawn = levels[currentLevel].arenasToSpawnCount;
        //SceneLoader.AddArenas(levels[currentLevel].arenas, levels[currentLevel].arenasToSpawnCount);
        print("arenas left to spawn : " + arenasLeftToSpawn);

        SceneManager.sceneLoaded += OnSceneAdded;
        if (!useSave)
        {
            currentArenaIndex = Random.Range(0, levels[currentLevel].arenas.Count);
        }
        else
        {
            currentArenaIndex = SaveManager.instance.playerDatas[0].arenaIndexes[levels[currentLevel].arenas.Count - arenasLeftToSpawn];
        }
        selectedArenaIndexes = new List<int>() { currentArenaIndex };
        SceneManager.LoadScene(levels[currentLevel].arenas[currentArenaIndex].sceneName, LoadSceneMode.Additive);
    }

    private void OnSceneAdded(Scene arg0, LoadSceneMode arg1)
    {
        arg0.GetRootGameObjects()[0].transform.position = new Vector3(arenaOffset, 0, 0);
        arenasLeftToSpawn--;
        if (arenasLeftToSpawn > 0)
        {
            arenaOffset += levels[currentLevel].arenas[currentArenaIndex].sceneWidth;
            if (!useSave)
            {
                currentArenaIndex = Random.Range(0, levels[currentLevel].arenas.Count);
            }
            else
            {
                currentArenaIndex = SaveManager.instance.playerDatas[0].arenaIndexes[levels[currentLevel].arenas.Count - arenasLeftToSpawn];
            }
            
            selectedArenaIndexes.Add(currentArenaIndex);
            SceneManager.LoadScene(levels[currentLevel].arenas[currentArenaIndex].sceneName, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.sceneLoaded -= OnSceneAdded;
            arenaOffset = 0;

            //build navmesh

            for (int i = 0; i < WalkableSurface.surfaces.Count; i++)
            {
                WalkableSurface.surfaces[i].gameObject.AddComponent<NavMeshSourceTag>();
            }




            if (callback != null)
            {
                callback.Invoke();
            }
        }
    }


}
