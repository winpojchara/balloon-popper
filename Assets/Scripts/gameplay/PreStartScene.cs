using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreStartScene : MonoBehaviour
{
    [Header("Tutorial screen")]
    [SerializeField] GameObject tutorialScene;
    [SerializeField] List<GameObject> tutorialImage;
    [SerializeField] List<GameObject> tutorialText;
    [SerializeField] TextMeshProUGUI textDisplayNumPage;
    [SerializeField]int currentPage;

    public event Action OnGameStart;

    public void Start()
    {
        tutorialScene.SetActive(true);

        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.SetFreezeTrue();
            OnGameStart += timer.SetFreezeFalse;
        }

        PauseScene pauseScene = FindObjectOfType<PauseScene>();
        if (pauseScene != null)
        {
            pauseScene.SetFreezeTrue();
            OnGameStart += pauseScene.SetFreezefalse;
        }

        SpawnBall spawnBall = FindObjectOfType<SpawnBall>();
        if (spawnBall != null)
        {
            spawnBall.SetspawnOnStart(false);
            OnGameStart += spawnBall.SpawnWave;
        }

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if(playerController != null)
        {
            playerController.SetFreezeTrue();
            OnGameStart += playerController.SetFreezeFalse;
        }

    }

    public void GoNextPage()
    {
        if (currentPage >= tutorialImage.Count -1) return;
        CloseCurrentPage();
        currentPage++;
        //open new page
        OpenCurrentPage();
        DisplayPageNumber();
    }

    public void GoPreviousPage()
    {
        if (currentPage <= 0) return;
        CloseCurrentPage();
        currentPage--;
        //open new page
        OpenCurrentPage();
        DisplayPageNumber();
    }

    public void CloseCurrentPage()
    {
        tutorialImage[currentPage].SetActive(false);
        tutorialText[currentPage].SetActive(false);
    }

    public void OpenCurrentPage()
    {
        tutorialImage[currentPage].SetActive(true);
        tutorialText[currentPage].SetActive(true);
    }

    public void ReadyTheGame()
    {
        tutorialScene.SetActive(false);
        if (OnGameStart != null)
        {
            OnGameStart();
        }
    }

    public void DisplayPageNumber()
    {
        int curpage = currentPage+1;
        int totalPage = tutorialImage.Count;
        textDisplayNumPage.text = "Page "+ curpage.ToString() + "/" + totalPage.ToString();
    }

    public void PlayClickSound()
    {
        AudioPlaying.GetAudioPlaying.PlaySound("Click");
    }
}
