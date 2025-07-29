using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private InputActionsHolder _inputAcionsHolder;
    private GameInputActions _gameInputActions;

    [SerializeField] private bool _flashlightOn;
    [SerializeField] private float _lightTimer;
    [SerializeField] private GameObject _light;

    public static bool hasFlashlight;

    private void OnDestroy()
    {
        _gameInputActions.Player.Flashlight.performed -= ToggleFlashlight;
    }
    private void Start()
    {
        Prepare();
        _flashlightOn = false;
        _light.SetActive(false);
        hasFlashlight = false;
    }
    private void Prepare()
    {
        _gameInputActions = _inputAcionsHolder._GameInputActions;
        _gameInputActions.Player.Flashlight.performed += ToggleFlashlight;
    }
    public void ToggleFlashlight(InputAction.CallbackContext ctx)
    {
        if (_flashlightOn)
        {
            _flashlightOn = false;
            _light.SetActive(false);
        }
        else
        {
            _flashlightOn = true;
            _light.SetActive(true);
            StartCoroutine(TurningOffFlashlight());
        }
    }
    public IEnumerator TurningOffFlashlight()
    {
        yield return new WaitForSeconds(_lightTimer);
        _flashlightOn = false;
        _light.SetActive(false);
    }
}