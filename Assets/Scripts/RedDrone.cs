using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDrone : MonoBehaviour
{


    private Rigidbody2D rb;
    //private Animator anim;
    private GameObject player;
    private GameObject[] projectiles;


    private bool attacking = false;
    private bool seenplayer = false;
    private int proj = 0;
    //private bool changed = true;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        player = FindObjectOfType<Stats>().gameObject;

        projectiles = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            projectiles[i] = transform.GetChild(i).gameObject;
        }


        StartCoroutine("OnGuard");
    }

    // Update is called once per frame
    void Update()
    {

        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (!seenplayer)
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
            if (Mathf.Abs(transform.position.x - player.transform.position.x) >= 8)
            {
                Vector3 direction = player.transform.position - transform.position;
                direction.Normalize();
                rb.velocity = direction * 3.5f;
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

        Vector3 directionproj = player.transform.position - transform.position;
        directionproj.Normalize();

        projectiles[proj].transform.localPosition = Vector3.zero;
        projectiles[proj].SetActive(true);
        projectiles[proj].GetComponent<Rigidbody2D>().velocity = directionproj * 8f;

        proj++;
        if (proj >= transform.childCount)
            proj = 0;


        yield return new WaitForSeconds(1f);
        attacking = false;
    }
}
