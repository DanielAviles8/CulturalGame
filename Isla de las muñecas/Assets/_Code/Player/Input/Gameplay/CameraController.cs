using FirstGearGames.SmoothCameraShaker;
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
    [SerializeField] private Vector3 GameplayPos;

    private Vector2 _inputVector;
    private float xRotation = 0f;

    [Header("Crouched")]
    [SerializeField] public static bool _isCrouched = false;
    [SerializeField] private float _lerpSpeed = 2f;
    [SerializeField] Transform _crouchPosition;
    private Vector3 _targetCameraPos;

    private bool _cameraInitialized = false;

    [SerializeField] Transform _deathPosition;
    [SerializeField] GameObject fence;
    
    private void OnDestroy()
    {
        _gameInputActions.Player.Crouch.performed -= CrouchPlayer;
    }
    void Start()
    {
        Prepare();

        GameplayPos = new Vector3 (0, 0.5f, 0);
        _targetCameraPos = GameplayPos;

        xRotation = 0f;
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    private void Prepare()
    {
        _characterController = GetComponent<CharacterController>();
        _gameInputActions = _inputActionsHolder._GameInputActions;
        _gameInputActions.Player.Crouch.performed += CrouchPlayer;
    }
    void Update()
    {
        _inputVector = _gameInputActions.Player.FacingTo.ReadValue<Vector2>();


        if (!Inventory._inventoryOn)
        {
            MoveCamera();
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }

        playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, _targetCameraPos, _lerpSpeed * Time.deltaTime);
        DeathAnimation();
    }
    void LateUpdate()
    {
        if (!_cameraInitialized)
        {
            GameplayPos = playerCamera.localPosition;
            _targetCameraPos = GameplayPos;
            _cameraInitialized = true;
        }
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
    private void CrouchPlayer(InputAction.CallbackContext ctx)
    {
        _isCrouched = !_isCrouched;

        if (_isCrouched)
        {
            _targetCameraPos = _crouchPosition.localPosition;
            fence.SetActive(false);
        }
        else
        {
            _targetCameraPos = GameplayPos;
            fence.SetActive(true);
        }
    }
    private void DeathAnimation()
    {
        if(TakeDamage.Death == true)
        {
            _targetCameraPos = _deathPosition.localPosition;
            playerCamera.transform.rotation = _deathPosition.transform.rotation;
        }
    }
}
