using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField] List<Transform> spawnpoints;
    [SerializeField] List<GameObject> remainBalls;
    [SerializeField] Subwave wave;
    [SerializeField] bool spawnOnStart = true;


    // Start is called before the first frame update
    void Start()
    {
        if (spawnOnStart)
        {
            SpawnWave();
        }
    }

    public void CheckRemainBall()
    {
        foreach (GameObject ball in remainBalls)
        {
            if (ball == null)
            {
                RemoveBall(ball);
            }
        }
        if (remainBalls.Count == 0)
        {
            Subwave newWave = wave.GetNextwave;
            //if no new wave left, repeat same wave
            if (newWave == null)
            {
                newWave = wave;
            }
            SetnewWave(newWave);
            SpawnWave();
        }
    }

    private void SetnewWave(Subwave newWave)
    {
        wave = newWave;
    }

    public void SpawnWave()
    {
        foreach (GameObject ball in wave.balls)
        {
            int i = UnityEngine.Random.Range(0, spawnpoints.Count);
            GameObject obj = Instantiate(ball, spawnpoints[i].position, quaternion.identity);
            Rigidbody2D rg = obj.GetComponent<Rigidbody2D>();
            float randomForce = UnityEngine.Random.Range(100, 200);
            rg.AddForce(Vector2.right * randomForce);

            BouncingBall bouncingBall = obj.GetComponent<BouncingBall>();
            bouncingBall.OnDestoryAction += CheckRemainBall;
        }
    }

    public void AddBall(GameObject gameObject)
    {
        remainBalls.Add(gameObject);
    }

    public void RemoveBall(GameObject gameObject)
    {
        remainBalls.Remove(gameObject);
    }
    
    public void SetspawnOnStart(bool value)
    {
        spawnOnStart = value;
    }
}
