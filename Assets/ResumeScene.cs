using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeScene : MonoBehaviour {

    public GameObject MenuCanvas;

    public void Resume() {
        MenuCanvas.SetActive(false);
        Time.timeScale = 1.0f; 
    }
}
