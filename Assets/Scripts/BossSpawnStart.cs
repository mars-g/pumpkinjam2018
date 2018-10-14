using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnStart : MonoBehaviour {

    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject[] enemies;

    private bool hasSpawned;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //check if all enemies are dead
        bool trueFam = true;
		foreach (GameObject enemy in enemies) {
            if (enemy.activeSelf)
            {
                trueFam = false;
            }
        }
        if (trueFam) {
            rightWall.SetActive(false);
        }
	}

    public void resetEnemiesAndWalls() {
        
        leftWall.SetActive(false);
        rightWall.SetActive(false);
        hasSpawned = false;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyHealth>().RespawnHidden();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hasSpawned) {
            Spawn();
            hasSpawned = true;
        }
    }

    private void Spawn()
    {
        leftWall.SetActive(true);
        rightWall.SetActive(true);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(true);
        }
    }
}
