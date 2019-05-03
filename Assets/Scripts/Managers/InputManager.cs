using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{

    private bool on = false;

    public void Init()
    {
        Enable(true);
    }


    public void Enable(bool on)
    {
        this.on = on;
    }


    private void Update()
    {
        if (!on) { return; }

        //add inputs here
        if (Input.GetAxisRaw("Vertical_1")>0)
        {
            if (SubMenu.activeSubMenu != null)
            {
                SubMenu.activeSubMenu.ChangeSelectedButtonIndex(-1);
            }
            
        }

        if (Input.GetAxisRaw("Vertical_1") < 0)
        {
            if (SubMenu.activeSubMenu != null)
            {
                SubMenu.activeSubMenu.ChangeSelectedButtonIndex(1);
            }
        }


        if (Input.GetButtonDown("A_Button_1"))
        {
            if (SubMenu.activeSubMenu != null)
            {
                SubMenu.activeSubMenu.Click();
            }
        }



    }


}
