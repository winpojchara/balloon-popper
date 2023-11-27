using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordWeapon : MonoBehaviour
{
    string TAG_enemy = "Enemy";
    float startPosX;
    float collideroffsetX;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Collider2D coll;
    PlayerController playerController;
    Animator anim;
    

    [Header("Animator parameter")]
    [SerializeField] string ANIM_attack = "attack";

    [Header("attack rate")]
    [SerializeField] float fireRate = 2.2f;
    float GetfireRateGap => 1 / fireRate;
    float fireRatetimer;
    bool hit;

    public event Action<bool> onAttack;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        startPosX = transform.localPosition.x;
        collideroffsetX = coll.offset.x;
    }

    void Start()
    {

        playerController = GetComponentInParent<PlayerController>();
        if (playerController == null)
        {
            Debug.Log("disable:" + this.ToString());
            enabled = false;
        }
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        FlipSword();
        fireRatetimer = Mathf.MoveTowards(fireRatetimer, 0, Time.deltaTime);
    }

    private void FlipSword()
    {
        float velocityX = playerController.GetMoveValue();
        float newCollOffsetX = collideroffsetX;
        float newCollOffsetY = coll.offset.y;
        //faceRight
        if (velocityX > 0)
        {
            transform.localPosition = new Vector3(startPosX, 0, 0);
            coll.offset = new Vector2(newCollOffsetX, newCollOffsetY);
            sprite.flipX = false;
        }
        //faceleft
        else if (velocityX < 0)
        {
            transform.localPosition = new Vector3(-startPosX, 0, 0);
            coll.offset = new Vector2(-newCollOffsetX, newCollOffsetY);
            sprite.flipX = true;
        }
    }

    public void Attack()
    {
        if (fireRatetimer > 0) return;
        anim.SetTrigger(ANIM_attack);
        PlaySound();
        fireRatetimer = GetfireRateGap;
    }
    //sword event
    public IEnumerator OpenAttackWindowFrame()
    {
        InventoryHolder inventoryHolder = FindObjectOfType<InventoryHolder>();
        try {inventoryHolder.SetBusy(true);} catch {}
        hit = false;
        coll.enabled = true;
        yield return new WaitForSeconds(.15f);
        coll.enabled = false;
        if(onAttack != null)
        {
            onAttack(hit);
        }
        try {inventoryHolder.SetBusy(false);} catch {}
    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TAG_enemy))
        {
            BouncingBall ball = other.GetComponent<BouncingBall>();
            if (ball == null) return;
            print("Hit the" + ball.gameObject.name);
            ball.OnHit();
            hit = true;
        }
    }

    public void PlaySound()
    {
        AudioPlaying.GetAudioPlaying.PlaySound("Sword");
    }
}
