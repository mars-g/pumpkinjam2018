using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour {

    public GameObject[] waypoints;
    private GameObject curWaypoint;
    private int wpNum = 0;

    public GameObject specialWaypoint;

    private GameObject NormalProj, SpecialProj;
    private GameObject[] normals, specials;
    private int normNum = 0;
    private int specNum = 0;

    private Animator anim;
    private Rigidbody2D rb;

    private GameObject player;

    private bool readytoattack = false;
    private bool specialattacking = false;

    private float attackStart = 0;
    public float normaltime = 2f;
    public float specialtime = 8f;

    private bool canatk = true;
	// Use this for initialization
	void OnEnable ()
    {

        NormalProj = transform.GetChild(0).gameObject;
        SpecialProj = transform.GetChild(1).gameObject;

        normals = new GameObject[NormalProj.transform.childCount];
        specials = new GameObject[SpecialProj.transform.childCount];

        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = NormalProj.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < specials.Length; i++)
        {
            specials[i] = SpecialProj.transform.GetChild(i).gameObject;
        }

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        curWaypoint = waypoints[wpNum];

        player = FindObjectOfType<Stats>().gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!readytoattack)
        {
            Vector3 direction = curWaypoint.transform.position - transform.position;
            rb.velocity = direction * 2.5f;
        }
        else if(!specialattacking && readytoattack)
        {
            if(normaltime >= Time.time - attackStart)
            {
                if (canatk)
                {
                    StartCoroutine("Normal");
                }
            }
            else
            {
                StopCoroutine("Normal");
                canatk = true;
                wpNum++;
                if (wpNum >= waypoints.Length)
                {
                    curWaypoint = specialWaypoint;
                    specialattacking = true;
                }
                else
                {
                    curWaypoint = waypoints[wpNum];
                }
                readytoattack = false;
            }
        }
        else if (specialattacking && readytoattack)
        {
            if (specialtime >= Time.time - attackStart)
            {
                if (canatk)
                {
                    StartCoroutine("Special");
                }
            }
            else
            {
                StopCoroutine("Special");
                canatk = true;
                wpNum=0;
                curWaypoint = waypoints[wpNum];
                readytoattack = false;
                specialattacking = false;
            }
        }


        anim.SetBool("Attacking", readytoattack && !specialattacking);
        anim.SetBool("SpecialAttacking", readytoattack && specialattacking);
        anim.SetBool("Dying", GetComponent<EnemyHealth>().isDying());
	}


    IEnumerator Normal()
    {
        canatk = false;
        Vector3 directionproj = player.transform.position - transform.position;
        directionproj.Normalize();

        normals[normNum].transform.localPosition = Vector3.zero;
        normals[normNum].SetActive(true);
        normals[normNum].GetComponent<Rigidbody2D>().velocity = directionproj * 8f;

        normNum++;
        if (normNum >= normals.Length)
            normNum = 0;


        yield return new WaitForSeconds(.8f);

        canatk = true;
    }

    IEnumerator Special()
    {
        canatk = false;
        Vector2 directionproj = Vector2.right * Random.Range(-.3f, .3f);
        directionproj += Vector2.up;
        directionproj.Normalize();

        specials[specNum].transform.localPosition = Vector3.zero;
        specials[specNum].SetActive(true);
        specials[specNum].GetComponent<Rigidbody2D>().AddForce(directionproj * 2000f);

        specNum++;
        if (specNum >= specials.Length)
            specNum = 0;


        yield return new WaitForSeconds(.4f);

        canatk = true;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BossWaypoint"))
        {
            if (collision.gameObject == curWaypoint)
            {
                readytoattack = true;
                attackStart = Time.time;
                rb.velocity = Vector3.zero;
            }
        }
    }
}
