using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioPlaying : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public static AudioPlaying GetAudioPlaying;

    private void Awake()
    {
        if (GetAudioPlaying == null)
        {
            GetAudioPlaying = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound(string audioname)
    {
        SO_audio chip = Resources.Load<SO_audio>("Audio/"+ audioname);

        audioSource.PlayOneShot(chip.GetAudioClip());
        
    }
}
