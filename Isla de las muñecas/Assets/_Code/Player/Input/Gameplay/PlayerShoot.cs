using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private InputActionsHolder _inputActionsHolder;
    private GameInputActions _gameInputActions;

    [SerializeField] private GameObject Weapon;

    [SerializeField] Gun Gun;
    private void OnDestroy()
    {
        _gameInputActions.Player.Shoot.performed -= ShootGun;
        _gameInputActions.Player.Reload.performed -= ReloadGun;
    }
    private void Start()
    {
        Prepare();
    }
    private void Prepare()
    {
        _gameInputActions = _inputActionsHolder._GameInputActions;
        _gameInputActions.Player.Shoot.performed += ShootGun;
        _gameInputActions.Player.Reload.performed += ReloadGun;
    }
    public void ShootGun(InputAction.CallbackContext ctx)
    {
        Gun.Shoot();
    }
    public void ReloadGun(InputAction.CallbackContext ctx)
    {
        StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {
        if (Gun._bulletBackUps > 0)
        {
            Gun._reloading = true;
            yield return new WaitForSeconds(Gun._reloadTime);


            if (Gun._bulletBackUps > 0 && Gun._bulletBackUps < Gun._magSize)
            {
                Gun._bulletRemaining += Gun._bulletBackUps;
                Gun._bulletBackUps = 0;
            }
            else if (Gun._bulletBackUps == Gun._magSize || Gun._bulletBackUps > Gun._magSize)
            {
                if (Gun._bulletRemaining == 0)
                {
                    Gun._bulletRemaining += Gun._magSize;
                    Gun._bulletBackUps -= Gun._magSize;
                }
                else if (Gun._bulletRemaining > 0)
                {
                    float reloadBullets = Gun._magSize - Gun._bulletRemaining;
                    Gun._bulletRemaining += reloadBullets;
                    Gun._bulletBackUps -= reloadBullets;
                }
            }


            Gun._reloading = false;
            
        }
    }
}
