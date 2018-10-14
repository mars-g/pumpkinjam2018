using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurret : MonoBehaviour {


    public float projspeed = 7f;
    public float cooldown = .7f;
    public Vector3 direction = Vector3.zero;
    public Vector3 origin;

    private GameObject[] projectiles;

    private int proj = 0;


    // Use this for initialization
    void Start ()
    {
        projectiles = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            projectiles[i] = transform.GetChild(i).gameObject;
        }

        StartCoroutine("Swing");
    }

    IEnumerator Swing()
    {
        while (!GetComponent<EnemyHealth>().isDying())
        {
            projectiles[proj].transform.localPosition = origin;
            projectiles[proj].SetActive(true);
            projectiles[proj].GetComponent<Rigidbody2D>().velocity = direction.normalized * projspeed;

            proj++;
            if (proj >= transform.childCount)
                proj = 0;


            yield return new WaitForSeconds(cooldown);
        }
    }
}
