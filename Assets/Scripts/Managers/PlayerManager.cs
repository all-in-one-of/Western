using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [System.NonSerialized] public List<PlayerBehaviour> players;
    public GameObject playerPrefab;

    public void SpawnPlayer(Transform spawnPoint)
    {
        players.Add(Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity).GetComponent<PlayerBehaviour>());
    }
}
