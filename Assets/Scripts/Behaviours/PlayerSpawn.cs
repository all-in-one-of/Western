using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public static List<PlayerSpawn> spawns;
    public Transform self;
    public GameObject preview;


    private void Awake()
    {
        if (spawns == null)
        {
            spawns = new List<PlayerSpawn>();   
        }
        spawns.Add(this);
    }

    private void Start()
    {
        preview.SetActive(false);
    }
}
