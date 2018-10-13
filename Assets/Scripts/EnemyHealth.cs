using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int Health = 10;
    private int curHealth;

	// Use this for initialization
	void OnEnable ()
    {
        curHealth = Health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerSlide")
        {
            curHealth -= 2;
        }
        else if (collision.gameObject.name == "PlayerSwing")
        {
            curHealth -= 3;
        }

        if (curHealth < 0)
            curHealth = 0;

        if (curHealth <= 0)
            Die();
    }
}
