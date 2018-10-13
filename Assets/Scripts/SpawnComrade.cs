using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComrade : MonoBehaviour {

    public GameObject comrade;
    public bool canSpawn = true;

    private Stats stats;
	// Use this for initialization
	void Start () {
        stats = FindObjectOfType<Stats>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.timeScale == 0) {
            StartCoroutine(pauseBuffer());
        }
		if (Input.GetMouseButtonDown(0) && canSpawn)
        {
            if (stats.comrades > 0) {
                Vector3 position = FindObjectOfType<CursorControl>().gameObject.transform.position;
                Instantiate(comrade, position, Quaternion.identity);
                stats.comrades--;
            }

            
        }
	}

    private IEnumerator pauseBuffer() {
        canSpawn = false;
        yield return new WaitForSeconds(0.1f);
        canSpawn = true;
    }
}
