using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AimingBow : MonoBehaviour
{
    [SerializeField] Transform bowTranform;
    [SerializeField] SpriteRenderer bowSprite;


    // Update is called once per frame
    void Update()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        AdjustAim(direction);
        FlipingBow(direction);
    }

    private void FlipingBow(Vector2 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        //positive
        if(rotation.y > 0)
        {
            bowSprite.flipY = false;
        }
        //negetive
        else if(rotation.y < 0)
        {
            bowSprite.flipY = true;
        }
    }

    private void AdjustAim(Vector2 direction)
    {
        //adjust aim
        transform.right = direction;
    }
}
