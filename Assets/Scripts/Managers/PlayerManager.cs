using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [System.NonSerialized] public List<PlayerBehaviour> players;
    public GameObject playerPrefab;

    public void SpawnPlayer(int playerNumber)
    {
        if (playerNumber > PlayerSpawn.spawns.Count)
        {
            Debug.Log("no spawn point available");
            return;
        }
        players.Add(Instantiate(playerPrefab, PlayerSpawn.spawns[playerNumber].self.position, Quaternion.identity).GetComponent<PlayerBehaviour>());
    }
}
