using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int Health = 10;
    private int curHealth;
    private bool dying = false;
	// Use this for initialization
	void OnEnable ()
    {
        //GetComponent<CapsuleCollider>().enabled = true;
        curHealth = Health;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Die()
    {
        StartCoroutine("Dying");
        //gameObject.SetActive(false);
    }

    public bool isDying() { return dying; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerSlide")
        {
            curHealth -= 2;
        }
        else if (collision.gameObject.name == "PlayerDive")
        {
            curHealth -= 3;
        }
        else if (collision.gameObject.name == "PlayerSwing")
        {
            curHealth -= 4;
        }
        else if (collision.gameObject.tag == "ExplosionBox")
        {
            curHealth -= 5;
        }

        if (curHealth < 0)
            curHealth = 0;

        if (curHealth <= 0)
            Die();
    }

    IEnumerator Dying()
    {
        //GetComponent<CapsuleCollider>().enabled = false;
        Color temp = Color.white;
        temp.a = 1;
        while (temp.a >= .1)
        {
            temp.a -= .1f;
            GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForFixedUpdate();
        }
        gameObject.SetActive(false);
    }

}
