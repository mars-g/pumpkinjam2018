using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDControls : MonoBehaviour {


    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHor = Input.GetAxis("Horizontal") * moveSpeed;
        float moveVert = rb.velocity.y;
        if (Input.GetKeyDown("space")) {
            moveVert = jumpSpeed;
        }
        rb.velocity = new Vector2(moveHor, moveVert);
	}
}
