using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int Health;
    
    private int curhealth;

    private bool onSpike = false;
    //public HealthDisplay HealthDis;


    public static bool IsActive() { return instance != null; }
    private static PlayerHealth curInstance;
    private static PlayerHealth instance { get { return curInstance; } }


    // Use this for initialization
    void Start()
    {
        if (curInstance == null) { curInstance = this; }
        curhealth = Health; //change to account for loading
    }

    // Update is called once per frame
    void Update()
    {
        //debug
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(3);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Heal(2);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            HealMax();
        }*/
        if (onSpike) {
            TakeDamage(2);
        }
    }

    public static void SendToDisplay()
    {
        //HealthDisplay.UpdateDisplay()
    }


    public static void TakeDamage(int damage)
    {

        AudioManager.PlayerHurt();
        if (instance.GetComponent<Stats>().invuln) {
            return;
        }
        instance.GetComponent<Stats>().damageCo();



        instance.curhealth -= damage;
        if (instance.curhealth <= 0)
        {
            instance.curhealth = 0;
            Die();
        }
        HealthDisplay.UpdateDisplay(instance.curhealth, instance.Health);


    }

    
    public static void Die()
    {
        HealMax();
        instance.gameObject.transform.position = instance.GetComponent<Stats>().lastCheckpoint;
        
        
    }

    public static void Heal(int heal)
    {
        instance.curhealth += heal;
        if (instance.curhealth > instance.Health)
        {
            instance.curhealth = instance.Health;
        }
        HealthDisplay.UpdateDisplay(instance.curhealth, instance.Health);

    }

    public static void HealMax()
    {
        instance.curhealth = instance.Health;
        HealthDisplay.UpdateDisplay(instance.curhealth, instance.Health);

    }

    public static void RaiseMax()
    {
        instance.Health += 2;
        instance.curhealth = instance.Health;
        HealthDisplay.SetDisplay();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHitbox")
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyWepDamage>().Damage);
        }
        else if (collision.gameObject.tag == "Spikes") {
            onSpike = true;
        }
        else if (collision.gameObject.GetComponent<KillBox>())
        {
            Die();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            onSpike = false;
        }
    }

    public static bool IsFull()
    {
        return instance.curhealth == instance.Health;
    }
    public static int MaxHealth() { return instance.Health; }
    public static int CurHealth() { return instance.curhealth; }
}
