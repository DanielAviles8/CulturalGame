using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InputActionsHolder _inputActionsHolder;
    private GameInputActions _inputActions;

    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] public static bool _inventoryOn;
    // Start is called before the first frame update
    void Start()
    {
        Prepare();
        _inventoryPanel.SetActive(false);
    }
    private void OnDestroy()
    {
        _inputActions.Player.Inventory.performed -= OpenInventory;
    }
    private void Prepare()
    {
        _inputActions = _inputActionsHolder._GameInputActions;
        _inputActions.Player.Inventory.performed += OpenInventory;
    }
    private void OpenInventory(InputAction.CallbackContext ctx)
    {
        _inventoryOn = !_inventoryOn;
        if (_inventoryOn)
        {
            _inputActions.Player.Shoot.Disable();
            _inputActions.Player.Reload.Disable();
            _inventoryPanel.SetActive(true);
        }
        else
        {
            _inventoryOn = false;
            _inputActions.Player.Shoot.Enable();
            _inputActions.Player.Reload.Enable();
            _inventoryPanel.SetActive(false);
        }
    }
}
