using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{
    public void PlayButtonClick()
    {
        LoadingScene.LoadScene(1);
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
