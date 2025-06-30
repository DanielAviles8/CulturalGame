using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{
    public static bool Chase = false;
    public GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Chase = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Chase = false;
        }
    }*/
}
