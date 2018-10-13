﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDControls : MonoBehaviour {


    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;
    public float jumpTime = 2f;
    public float gravityScalar = 1f;
    public float wallPush = 3f;
    public float pushModifier = 0f;
    public float wallJumpTime = 1f;

    //private bool grounded;
    private float wallDirection;
    //jump state stores the number of jumps character can use before hitting the ground
    public int jumpState = 2;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHor = Input.GetAxis("Horizontal") * moveSpeed;
        float moveVert = rb.velocity.y;
        if (wallDirection != 0 && Input.GetButtonDown("Jump") && jumpState != 2)
        {
            moveVert = jumpSpeed;
            StartCoroutine(WallJump());
        }
        else if (jumpState > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpState--;
                moveVert = jumpSpeed;
            }
        }

        
        
        
        rb.velocity = new Vector2(moveHor + pushModifier, moveVert);
        if (jumpState < 2 && Mathf.Abs(rb.velocity.y) < 0.5)
        {
            StartCoroutine(AdvancedJumpPhysics());
        }

        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else if(rb.velocity.x > 0)
        {
            sr.flipX = false;
        }

        anim.SetBool("Running", (moveHor != 0) && (jumpState == 2));
        anim.SetBool("Jumping", (jumpState < 2) && (Input.GetButton("Jump")) && pushModifier == 0);
        anim.SetBool("Walljumping", (wallDirection != 0 && Input.GetButton("Jump") && jumpState != 2) && pushModifier !=0);
        anim.SetBool("Freefalling", rb.velocity.y<=0 && jumpState<2);

    }

    private IEnumerator AdvancedJumpPhysics() {
        int currentJumpState = jumpState;
        for (float t = 0; t < jumpTime; t += Time.deltaTime) {
            if (jumpState < currentJumpState || jumpState == 2) {
                break;
            }
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravityScalar);
            yield return null;
        }
        yield return null;
    }

    private IEnumerator WallJump()
    {
        float wallDir = wallDirection;
        for (float t = 0; t < wallJumpTime; t += Time.deltaTime)
        {
            pushModifier = Mathf.Abs(wallPush - t * 2) * wallDir;
            yield return null;
        }
        pushModifier = 0;
        yield return null;
    }

    //checks for collision with ground to refresh jump ability
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            //grounded = true;
            jumpState = 2;
            wallDirection = 0;
        }
        

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Wall>() && GetComponent<Transform>().position.y < collision.gameObject.transform.position.y + collision.gameObject.transform.localScale.y / 2)
        {
            
            wallDirection = Mathf.Sign(gameObject.transform.position.x - collision.gameObject.transform.position.x);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.GetComponent<Ground>()) && jumpState == 2)
        {
            //grounded = false;
            jumpState--;
        }
        else if (collision.gameObject.GetComponent<Wall>()) {
            wallDirection = 0;
        }
    }
}
