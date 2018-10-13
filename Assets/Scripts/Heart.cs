using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    private GameObject left, right;

    void Start()
    {
        left = transform.GetChild(0).gameObject;
        right = transform.GetChild(1).gameObject;
    }

    public void Heal()
    {
        left.SetActive(true);
        right.SetActive(true);
    }
    public void Half()
    {
        left.SetActive(true);
        right.SetActive(false);
    }
    public void Gone()
    {
        left.SetActive(false);
        right.SetActive(false);
    }
    public bool IsFull() { return left.activeSelf && right.activeSelf; }
    public bool IsHalf() { return left.activeSelf && !right.activeSelf; }
    public bool IsGone() { return !left.activeSelf && !right.activeSelf; }

}
