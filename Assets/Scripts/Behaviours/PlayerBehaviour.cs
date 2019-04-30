using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //ici ce sont les valeurs au runtime, qu'on actualise directement quand on achète une upgrade
    [System.NonSerialized] public int maxHealthUpgradeLevel;
    [System.NonSerialized] public int speedUpgradeLevel;
    [System.NonSerialized] public int maxAmmoUpgradeLevel;
    [System.NonSerialized] public int startAmmoUpgradeLevel;
    [System.NonSerialized] public int maxStaminaUpgradeLevel;

    [System.NonSerialized] public int credits;
}
