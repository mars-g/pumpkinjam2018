using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {


    public GameObject menuCanvas;

    public bool paused = false;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        if (Time.timeScale != 0.0f) {
            paused = false;
            AudioListener.volume = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioListener.volume = 0;
            Cursor.visible = !paused;
            menuCanvas.SetActive(!paused);
            Time.timeScale = (paused) ? 1.0f: 0.0f;
            paused = !paused;
        }
	}
}
