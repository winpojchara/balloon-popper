using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private string TAG_enemy = "Enemy";


    void Start()
    {
        Destroy(gameObject, 1);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(TAG_enemy))
        {
            BouncingBall ball = other.GetComponent<BouncingBall>();
            if(ball == null)return;
            print("Hit the" + ball.gameObject.name);
            ball.OnHit();
        }
    }
}
