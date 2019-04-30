using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="Level",menuName ="Western/Level",order =1)]
public class Level : ScriptableObject
{
    
    public List<Arena> arenas;
    public int arenasToSpawnCount;
}
