using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] List<TextMeshProUGUI> showTexts;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        DisplayText();
    }
    public void AddScore(int num)
    {
        score += num;
        DisplayText();
    }
    void DisplayText()
    {
        foreach (TextMeshProUGUI showText in showTexts)
        {
            showText.text = score.ToString("0000");
        }
    }

}
