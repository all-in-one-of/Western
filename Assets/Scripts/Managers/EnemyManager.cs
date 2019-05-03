using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private bool on = false;
    public List<EnemyBehaviour> enemies;

    public GameObject enemyPrefab;

    public float minGroupSpeed=1;
    public float maxGroupSpeed=2;

    public GameObject bulletPrefab;


    public void Init()
    {
        for (int i = 0; i < EnemySpawn.levelSpawns.Length; i++)
        {
            print("arena " + i + ", enemySpawn count : " + EnemySpawn.levelSpawns[i].Count);
        }



        for (int i = 0; i < EnemySpawn.levelSpawns[LevelManager.instance.currentArena].Count; i++)
        {
            SpawnEnemy(i);
        }
        Enable(true);
    }


    private void SpawnEnemy(int enemyNumber)
    {
        if (enemyNumber < EnemySpawn.levelSpawns[LevelManager.instance.currentArena].Count)
        {
            enemies.Add(Instantiate(enemyPrefab, EnemySpawn.levelSpawns[LevelManager.instance.currentArena][enemyNumber].self.position, EnemySpawn.levelSpawns[LevelManager.instance.currentArena][enemyNumber].self.rotation).GetComponent<EnemyBehaviour>());
            enemies[enemies.Count - 1].Init(EnemySpawn.levelSpawns[LevelManager.instance.currentArena][enemyNumber].enemyToSpawn);
        }
        else
        {
            Debug.Log("no more enemy spawn point");
            return;
        }
    }


    public void Enable(bool on)
    {
        this.on = on;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Activate(on);
        }
    }

    public void RemoveEnemy(EnemyBehaviour enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count <= 0)
        {
            //pop stand achat
        }
    }
}
