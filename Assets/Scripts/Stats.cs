using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public float invulnTime = 2f;
    public bool invuln = false;
    public Vector3 lastCheckpoint = new Vector3(0f,0f,0f);
    public int comrades = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void MakeInvuln(float invulnTime) {
        StartCoroutine(invulnCo(invulnTime));
    }

    private IEnumerator invulnCo(float invulnTime) {
        invuln = true;
        yield return new WaitForSeconds(invulnTime);
        invuln = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            lastCheckpoint = collision.gameObject.transform.position;
            StartCoroutine(destroyCheckpoint(collision.gameObject));

        }
    }

    public void damageCo() {
        StartCoroutine(damageCoroutine());
    }

    private IEnumerator damageCoroutine()
    {

        Color curColor = GetComponent<SpriteRenderer>().color;
        Color newColor = Color.red;
        invuln = true;
        bool flashBool = true;
        for (float t = 0; t < invulnTime; t += Time.deltaTime)
        {
            if (flashBool)
            {
                GetComponent<SpriteRenderer>().color = newColor;
                t += 0.2f;
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
               
                GetComponent<SpriteRenderer>().color = curColor;
                t += 0.2f;
                yield return new WaitForSeconds(0.2f);
            }
            flashBool = !flashBool;
            yield return null;
        }
            GetComponent<SpriteRenderer>().color = curColor;
       
        invuln = false;
        yield return null;
    }

    private IEnumerator destroyCheckpoint(GameObject checkpoint)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(checkpoint);
    }

}
