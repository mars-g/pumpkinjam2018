using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour {
    Vector3 spawnPoint;
    public float moveX = 1.5f;
	// Use this for initialization
	void Start () {
        spawnPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = new Vector3(moveX, 0f, 0f);
	}
}
