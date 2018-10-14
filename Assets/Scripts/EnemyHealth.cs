using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int Health = 10;
    private int curHealth;
    private bool dying = false;
    public AudioClip hurtsound;
    public bool playhurtsound = false;

	// Use this for initialization
	void OnEnable ()
    {
        //GetComponent<CapsuleCollider>().enabled = true;
        curHealth = Health;
	}
	
    void PlayHurtSound()
    {
        if (playhurtsound)
        {
            if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();

            GetComponent<AudioSource>().clip = hurtsound;
            GetComponent<AudioSource>().Play();
        }
    }

	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Die()
    {
        dying = true;

        if (name == "Boss1")
        {
            //StartCoroutine("BossDying");
            GetComponent<Rigidbody2D>().gravityScale = 2.1f;
            GetComponent<Rigidbody2D>().freezeRotation = false;
            GetComponent<Rigidbody2D>().mass = 1f;

            Color temp = Color.red;
            temp.a = 1;
            GetComponent<SpriteRenderer>().color = temp;

        }
        else
            StartCoroutine("Dying");
        
    }

    public bool isDying() { return dying; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerSlide")
        {
            PlayHurtSound();
            curHealth -= 2;
        }
        else if (collision.gameObject.name == "PlayerDive")
        {
            PlayHurtSound();

            curHealth -= 3;
        }
        else if (collision.gameObject.name == "PlayerSwing")
        {
            PlayHurtSound();

            AudioManager.SwingHit();
            curHealth -= 4;
        }
        else if (collision.gameObject.tag == "ExplosionBox")
        {
            PlayHurtSound();

            curHealth -= 5;
        }

        if (curHealth < 0)
        {
            dying = true;
            curHealth = 0;
        }

        if (curHealth <= 0)
            Die();
    }

    public void Respawn()
    {
        curHealth = Health;
        gameObject.SetActive(true);
    }
    public void RespawnHidden() {
        curHealth = Health;
        gameObject.SetActive(false);
        if (GetComponent<SimpleTurret>())
        {
            StartCoroutine(GetComponent<SimpleTurret>().Swing());
        }
    }

    IEnumerator Dying()
    {
        //GetComponent<CapsuleCollider>().enabled = false;
        Color ogColor = GetComponent<SpriteRenderer>().color;
        Color temp = Color.white;
        temp.a = 1;
        while (temp.a >= .1)
        {
            temp.a -= .1f;
            GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForFixedUpdate();
        }
        GetComponent<SpriteRenderer>().color = ogColor;
        gameObject.SetActive(false);
    }

}
