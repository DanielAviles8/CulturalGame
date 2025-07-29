using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryCount : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] private Image gun;
    [SerializeField] private Image bullet;
    [SerializeField] private TextMeshProUGUI bulletCount;

    [Header("Items")]
    [SerializeField] private Image flashlight;
    [SerializeField] private Image heal;
    [SerializeField] private TextMeshProUGUI healCount;

    [Header("KeyItems")]
    [SerializeField] private Image key;
    [SerializeField] private Image key0;

    // Start is called before the first frame update
    void Start()
    {
        gun.enabled = false;
        bullet.enabled = false;
        flashlight.enabled = false;
        heal.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ShowGun();
        ShowItems();
        ShowKeyItems();
    }
    private void ShowGun()
    {
        if (PlayerShoot.hasGun) gun.enabled = true;
        if(Gun._bulletBackUps >= 0)
        {
            bullet.enabled = true;
            bulletCount.enabled = true;
            bulletCount.text = Gun._bulletBackUps.ToString();
        }
        else
        {
            bulletCount.enabled = false;
        }
    }
    private void ShowItems()
    {
        if(Flashlight.hasFlashlight) flashlight.enabled = true;
        if(PlayerMovement._healtPots >= 0)
        {
            heal.enabled = true;
            healCount.enabled = true;
            healCount.text = PlayerMovement._healtPots.ToString();
        }
        else
        {
            healCount.enabled = false;
        }
    }
    private void ShowKeyItems()
    {

    }
}
