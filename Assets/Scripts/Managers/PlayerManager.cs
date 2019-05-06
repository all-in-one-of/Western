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
            players[players.Count - 1].GetComponent<HealthBehaviour>().healthBar = UIManager.instance.menu.player1lifebar;
            PlayerGameplayValues playerGameplayValues = players[players.Count - 1].GetComponent<PlayerGameplayValues>();
            playerGameplayValues.maxHealth = playerGameplayValues.health = playerGameplayValues.scriptableStats.maxHealth.startValue + playerGameplayValues.scriptableStats.maxHealth.upgradeValue * GameManager.instance.p1healthUpgradeLevel;
            playerGameplayValues.magazineSize = playerGameplayValues.scriptableStats.maxAmmo.startValue + playerGameplayValues.scriptableStats.maxAmmo.upgradeValue * GameManager.instance.p1magazineSizeUpgradeLevel;
            playerGameplayValues.maxStamina = playerGameplayValues.scriptableStats.maxStamina.startValue + playerGameplayValues.scriptableStats.maxStamina.upgradeValue * GameManager.instance.p1maxStaminaUpgradeLevel;
            playerGameplayValues.speed = playerGameplayValues.scriptableStats.speed.startValue + playerGameplayValues.scriptableStats.speed.upgradeValue * GameManager.instance.p1speedUpgradeLevel;
            //playerGameplayValues.a = playerGameplayValues.health = playerGameplayValues.scriptableStats.maxHealth.startValue + playerGameplayValues.scriptableStats.maxHealth.upgradeValue * GameManager.instance.p1healthUpgradeLevel;

            GameManagerValues.instance.Player1 = players[0].GetComponent<ControllerBehaviour>();
        }
        else
        {
            players[players.Count - 1].animator.runtimeAnimatorController = player2AnimatorController;
            players[players.Count - 1].GetComponent<HealthBehaviour>().healthBar = UIManager.instance.menu.player2lifebar;

            PlayerGameplayValues playerGameplayValues = players[players.Count - 1].GetComponent<PlayerGameplayValues>();
            playerGameplayValues.maxHealth = playerGameplayValues.health = playerGameplayValues.scriptableStats.maxHealth.startValue + playerGameplayValues.scriptableStats.maxHealth.upgradeValue * GameManager.instance.p2healthUpgradeLevel;
            playerGameplayValues.magazineSize = playerGameplayValues.scriptableStats.maxAmmo.startValue + playerGameplayValues.scriptableStats.maxAmmo.upgradeValue * GameManager.instance.p2magazineSizeUpgradeLevel;
            playerGameplayValues.maxStamina = playerGameplayValues.scriptableStats.maxStamina.startValue + playerGameplayValues.scriptableStats.maxStamina.upgradeValue * GameManager.instance.p2maxStaminaUpgradeLevel;
            playerGameplayValues.speed = playerGameplayValues.scriptableStats.speed.startValue + playerGameplayValues.scriptableStats.speed.upgradeValue * GameManager.instance.p2speedUpgradeLevel;


            GameManagerValues.instance.Player2 = players[players.Count - 1].GetComponent<ControllerBehaviour>();
        }

        players[players.Count - 1].gameObject.name = "Player" + playerNumber;
        players[players.Count - 1].GetComponent<ControllerData>().playerID ="_"+(playerNumber + 1);
        PlayerData playerData = SaveManager.instance.playerDatas[playerNumber];
        

        players[playerNumber].credits = playerData.credits;
        players[playerNumber].maxAmmoUpgradeLevel = playerData.maxAmmoUpgradeLevel;
        players[playerNumber].maxHealthUpgradeLevel = playerData.maxHealthUpgradeLevel;
        players[playerNumber].maxStaminaUpgradeLevel = playerData.maxStaminaUpgradeLevel;
        players[playerNumber].speedUpgradeLevel = playerData.speedUpgradeLevel;
        players[playerNumber].startAmmoUpgradeLevel = playerData.startAmmoUpgradeLevel;
    }
}
