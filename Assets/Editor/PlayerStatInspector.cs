using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStats))]
public class PlayerStatInspector : Editor
{

    private PlayerStats playerStats;

    private void OnEnable()
    {
        playerStats = target as PlayerStats;
    }


    private void OnDisable()
    {
        playerStats = null;
    }


}
