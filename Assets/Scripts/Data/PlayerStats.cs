using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerStats",menuName ="Western/PlayerStats",order =1)]
public class PlayerStats : ScriptableObject
{
    public PlayerStat maxHealth;
    public PlayerStat speed;
    public PlayerStat maxAmmo;
    public PlayerStat startAmmo;
    public PlayerStat maxStamina;
}
