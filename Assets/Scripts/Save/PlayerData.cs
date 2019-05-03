using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int credits;
    public int currentLevel;
    public int currentArena;
    public List<int> arenaIndexes;

    public int maxHealthUpgradeLevel;
    public int speedUpgradeLevel;
    public int maxAmmoUpgradeLevel;
    public int startAmmoUpgradeLevel;
    public int maxStaminaUpgradeLevel;




    [System.NonSerialized] public bool fromFile;
}
