using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    [SerializeField] string groundLayerMask;
    [SerializeField] GameObject childObject;
    [SerializeField] float spawnForce;
    [SerializeField] float bounceForce;
    Rigidbody2D rg;
    public event Action OnDestoryAction;
    int earnScore = 10;

    private void Awake()
    {
        SpawnBall spawnBall = FindObjectOfType<SpawnBall>();
        if (spawnBall != null)
        {
            OnDestoryAction += spawnBall.CheckRemainBall;
            spawnBall.AddBall(gameObject);
        }
    }
    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        ScoreSystem scoreSystem = FindObjectOfType<ScoreSystem>();
        if (scoreSystem != null)
        {
            OnDestoryAction += () => scoreSystem.AddScore(earnScore);
        }

        rg.AddForce(Vector2.up * 300);

        Invoke("SetColliderOn", 0.2f);

    }

    void SetColliderOn()
    {
        Collider2D coll = GetComponent<Collider2D>();
        coll.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(groundLayerMask))
        {
            rg.velocity = new Vector2(rg.velocity.x, 0);
            rg.AddForce(Vector2.up * bounceForce);

        }
    }

    public void OnHit()
    {
        if (childObject != null)
        {
            SplitTwoChildObject();
        }

        SpawnBall spawnBall = FindObjectOfType<SpawnBall>();
        if (spawnBall != null) spawnBall.RemoveBall(gameObject);
        PlayPopSound();
        Destroy(gameObject);

        if (OnDestoryAction != null)
        {
            OnDestoryAction();
        }
    }

    public void SplitTwoChildObject()
    {
        GameObject[] objects = new GameObject[2];
        for (int i = 0; i <= 1; i++)
        {
            objects[i] = Instantiate(childObject, transform.position, quaternion.identity);
        }
        objects[0].GetComponent<Rigidbody2D>().AddForce(Vector2.left * spawnForce);
        objects[1].GetComponent<Rigidbody2D>().AddForce(Vector2.right * spawnForce);
    }

    public void PlayPopSound()
    {
        AudioPlaying.GetAudioPlaying.PlaySound("Pop");
    }
}
