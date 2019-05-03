using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandSpawn : MonoBehaviour
{
    public GameObject preview;
    public GameObject standPrefab;

    public static List<StandSpawn> spawns;
    // Start is called before the first frame update
    void Start()
    {
        if (spawns == null)
        {
            spawns = new List<StandSpawn>();
        }
        spawns.Add(this);
        preview.SetActive(false);
    }

    
}
