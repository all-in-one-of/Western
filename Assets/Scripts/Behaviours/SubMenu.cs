using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenu : MonoBehaviour
{
    public static SubMenu activeSubMenu;
    public List<Button> buttons;
    [System.NonSerialized] public int selectedButtonIndex;


    public void ChangeSelectedButtonIndex(int value)
    {
        if (value > 0 && selectedButtonIndex<buttons.Count-1)
        {
            selectedButtonIndex++;
        }else if(value<0 && selectedButtonIndex > 0)
        {
            selectedButtonIndex--;
        }
        UIManager.instance.PlaceSelectionButton();
    }

    public void Click()
    {
        buttons[selectedButtonIndex].onClick.Invoke();
    }
}
