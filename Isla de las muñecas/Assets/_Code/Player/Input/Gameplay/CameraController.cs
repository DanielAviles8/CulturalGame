using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputActionsHolder _inputActionsHolder;
    private GameInputActions _gameInputActions;
    private CharacterController _characterController;

    [Header ("CameraSettings")]
    [SerializeField] private Transform playerCamera;       
    [SerializeField] private float mouseSensitivity = 400f;
    [SerializeField] private float stickSensitivity = 600f;
    [SerializeField] private float verticalClamp = 90f;

    private Vector2 _inputVector;
    private float xRotation = 0f;

    void Start()
    {
        Prepare();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        xRotation = 0f;
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    private void Prepare()
    {
        _characterController = GetComponent<CharacterController>();
        _gameInputActions = _inputActionsHolder._GameInputActions;
    }
    void Update()
    {
        _inputVector = _gameInputActions.Player.FacingTo.ReadValue<Vector2>();
        MoveCamera();
    }
    private void MoveCamera()
    {

        float mouseX, mouseY;

        if (Gamepad.current != null && Gamepad.current.rightStick.IsActuated())
        {
            mouseX = _inputVector.x * stickSensitivity * Time.deltaTime;
            mouseY = _inputVector.y * stickSensitivity * Time.deltaTime;
        }
        else
        {
            mouseX = _inputVector.x * mouseSensitivity * Time.deltaTime;
            mouseY = _inputVector.y * mouseSensitivity * Time.deltaTime;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * mouseX);
    }
    private void BlockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
