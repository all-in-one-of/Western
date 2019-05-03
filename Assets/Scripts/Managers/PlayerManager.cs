using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [System.NonSerialized] public List<PlayerBehaviour> players;
    public GameObject playerPrefab;

    public AnimatorController player1AnimatorController;
    public AnimatorController player2AnimatorController;

    public void SpawnPlayer(int playerNumber)
    {
        if (playerNumber > PlayerSpawn.levelSpawns[LevelManager.instance.currentArena].Count)
        {
            Debug.Log("no spawn point available");
            return;
        }
        if (players == null)
        {
            players = new List<PlayerBehaviour>();
        }
        print("spawning player" + playerNumber);
        players.Add(Instantiate(playerPrefab, PlayerSpawn.levelSpawns[LevelManager.instance.currentArena][playerNumber].self.position, PlayerSpawn.levelSpawns[LevelManager.instance.currentArena][playerNumber].self.rotation).GetComponent<PlayerBehaviour>());
        if (playerNumber == 0)
        {
            players[players.Count - 1].animator.runtimeAnimatorController = player1AnimatorController;
        }
        else
        {
            players[players.Count - 1].animator.runtimeAnimatorController = player2AnimatorController;
            Destroy(players[players.Count - 1].GetComponent<AudioListener>());
        }

        players[players.Count - 1].gameObject.name = "Player" + playerNumber;
        players[players.Count - 1].GetComponent<ControllerData>().playerID ="_"+(playerNumber + 1);
        PlayerData playerData = SaveManager.instance.playerDatas[playerNumber];
        UIManager.instance.menu.gameManagerValues.Player1 = players[players.Count - 1].GetComponent<ControllerBehaviour>();
        players[playerNumber].credits = playerData.credits;
        players[playerNumber].maxAmmoUpgradeLevel = playerData.maxAmmoUpgradeLevel;
        players[playerNumber].maxHealthUpgradeLevel = playerData.maxHealthUpgradeLevel;
        players[playerNumber].maxStaminaUpgradeLevel = playerData.maxStaminaUpgradeLevel;
        players[playerNumber].speedUpgradeLevel = playerData.speedUpgradeLevel;
        players[playerNumber].startAmmoUpgradeLevel = playerData.startAmmoUpgradeLevel;
    }
}
