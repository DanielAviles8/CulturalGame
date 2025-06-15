using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionsHolder _inputActionsHolder;
    private GameInputActions _inputActions;
    private CharacterController _characterController;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    private float _gravity = -9.81f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform; 

    private Vector2 _inputVector;

    private void Start()
    {
        Prepare();
    }

    private void Update()
    {
        _inputVector = _inputActions.Player.Movement.ReadValue<Vector2>();
        MovePlayer();
    }

    private void Prepare()
    {
        _characterController = GetComponent<CharacterController>();
        _inputActions = _inputActionsHolder._GameInputActions;
    }

    private void MovePlayer()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (right * _inputVector.x + forward * _inputVector.y) * _moveSpeed;
        moveDirection.y = _gravity;
        _characterController.Move(moveDirection * Time.deltaTime);


    }
}
