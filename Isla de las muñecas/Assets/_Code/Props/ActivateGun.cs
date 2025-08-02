using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGun : MonoBehaviour
{
    [SerializeField] private GameObject Weapon;
    [SerializeField] private float rotateVelocity = 10f;
    public static bool gunActivated;
    // Start is called before the first frame update
    void Start()
    {
        gunActivated = false;
        Weapon.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gunActivated = true;
            Weapon.SetActive(true);
            //Gun.gunActivated = true;
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        transform.Rotate(0f, rotateVelocity * Time.deltaTime, 0f);
    }
}
