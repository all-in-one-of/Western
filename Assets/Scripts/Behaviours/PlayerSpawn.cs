using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    public static List<PlayerSpawn>[] levelSpawns = new List<PlayerSpawn>[4];
    public Transform self;
    public GameObject preview;
    private int arenaIndex;


    public static void InitSpawns()
    {
        for (int i = 0; i < 4; i++)
        {
            levelSpawns[i] = new List<PlayerSpawn>();
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
