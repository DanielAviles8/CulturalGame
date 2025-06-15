using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private InputActionsHolder _inputActionsHolder;
    private GameInputActions _gameInputActions;

    [SerializeField] Gun Gun;
    private void OnDestroy()
    {
        _gameInputActions.Player.Shoot.performed -= ShootGun;
    }
    private void Start()
    {
        Prepare();
    }
    private void Prepare()
    {
        _gameInputActions = _inputActionsHolder._GameInputActions;
        _gameInputActions.Player.Shoot.performed += ShootGun;
    }
    public void ShootGun(InputAction.CallbackContext ctx)
    {
        Gun.Shoot();
    }
}
