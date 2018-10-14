using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBoss : MonoBehaviour {

    private GameObject boss;
    public bool disappearafterboss;

	// Use this for initialization
	void Start ()
    {
        boss = FindObjectOfType<Boss1>().gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (boss.GetComponent<EnemyHealth>().isDying())
        {
            if (disappearafterboss)
            {
                gameObject.SetActive(false);
            }
        }	
	}
}
