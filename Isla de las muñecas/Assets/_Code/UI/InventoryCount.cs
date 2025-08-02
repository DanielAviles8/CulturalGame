using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryCount : MonoBehaviour
{
    public bool hasGun { get; set; }
    public bool hasFlash { get; set; }
    public bool hasBullet { get; set; }
    public bool hasHealthPot { get; set; }
    public bool hasKey { get; set; }

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

    // Start is called before the first frame update
    void Start()
    {
        gun.enabled = false;
        bullet.enabled = false;
        flashlight.enabled = false;
        heal.enabled = false;
        key.enabled = false;
        bulletCount.enabled = false;
        healCount.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ShowGun();
        ShowFlashlight();
        ShowItems();
        ShowKeyItems();
    }
    private void ShowGun()
    {
        if (ActivateGun.gunActivated)
        {
            bulletCount.enabled = true;
            if (PlayerShoot.hasGun) gun.enabled = true;
            if (Gun._bulletBackUps >= 0)
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
    }
    private void ShowFlashlight()
    {
        if (hasFlash == true)
        {
            Flashlight.hasFlashlight = true;
            flashlight.enabled = true;
        }
    }
    private void ShowItems()
    {
        if(PlayerMovement._healtPots > 0)
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
        if (KeyTrigger.keyObtained)
        {
            key.enabled = true;
        }
    }
}
