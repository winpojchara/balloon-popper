using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    PlayerController playerController;
    Animator anim;
    [SerializeField] private string parametter_VELO_Y = "velocityY";
    [SerializeField] private string parametter_ISGROUND = "isGround";
    [SerializeField] private string parametter_ISMOVE = "isMove";

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        if (playerController == null)
        {
            enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        float velocityY = playerController.GetVelocity().y;
        bool isGround = playerController.GetIsGround();
        bool isMove = playerController.GetIsMove();

        anim.SetFloat(parametter_VELO_Y, velocityY);
        anim.SetBool(parametter_ISGROUND, isGround);
        anim.SetBool(parametter_ISMOVE, isMove);
    }

}
