using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] private SpriteRenderer sprite;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float velocityX = playerController.GetMoveValue();

        //faceRight
        if(velocityX > 0)
        {
            sprite.flipX = false;
        }
        //faceleft
        else if(velocityX < 0)
        {
            sprite.flipX = true;
        }
    }
}
