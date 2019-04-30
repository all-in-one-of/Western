using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableSurface : MonoBehaviour
{
    public static List<WalkableSurface> surfaces;

    private void Awake()
    {
        if (surfaces == null)
        {
            surfaces = new List<WalkableSurface>();
        }
        surfaces.Add(this);
    }
}
