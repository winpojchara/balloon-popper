using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] GameObject gameoverScreen;
    [SerializeField] Timer timer;
    [SerializeField] PlayerController player;
    public event Action onGameOver;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        timer.onTimeUpTrigger += GameOver;
        onGameOver += player.SetFreezeTrue;
        
        PauseScene pauseScene = FindObjectOfType<PauseScene>();
        if(pauseScene != null)
        {
            onGameOver += pauseScene.SetFreezeTrue;
        }
    }
    
    public void GameOver()
    {
        gameoverScreen.SetActive(true);
        if(onGameOver != null)
        {
            onGameOver();
        }
    }

    public void ReturnToTitleScene()
    {
        LoadingScene.LoadScene(0);
    }
}
