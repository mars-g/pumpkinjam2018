using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComrade : MonoBehaviour {

    public GameObject comrade;

    private Stats stats;
	// Use this for initialization
	void Start () {
        stats = FindObjectOfType<Stats>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            if (stats.comrades > 0) {
                Vector3 position = FindObjectOfType<CursorControl>().gameObject.transform.position;
                Instantiate(comrade, position, Quaternion.identity);
                stats.comrades--;
            }

            
        }
	}
}
