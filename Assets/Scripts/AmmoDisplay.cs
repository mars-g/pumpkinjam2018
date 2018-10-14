using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour {

    private TextMesh tm;

	// Use this for initialization
	void Start ()
    {
        tm = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        tm.text = "" + FindObjectOfType<Stats>().comrades;
	}
}
