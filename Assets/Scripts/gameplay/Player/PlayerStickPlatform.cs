using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform currentPlatform;
    private string movingPlatform_TAG = "MovePlatform";

    PlayerController playerController;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("enter");
        if (playerController.GetIsGround() == false) return;
        if (rb.velocity.y != 0) return;
        if (currentPlatform != null)
        {
            return;
        }

        if (collision.gameObject.CompareTag(movingPlatform_TAG))
        {

            currentPlatform = collision.transform;
            transform.parent = currentPlatform; // Set the platform as the parent
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //print("stay");
        if (playerController.GetIsGround() == false) return;
        if (rb.velocity.y != 0) return;
        if (currentPlatform != null)
        {
            return;
        }

        if (collision.gameObject.CompareTag(movingPlatform_TAG))
        {

            currentPlatform = collision.transform;
            transform.parent = currentPlatform; // Set the platform as the parent
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        print("exit");
        if (currentPlatform == null)
        {
            return;
        }
        if (collision.gameObject.CompareTag(movingPlatform_TAG))
        {

            transform.parent = null; // Remove the platform as the parent
            currentPlatform = null;
        }
    }
}
