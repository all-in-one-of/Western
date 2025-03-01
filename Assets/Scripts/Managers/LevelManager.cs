﻿using System.Collections;
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


    private Scene[] arenaScenes;
    private GameObject currentRootArena;

    public Camera[] cameras = new Camera[4];
    public Light[] directionalLights=new Light[4];
    private Light currentDirectionalLight;
    private Camera currentCamera;

    public GameObject standPrefab;

    [System.NonSerialized] public GameObject currentStand;


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

    public void LoadArena()
    {
        if (currentRootArena != null)
        {
            currentRootArena.SetActive(false);
        }
        
        RemoveAllPlayers();
        RemoveAllEnemies();
        if (currentCamera != null)
        {
            currentCamera.enabled = false;
            currentCamera.GetComponent<CameraBehaviour>().Activate(false);
        }
        if (currentDirectionalLight != null)
        {
            currentDirectionalLight.enabled = false;
        }
        
        SpawnPlayersAndEnemies();
        currentCamera = cameras[currentArena];
        if (currentCamera != null)
        {
            currentCamera.enabled = true;
            currentCamera.GetComponent<CameraBehaviour>().Init();
        }

        currentDirectionalLight = directionalLights[currentArena];
        if (currentDirectionalLight != null)
        {
            currentDirectionalLight.enabled = true;
        }


        UIManager.instance.menu.MainMenu.gameObject.SetActive(false);
        UIManager.instance.menu.HUD.gameObject.SetActive(true);
        InputManager.instance.Enable(false);
        currentRootArena = arenaScenes[currentArena].GetRootGameObjects()[0];

    }

    private void SpawnPlayersAndEnemies()
    {
        //Spawn players according to the number of spawn points in the arena
        for (int i = 0; i < PlayerSpawn.levelSpawns[currentArena].Count; i++)
        {
            PlayerManager.instance.SpawnPlayer(i);

        }

        //spawn enemies
        EnemyManager.instance.Init();
    }

    private void RemoveAllPlayers()
    {
        if (PlayerManager.instance.players != null)
        {
            for (int i = 0; i < PlayerManager.instance.players.Count; i++)
            {
                Destroy(PlayerManager.instance.players[i].gameObject);
                
            }
            PlayerManager.instance.players.Clear();
        }

    }

    private void RemoveAllEnemies()
    {
        if (EnemyManager.instance.enemies != null)
        {
            for (int i = 0; i < EnemyManager.instance.enemies.Count; i++)
            {
                Destroy(EnemyManager.instance.enemies[i].gameObject);
            }
            EnemyManager.instance.enemies.Clear();
        }
    }

    public void GenerateLevel()
    {
        //Nécessaire, ne pas enlever
        EnemySpawn.InitSpawns();
        PlayerSpawn.InitSpawns();
        cameras = new Camera[4];
        arenaScenes = new Scene[4];

        arenasLeftToSpawn = levels[currentLevel].arenasToSpawnCount;
        //SceneLoader.AddArenas(levels[currentLevel].arenas, levels[currentLevel].arenasToSpawnCount);

        SceneManager.sceneLoaded += OnSceneAdded;
        if (!useSave)
        {
            int random = Random.Range(0, levels[currentLevel].arenas.Count);
            while (currentArenaIndex == random)
            {
                random= Random.Range(0, levels[currentLevel].arenas.Count);
            }
            currentArenaIndex = random;
        }
        else
        {
            currentArenaIndex = SaveManager.instance.playerDatas[0].arenaIndexes[levels[currentLevel].arenas.Count - arenasLeftToSpawn];
        }
        selectedArenaIndexes = new List<int>() { currentArenaIndex };
        SceneManager.LoadScene(levels[currentLevel].arenas[currentArenaIndex].sceneName, LoadSceneMode.Additive);
    }

    public void DestroyLevel()
    {
        RemoveAllPlayers();
        RemoveAllEnemies();
        Destroy(currentStand);
        for (int i = 0; i < levels.Count; i++)
        {
            StartCoroutine(UnloadArena(arenaScenes[i]));
        }
        
    }

    private IEnumerator WaitManyTimes()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            yield return null;
        }

        //here all the arenas are unloaded

    }

    private IEnumerator UnloadArena(Scene arena)
    {
        AsyncOperation unload= SceneManager.UnloadSceneAsync(arena);
        yield return unload;
        StartCoroutine(WaitManyTimes());
    }


    private void OnSceneAdded(Scene arena, LoadSceneMode arg1)
    {
        
        Transform rootTransform = arena.GetRootGameObjects()[0].transform;
        rootTransform.position = new Vector3(arenaOffset, 0, 0);

        //rootTransform.gameObject.SetActive(false);

        

        //set arena index to every spawn point
        List<EnemySpawn> enemySpawnsInThisArena = GetObjectsByComponent<EnemySpawn>(rootTransform);

        int arenaNumber = levels[currentLevel].arenas.Count - arenasLeftToSpawn;

        arenasLeftToSpawn--;

        for (int i = 0; i < enemySpawnsInThisArena.Count; i++)
        {
            enemySpawnsInThisArena[i].Init(arenaNumber);
        }

        List<PlayerSpawn> playerSpawnsInThisArena = GetObjectsByComponent<PlayerSpawn>(rootTransform);

        for (int i = 0; i < playerSpawnsInThisArena.Count; i++)
        {
            playerSpawnsInThisArena[i].Init(arenaNumber);
        }

        List<Camera> cams = GetObjectsByComponent<Camera>(rootTransform);
        List<Light> lights = GetObjectsByComponent<Light>(rootTransform);

        cameras[arenaNumber] = cams[0];
        arenaScenes[arenaNumber] = arena;
        directionalLights[arenaNumber] = lights[0];
        lights[0].enabled = false;
        //

        if (arenasLeftToSpawn > 0)
        {
            arenaOffset += levels[currentLevel].arenas[currentArenaIndex].sceneWidth;
            if (!useSave)
            {
                currentArenaIndex = Random.Range(0, levels[currentLevel].arenas.Count);
            }
            else
            {
                currentArenaIndex = SaveManager.instance.playerDatas[0].arenaIndexes[arenaNumber];
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
            if (Obstacle.obstacles == null)
            {
                Debug.LogWarning("t'as oublié de mettre des obstacles wesh");
                return;
            }
            for (int i = 0; i < Obstacle.obstacles.Count; i++)
            {
                Obstacle.obstacles[i].gameObject.AddComponent<NavMeshSourceTag>();
            }

            currentCamera = cameras[0];
            currentDirectionalLight = directionalLights[0];

            if (callback != null)
            {
                callback.Invoke();
            }
        }
    }



    private List<T> GetObjectsByComponent<T>(Transform parent)
    {
        List<T> objects = new List<T>();

        SearchChildrenByComponent(objects, parent);

        return objects;
    }

    private void SearchChildrenByComponent<T>(List<T> children,Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform childTransform = parent.GetChild(i);
            T childObject = childTransform.GetComponent<T>();

            if (childObject!= null && !childObject.Equals(default(T)))
            {
                children.Add(childObject);
            }
            else
            {
                SearchChildrenByComponent(children, childTransform);
            }
        }
    }

    public void SpawnStand()
    {
        currentStand=Instantiate(standPrefab,StandSpawn.spawns[currentArena].transform);
    }
}
