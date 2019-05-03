using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject canvasPrefab;
    

    [System.NonSerialized] public Menu menu;

    public void SpawnCanvas()
    {
        menu=Instantiate(canvasPrefab).GetComponent<Menu>();
        SubMenu.activeSubMenu = menu.MainMenu;
        PlaceSelectionButton();
    }

    public void PlaceSelectionButton()
    {
        RectTransform selectedButtonTransform = SubMenu.activeSubMenu.buttons[SubMenu.activeSubMenu.selectedButtonIndex].GetComponent<RectTransform>();
        menu.selectionButtonTransform.anchoredPosition3D = selectedButtonTransform.anchoredPosition3D + new Vector3(selectedButtonTransform.rect.size.x * selectedButtonTransform.localScale.x / 2 + 50, 0, 0);

    }

    public void ShowUpgradeMenu()
    {
        //menu.upgradeMenu.setActive(true);
        menu.upgradeMenu.gameObject.SetActive(true);
    }

    [MenuItem("Western/NextArena")]
    public static void NextArena()
    {
        if (LevelManager.instance.currentArena < 4)
        {
            LevelManager.instance.currentArena++;
            LevelManager.instance.LoadArena();
        }
    }


    [MenuItem("Western/NextLevel")]
    public static void NextLevel()
    {
        LevelManager.instance.currentArena = 0;
        LevelManager.instance.DestroyLevel();
        if (LevelManager.instance.currentLevel < LevelManager.instance.levels.Count-1)
        {
            LevelManager.instance.currentLevel++;
        }
        else
        {
            LevelManager.instance.currentLevel = 0;
        }
        LevelManager.instance.LoadLevel(LevelManager.instance.LoadArena);
    }
    
}
