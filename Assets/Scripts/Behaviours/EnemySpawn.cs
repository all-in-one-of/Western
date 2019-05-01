using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static List<EnemySpawn> spawns;
    public Transform self;
    public GameObject preview;

    public Enemy enemyToSpawn;

    private void Awake()
    {
        if (spawns == null)
        {
            spawns = new List<EnemySpawn>();
        }
        spawns.Add(this);
    }

    private void Start()
    {
        preview.SetActive(false);
    }
}
