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

    }


}
