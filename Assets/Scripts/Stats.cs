using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public bool invuln = false;
    public int comrades = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (invuln)
            GetComponent<CapsuleCollider2D>().enabled = false;
        else
            GetComponent<CapsuleCollider2D>().enabled = true;
    }

    public void Invuln(bool invbool) { invuln = invbool; }
}
