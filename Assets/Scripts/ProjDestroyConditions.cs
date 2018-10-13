using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjDestroyConditions : MonoBehaviour {


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerSwing")
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false); //make this in the player health function instead
        }
        else if (collision.gameObject.tag == "Comrade")
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerSwing")
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false); //make this in the player health function instead
        }
        else if(collision.gameObject.tag == "Comrade")
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
