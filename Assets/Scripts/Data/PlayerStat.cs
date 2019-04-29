using UnityEngine;


[System.Serializable]
public class PlayerStat
{
    [Header("Default")]
    public float startValue;
    [Space()]
    [Header("Upgrades")]
    public AnimationCurve priceOverUpgradeLevel;
    public float upgradeValue;
    [HideInInspector]
    public int upgradeLevel;
    
    

    
}
