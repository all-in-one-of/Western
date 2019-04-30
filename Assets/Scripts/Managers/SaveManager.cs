using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public List<PlayerData> playerDatas=new List<PlayerData>();


    public void LoadPlayerData(int playerNumber)
    {
        //on récup la save du joueur correspondant
        PlayerData playerData= JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("PlayerData" + playerNumber));
        if (playerData != null)
        {
            playerData.fromFile = true;
        }
        else
        {
            playerData = new PlayerData();
            playerData.fromFile = false;
        }

        //on sauvegarde les données en mémoire, pour pouvoir les utiliser au moment du spawn du joueur
        playerDatas.Add(playerData);  
    }





    public void SavePlayerData(int playerNumber)
    {
        if (playerNumber < playerDatas.Count && playerDatas[playerNumber] == null)
        {
            playerDatas[playerNumber].arenaIndexes = LevelManager.instance.selectedArenaIndexes;
        }

        PlayerBehaviour player = PlayerManager.instance.players[playerNumber];

        //on récupère toutes les valeurs 
        playerDatas[playerNumber].credits = player.credits;
        playerDatas[playerNumber].currentLevel = LevelManager.instance.currentLevel;
        playerDatas[playerNumber].currentArena = LevelManager.instance.currentArena;
        playerDatas[playerNumber].maxAmmoUpgradeLevel = player.maxAmmoUpgradeLevel;
        playerDatas[playerNumber].maxHealthUpgradeLevel = player.maxHealthUpgradeLevel;
        playerDatas[playerNumber].maxStaminaUpgradeLevel = player.maxStaminaUpgradeLevel;
        playerDatas[playerNumber].speedUpgradeLevel = player.speedUpgradeLevel;
        playerDatas[playerNumber].startAmmoUpgradeLevel = player.startAmmoUpgradeLevel;
        

        //on save 
        PlayerPrefs.SetString("PlayerData"+playerNumber,JsonUtility.ToJson(playerDatas[playerNumber]));
    }
}
