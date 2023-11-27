using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] bool freeze = false;
    [SerializeField] float time;
    public event Action onTimeUpTrigger;
    [SerializeField] TextMeshProUGUI text;
    

    // Update is called once per frame
    void Update()
    {
        if(freeze) return;
        time = Mathf.MoveTowards(time,0, Time.deltaTime);
        DisplayRemainingTime();

        if(time <= 0)
        {     
            if(onTimeUpTrigger != null)
            {
                onTimeUpTrigger();
            }
            SetFreezeTrue();
        }
    }

    private void DisplayRemainingTime()
    {
        text.text = time.ToString("0");
    }

    public void SetFreezeTrue()
    {
        freeze = true;
    }

    public void SetFreezeFalse()
    {
        freeze = false;
    }
}
