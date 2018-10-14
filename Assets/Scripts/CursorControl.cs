using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour {


    public Shader pressShader;
    public Shader defShader;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var v = Input.mousePosition;
        v.z = 1.0f;
        transform.position = Camera.main.ScreenToWorldPoint(v);

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            GetComponent<SpriteRenderer>().material.shader = pressShader;
        }
        else {
            GetComponent<SpriteRenderer>().material.shader = defShader;
        }
	}
}
