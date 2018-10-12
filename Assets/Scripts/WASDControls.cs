using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDControls : MonoBehaviour {


    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;
    public float jumpTime = 2f;
    public float gravityScalar = 1f;
    //jump state stores the number of jumps character can use before hitting the ground
    public int jumpState = 2;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHor = Input.GetAxis("Horizontal") * moveSpeed;
        float moveVert = rb.velocity.y;
        if (jumpState > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpState--;
                moveVert = jumpSpeed;
            }
        }
        
        
        rb.velocity = new Vector2(moveHor, moveVert);
        if (jumpState < 2 && Mathf.Abs(rb.velocity.y) < 0.5)
        {
            StartCoroutine(AdvancedJumpPhysics());
        }
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

    //checks for collision with ground to refresh jump ability
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            jumpState = 2;
        }

    }
}
