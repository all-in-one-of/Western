using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static List<EnemySpawn>[] levelSpawns=new List<EnemySpawn>[4];
    public Transform self;
    public GameObject preview;

    public Enemy enemyToSpawn;

    private int arenaIndex;

    public static void InitSpawns()
    {
        for (int i = 0; i < LevelManager.instance.levels[LevelManager.instance.currentLevel].arenasToSpawnCount; i++)
        {
            levelSpawns[i] = new List<EnemySpawn>();
        }
    }

    public void Init(int arenaIndex)
    {
        this.arenaIndex = arenaIndex;
        levelSpawns[arenaIndex].Add(this);
    }


    private void Start()
    {
        preview.SetActive(false);
    }
}
