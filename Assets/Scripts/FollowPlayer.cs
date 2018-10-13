using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public float offset = 10f;
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Stats>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = player.GetComponent<Transform>().position;
        GetComponent<Rigidbody>().transform.position = new Vector3(playerPos.x, playerPos.y + offset, GetComponent<Rigidbody>().transform.position.z);
    }
}
