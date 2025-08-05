using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorials : MonoBehaviour
{
    [SerializeField] private Image tutorial;
    // Start is called before the first frame update
    private void Start()
    {
        tutorial.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        tutorial.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        tutorial.enabled = false;
    }
}
