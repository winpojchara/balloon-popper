using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScene : MonoBehaviour
{
    bool freeze;
    bool pause = false;

    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject player;
    public event Action OnResume;
    public event Action OnPause;

    void Start()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        OnPause += playerController.SetFreezeTrue;
        OnResume += playerController.SetFreezeFalse;
    }

    private void Update()
    {
        if (freeze) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        pause = false;
        if (OnResume != null)
        {
            OnResume();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
        pause = true;
        if (OnPause != null)
        {
            OnPause();
        }
    }

    public void ReturnToTitleScene()
    {
        Time.timeScale = 1;
        LoadingScene.LoadScene(0);
    }

    public void SetFreezeTrue()
    {
        freeze = true;
    }
    public void SetFreezefalse()
    {
        freeze = false;
    }
}
