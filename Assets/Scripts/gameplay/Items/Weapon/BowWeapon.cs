using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class BowWeapon : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootLocation;
    [SerializeField] float force;

    public event Action onAttack;

    public void Shoot()
    {
        GameObject obj = Instantiate(projectile, shootLocation.position, shootLocation.rotation);
        var arrow = obj.GetComponent<Rigidbody2D>();
        arrow.AddForce(obj.transform.right * force);
        if (onAttack != null)
        {
            onAttack();
        }
        PlaySound();
    }

    public void PlaySound()
    {
        if (AudioPlaying.GetAudioPlaying != null)
        {
            AudioPlaying.GetAudioPlaying.PlaySound("bow");
        }
    }
}
