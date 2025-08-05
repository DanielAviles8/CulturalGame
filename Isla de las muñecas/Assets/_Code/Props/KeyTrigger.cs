using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Key;
    [SerializeField] private GameObject Door;
    [SerializeField] private float rotateVelocity = 10f;
    public static bool keyObtained;
    // Start is called before the first frame update
    void Start()
    {
        keyObtained = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotateVelocity * Time.deltaTime, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyObtained = true;
            Door.SetActive(false);
            Key.SetActive(false);
        }
    }
}
