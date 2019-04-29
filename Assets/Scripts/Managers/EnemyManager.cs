using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private bool on = false;
    public List<EnemyBehaviour> enemies;


    public void Init()
    {
        Enable(on);
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
