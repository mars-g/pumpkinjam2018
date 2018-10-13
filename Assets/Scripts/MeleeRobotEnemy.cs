using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRobotEnemy : MonoBehaviour {


    private Rigidbody2D rb;
    private Animator anim;
    private GameObject hitbox;

    private bool attacking = false;
    private bool seenplayer = false;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitbox = transform.GetChild(0).gameObject;

        StartCoroutine("OnGuard");
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (seenplayer)
        {

        }
        else
        {

        }
	}

    void DownForce()
    {

    }


    IEnumerator OnGuard()
    {
        int i = 0;
        while (!seenplayer)
        {
            while (i < 90)
            {
                rb.velocity = Vector3.right * 1.5f;
                yield return new WaitForEndOfFrame();
                i++;
            }
            i = 0;
            while (i < 90)
            {
                rb.velocity = Vector3.right * -1.5f;
                yield return new WaitForEndOfFrame();
                i++;
            }
            i = 0;
        }
    }

    IEnumerator Chase()
    {
        yield return null;
    }

    IEnumerator Swing()
    {
        attacking = true;
        hitbox.SetActive(true);
        yield return new WaitForSeconds(.2f);
        hitbox.SetActive(false);
        attacking = false;
    }
}
