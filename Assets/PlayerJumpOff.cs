using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpOff : MonoBehaviour {

    public float lowerTime = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.GetComponent<Stats>())
        {
            
            StartCoroutine(Lower());
        }
    }

    private IEnumerator Lower() {
        for (float t = 0; t < lowerTime; t += Time.deltaTime)
        {
            Vector3 pos = GetComponentInParent<Transform>().position;
            //GetComponentInParent<Transform>().position = new Vector3(pos.x, pos.y -0.2f, pos.z);
            yield return null;
        }
        Destroy(transform.parent.gameObject);
    }
}
