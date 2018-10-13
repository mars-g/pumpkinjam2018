using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{


    public Heart[] hearts = new Heart[10];

    private int numhearts = 0;

    public static bool IsActive() { return instance != null; }
    private static HealthDisplay curInstance;
    private static HealthDisplay instance { get { return curInstance; } }


    // Use this for initialization
    void Start()
    {
        if (curInstance == null) { curInstance = this; }
    }

    private bool setyet = false;
    // Update is called once per frame
    void Update()
    {
        if (!setyet)
        {
            if (PlayerHealth.IsActive())
            {
                SetDisplay();
                setyet = true;
            }
        }
    }

    public static void SetDisplay()
    {
        instance.numhearts = PlayerHealth.MaxHealth() / 2;
        for (int i = 0; i < instance.numhearts; i++)
        {
            instance.hearts[i].Heal();
        }
        for (int i = instance.numhearts; i < instance.hearts.Length; i++)
        {
            instance.hearts[i].Gone();
        }
    }

    public static void ClearAll()
    {
        for (int i = 0; i < instance.hearts.Length; i++)
        {
            instance.hearts[i].Gone();
        }
    }

    public static void UpdateDisplay(int curhealth, int maxhealth)
    {
        int i = instance.numhearts - 1;
        int diff = maxhealth - curhealth;

        ClearAll();

        if (diff == 0) SetDisplay();
        else
        {

            for (int t = 0; t < maxhealth - diff; t++)
            {
                if (instance.hearts[i].IsGone())
                {
                    instance.hearts[i].Half();
                }
                else if (instance.hearts[i].IsHalf())
                {
                    instance.hearts[i].Heal();
                    i--;
                }
                else if (instance.hearts[i].IsFull())
                {
                    i--;
                }
            }
        }

    }
}
