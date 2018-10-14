using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComradeController : MonoBehaviour {


    public Sprite explodeSprite;
    public float explodeTime = 2f;
    private int health = 5;
    private Color fadein = Color.white;
    private bool canexplode = true;
	// Use this for initialization
	void OnEnable ()
    {
        fadein.a = 0f;
        GetComponent<SpriteRenderer>().color = fadein;
        StartCoroutine("FadeIn");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            if(canexplode)
                Explode();
            
        }
   }

    public void FadeOut()
    {
        canexplode = false;
        StartCoroutine("FadeOutCo");
    }

    public void DealDamage(int dam)
    {
        health -= dam;
        if (health <= 0)
        {
            FadeOut();
        }
    }

    public void Explode() {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<PlayerJumpOff>().gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = explodeSprite;
        StartCoroutine(explodeCo());
    }

    IEnumerator FadeIn()
    {
        while (fadein.a <= .9)
        {
            fadein.a += .1f;
            GetComponent<SpriteRenderer>().color = fadein;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator FadeOutCo()
    {

        canexplode = false;
        fadein.a = 1;
        GetComponent<SpriteRenderer>().color = fadein;
        while (fadein.a >= .1)
        {
            fadein.a -= .1f;
            GetComponent<SpriteRenderer>().color = fadein;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
    private IEnumerator explodeCo()
    {
        GetComponent<AudioSource>().Play();
        GetComponentInChildren<CircleCollider2D>().enabled = true;
        Color temp = Color.white;
        temp.a = 1;

        for (float t = 0; t < explodeTime; t += Time.deltaTime)
        {
            transform.Rotate(-Vector3.forward, 2.5f);
            temp.a -= .05f;
            GetComponent<SpriteRenderer>().color = temp;

            yield return null;
        }
        Destroy(GetComponentInParent<Transform>().gameObject);
        yield return null;
    }
    
}
