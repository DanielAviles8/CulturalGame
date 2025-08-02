using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFlashlight : MonoBehaviour
{
    [SerializeField] private GameObject Flashlight;
    [SerializeField] private float velocidadRotacion = 10f;
    public bool flashlightActivated { get; set; }

    [SerializeField] private InventoryCount inventoryCount;
    // Start is called before the first frame update
    void Start()
    {
        flashlightActivated = false;
        Flashlight.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventoryCount.hasFlash = true;
            Flashlight.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        transform.Rotate(0f, velocidadRotacion * Time.deltaTime, 0f);
    }
}
