using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("HERE2");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.GetComponent<ComradePickup>()) {
            GetComponent<Stats>().comrades += collision.gameObject.GetComponent<ComradePickup>().count;
            Destroy(collision.gameObject);
            AudioManager.PickUpOrb();
        }
        if (collision.gameObject.GetComponent<HeartPickup>()) {
            PlayerHealth.Heal(collision.gameObject.GetComponent<HeartPickup>().amount);
            Destroy(collision.gameObject);
            AudioManager.PickUpOrb();
        }
    }
}
