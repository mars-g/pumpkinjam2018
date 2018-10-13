using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComradeController : MonoBehaviour {


    public Sprite explodeSprite;
    public float explodeTime = 2f;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Explode();
            
        }
   }


    public void Explode() {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<PlayerJumpOff>().gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = explodeSprite;
        StartCoroutine(explodeCo());
    }


    private IEnumerator explodeCo()
    {
        GetComponentInChildren<CircleCollider2D>().enabled = true;

        for (float t = 0; t < explodeTime; t += Time.deltaTime)
        {
            yield return null;
        }
        Destroy(GetComponentInParent<Transform>().gameObject);
        yield return null;
    }
    
}
