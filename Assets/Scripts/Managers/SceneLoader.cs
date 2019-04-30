using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    private static UnityAction callback;
    



    public static void LoadScene(string sceneName,UnityAction callback=null)
    {
        SceneLoader.callback = callback;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (callback != null)
        {
            callback.Invoke();
        }
        
    }


}
