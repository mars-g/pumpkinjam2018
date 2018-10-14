using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAfterBoss : MonoBehaviour {

    private GameObject boss;
    private bool doneyet = false;
	// Use this for initialization
	void Start () {
        boss = FindObjectOfType<Boss1>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (boss.GetComponent<EnemyHealth>().isDying())
        {
            if (!doneyet)
            {
                for(int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                doneyet = true;
            }
        }
	}
}
