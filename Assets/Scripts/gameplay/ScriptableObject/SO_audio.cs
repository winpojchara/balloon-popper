using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new_audio", menuName = "MiniTrooper2D/SO_audio", order = 0)]
public class SO_audio : ScriptableObject
{

    [SerializeField]List<AudioClip> audios = new List<AudioClip>();

    public AudioClip GetAudioClip()
    {
        if(audios.Count == 1)
        {
            return audios[0];
        }

        int i = UnityEngine.Random.Range(0, audios.Count);
        return audios[i];
    }

}