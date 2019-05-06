using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static List<Obstacle> obstacles;

    private void Awake()
    {
        if (obstacles == null)
        {
            obstacles = new List<Obstacle>();
        }
        obstacles.Add(this);
    }

    private void OnDestroy()
    {
        if (obstacles != null)
        {
            obstacles.Remove(this);
        }
    }
}
