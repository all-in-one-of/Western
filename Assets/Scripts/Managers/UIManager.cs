using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject canvasPrefab;
    public Menu menu;

    public void SpawnCanvas()
    {
        menu=Instantiate(canvasPrefab).GetComponent<Menu>();
    }
}
