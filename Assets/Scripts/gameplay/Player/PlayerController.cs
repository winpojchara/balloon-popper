using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{

    private PlayerInput playerInput;
    PlayerAction playerAction;

    public event Action onInteractionPress;
    public event Action<int> onSwitchItemPress;
    public event Action onAttackPress;
    public event Action onJump;

    bool freeze;
    bool move;
    private bool isMoving;
    [SerializeField] bool jump;
    [SerializeField] bool performedJump = false;
    [SerializeField] bool isJump = false;
    bool interact;

    [Header("Contorl parametter")]
    [SerializeField] float walkSpeed;
    [Space(15)]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpDuration;
    float maxFallingSpeed = 15;
    Rigidbody2D rb;
    Collider2D hitbox;



    bool isGround()
    {
        bool isGrounded;
        Color raycastColor;
        RaycastHit2D raycastHit;
        float raycastDistance = 0.05f;
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        //ground check
        Vector3 box_origin = hitbox.bounds.center;
        box_origin.y = hitbox.bounds.min.y + (hitbox.bounds.extents.y / 4f);
        Vector3 box_size = hitbox.bounds.size;
        box_size.y = hitbox.bounds.size.y / 4f;
        raycastHit = Physics2D.BoxCast(box_origin, box_size, 0f, Vector2.down, raycastDistance, layerMask);

        if (raycastHit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Draw debug lines
        raycastColor = (isGrounded) ? Color.green : Color.red;
        Debug.DrawRay(box_origin + new Vector3(hitbox.bounds.extents.x, 0), Vector2.down * (hitbox.bounds.extents.y / 4f + raycastDistance), raycastColor);
        Debug.DrawRay(box_origin - new Vector3(hitbox.bounds.extents.x, 0), Vector2.down * (hitbox.bounds.extents.y / 4f + raycastDistance), raycastColor);
        Debug.DrawRay(box_origin - new Vector3(hitbox.bounds.extents.x, hitbox.bounds.extents.y / 4f + raycastDistance), Vector2.right * (hitbox.bounds.extents.x * 2), raycastColor);
        return isGrounded;
    }

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<Collider2D>();
        playerAction = new PlayerAction();
        playerAction.Player.Enable();
        playerAction.Player.Move.performed += Move_performed;
        playerAction.Player.Move.canceled += Move_performed;
        playerAction.Player.Jump.performed += Jump_performed;
        playerAction.Player.Jump.canceled += Jump_performed;
        playerAction.Player.Interact.started += Interact_performed;
        playerAction.Player.Interact.performed += Interact_performed;
        playerAction.Player.Interact.canceled += Interact_performed;
        playerAction.Player.Attack1.started += Attack_performed;
        playerAction.Player.SlotItem1.performed += SlotItem1_performed;
        playerAction.Player.SlotItem2.performed += SlotItem2_performed;
        playerAction.Player.SlotItem3.performed += SlotItem3_performed;
        playerAction.Player.SlotItem4.performed += SlotItem4_performed;
    }



    private void SlotItem1_performed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (onSwitchItemPress != null)
            {
                onSwitchItemPress(0);
            }
        }
    }
    private void SlotItem2_performed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (onSwitchItemPress != null)
            {
                onSwitchItemPress(1);
            }
        }
    }
    private void SlotItem3_performed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (onSwitchItemPress != null)
            {
                onSwitchItemPress(2);
            }
        }
    }

    private void SlotItem4_performed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (onSwitchItemPress != null)
            {
                onSwitchItemPress(3);
            }
        }
    }
    private void Attack_performed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // onAttackPress.Invoke();
            if (onAttackPress != null)
            {
                onAttackPress();
            }
        }
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (onInteractionPress != null)
            {
                onInteractionPress();
            }
        }
    }

    private void Move_performed(InputAction.CallbackContext context)
    {

    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jump = true;
        }

        if (context.canceled)
        {
            jump = false;

            if (performedJump)
            {
                StartFalling();
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        Move();

        if (jump)
        {
            if (isGround() && performedJump == false && isJump == false)
            {
                Jumping();
            }
        }
        else
        {
            if (isGround())
            {
                performedJump = false;
            }
        }
    }

    private void OnFreezeInputChange()
    {
        if (freeze)
        {
            if (playerAction.Player.enabled == true)
            {
                playerAction.Player.Disable();
            }
        }
        else
        {
            if (playerAction.Player.enabled == false)
            {
                playerAction.Player.Enable();
            }
        }
    }

    private void Jumping()
    {
        print("Jump");
        isJump = true;
        performedJump = true;
        if (onJump != null)
        {
            onJump();
        }
        PlayJumpSound();
        StartCoroutine(NormalJump());
    }

    private void Move()
    {
        move = GetMoveValue() != 0;

        //idle
        if (!move)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }


        float moveSpeed = walkSpeed;

        //Go right
        if (GetMoveValue() > 0)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        //Go left
        else if (GetMoveValue() < 0)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }

    void StartFalling()
    {
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    IEnumerator NormalJump()
    {
        float jumpTimer = 0;
        while (jumpTimer < jumpDuration)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimer += Time.deltaTime;

            yield return new WaitForEndOfFrame();
            if (!jump)
            {
                isJump = false;
                break;
            }
        }
        isJump = false;
        rb.velocity = new Vector2(rb.velocity.x, math.lerp(rb.velocity.y, 0, .5f));
    }


    public float GetMoveValue()
    {
        return playerAction.Player.Move.ReadValue<float>();
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public bool GetIsGround()
    {
        return isGround();
    }

    public bool GetIsMove()
    {
        return move;
    }

    public bool GetperformedJump()
    {
        return performedJump;
    }

    public void PlayJumpSound()
    {
        AudioPlaying.GetAudioPlaying.PlaySound("Jump");
    }

    public void SetFreezeTrue()
    {
        freeze = true;
        OnFreezeInputChange();
    }
        public void SetFreezeFalse()
    {
        freeze = false;
        OnFreezeInputChange();
    }
}
