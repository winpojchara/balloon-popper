using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    string TAG_enemy = "Enemy";
    private Collider[] childrenColliders;

    void Start()
    {
        InvokeRepeating("test",1,1);
    }

    void test()
    {
        AudioPlaying.GetAudioPlaying.PlaySound("Jump");
    }
}
