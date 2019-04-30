using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private bool on = false;
    public List<EnemyBehaviour> enemies;

    public GameObject enemyPrefab;


    public void Init()
    {
        
        for (int i = 0; i < EnemySpawn.spawns.Count; i++)
        {
            SpawnEnemy(i);
        }
        Enable(on);
    }


    private void SpawnEnemy(int enemyNumber)
    {
        if (enemyNumber < EnemySpawn.spawns.Count)
        {
            enemies.Add(Instantiate(enemyPrefab, EnemySpawn.spawns[enemyNumber].self.position, EnemySpawn.spawns[enemyNumber].self.rotation).GetComponent<EnemyBehaviour>());
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
            enemies[i].active = on;
        }
    }
}
