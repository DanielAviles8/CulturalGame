using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform notesParent; 
    [TextArea]
    [SerializeField] private string[] noteTexts;

    public TMP_Text tmp { get; set; }

    private GameObject[] spawnedNotes;

    void Start()
    {
        spawnedNotes = new GameObject[noteTexts.Length];

        
        GameObject newNote = Instantiate(notePrefab, notesParent);
        tmp = newNote.GetComponentInChildren<TMP_Text>();
                   
        newNote.SetActive(false);
    }
    public void ShowNoteByIndex(int index)
    {
        tmp.gameObject.SetActive(true);
        tmp.text = noteTexts[index];
    }
    public void ClearNote()
    {
        tmp.text = "";
    }
}
