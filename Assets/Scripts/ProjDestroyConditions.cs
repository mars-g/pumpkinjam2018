using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjDestroyConditions : MonoBehaviour {


    public int Damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerSwing")
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Player")
        {
            PlayerHealth.TakeDamage(Damage);
            gameObject.SetActive(false); //make this in the player health function instead
        }
        else if (collision.gameObject.tag == "Comrade")
        {
            collision.gameObject.GetComponent<ComradeController>().DealDamage(Damage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag=="Ground")
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.GetComponent<Ground>())
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.GetComponent<Wall>())
        {
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
            PlayerHealth.TakeDamage(Damage);
            gameObject.SetActive(false); //make this in the player health function instead
        }
        else if(collision.gameObject.tag == "Comrade")
        {
            collision.gameObject.GetComponent<ComradeController>().DealDamage(Damage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.GetComponent<Ground>())
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.GetComponent<Wall>())
        {
            gameObject.SetActive(false);
        }
    }
}
