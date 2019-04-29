using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public PlayerData playerData;


    public void LoadData()
    {
        playerData=JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("data"));

    }





    public void SaveData()
    {

        PlayerPrefs.SetString("data",JsonUtility.ToJson(playerData));
    }
}
