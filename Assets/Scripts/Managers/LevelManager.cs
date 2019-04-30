using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels;

    

    [System.NonSerialized] public int currentLevel;

    public void GenerateMap()
    {
        SceneLoader.AddArenas(levels[currentLevel].arenas, levels[currentLevel].arenasToSpawnCount);

    }
}
