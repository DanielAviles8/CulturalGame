using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractNote : MonoBehaviour
{
    [SerializeField] private InputActionsHolder _inputActionsHolder;
    private GameInputActions _gameInputActions;

    [SerializeField] private GameObject UIInteractionBinding;
    [SerializeField] private GameObject Note;
    private bool _interact;

    public int noteIndex;
    public NotesSpawner notesSpawner;

    [SerializeField] private float _speed;      
    [SerializeField] private float _amplitude; 

    private Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        Prepare();
        UIInteractionBinding.SetActive(false);
        Note.SetActive(false);
        _interact = false;
        _startPos = transform.position;
    }
    private void OnDestroy()
    {
        _gameInputActions.Player.Interact.performed -= InteractWithNote;
    }
    private void Prepare()
    {
        _gameInputActions = _inputActionsHolder._GameInputActions;
        _gameInputActions.Player.Interact.performed += InteractWithNote;
    }
    private void Update()
    {
        float offsetY = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = _startPos + new Vector3(0f, offsetY, 0f);
    }
    private void InteractWithNote(InputAction.CallbackContext ctx)
    {
        if(_interact )
        {
            Note.SetActive(true);
            notesSpawner.ShowNoteByIndex(noteIndex);
            UIInteractionBinding.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _interact = true;
            UIInteractionBinding.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _interact = false;
            UIInteractionBinding.SetActive(false);
            notesSpawner.ClearNote();
        }
    }
}
