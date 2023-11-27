using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadingScene 
{
    public static void LoadScene(int num)
    {
        SceneManager.LoadSceneAsync(num);
    }

    public static void LoadScene(string scenename)
    {
        SceneManager.LoadSceneAsync(scenename);
    }
}
