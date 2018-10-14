using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRobotEnemy : MonoBehaviour {


    private Rigidbody2D rb;
    private Animator anim;
    private GameObject hitbox;
    private GameObject player;

    private bool attacking = false;
    private bool seenplayer = false;
    //private bool changed = true;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitbox = transform.GetChild(0).gameObject;
        player = FindObjectOfType<Stats>().gameObject;

        StartCoroutine("OnGuard");
	}
	
	// Update is called once per frame
	void Update ()
    {

        anim.SetBool("Attacking", attacking);

        if(rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if(!seenplayer)
            seenplayer = DetectPlayer();

        if (seenplayer)
        {
            StopCoroutine("OnGuard");
            StartCoroutine("Chase");
        }
        else
        {

        }
	}

    void DownForce()
    {

    }

    bool DetectPlayer()
    {
        if ((transform.position - player.transform.position).magnitude <= 10)
        {
            return true;
        }
        else return false;
    }

    IEnumerator OnGuard()
    {
        int i = 0;
        while (!seenplayer)
        {
            while (i < 90)
            {
                rb.velocity = Vector3.right * 1.5f;
                yield return new WaitForFixedUpdate();
                i++;
            }
            i = 0;
            while (i < 90)
            {
                rb.velocity = Vector3.right * -1.5f;
                yield return new WaitForFixedUpdate();
                i++;
            }
            i = 0;
        }
    }

    IEnumerator Chase()
    {
        while (seenplayer && !GetComponent<EnemyHealth>().isDying())
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) >= 2.5)
            {
                Vector3 direction = player.transform.position - transform.position;
                //direction.Normalize();
                if (direction.x > 0)
                {
                    rb.velocity = Vector3.right * 3f;
                }
                else if (direction.x < 0)
                {
                    rb.velocity = Vector3.right * -3f;
                }
            }
            else
            {
                if (!attacking)
                {
                    StartCoroutine("Swing");
                }
            }

            yield return new WaitForFixedUpdate();
        }
            
    }

    IEnumerator Swing()
    {
        attacking = true;
        hitbox.SetActive(true);
        yield return new WaitForSeconds(.1f);
        hitbox.SetActive(false);
        yield return new WaitForSeconds(.5f);
        attacking = false;
    }
}
