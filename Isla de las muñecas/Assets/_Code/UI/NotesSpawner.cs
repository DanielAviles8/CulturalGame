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

    private GameObject[] spawnedNotes;

    void Start()
    {
        spawnedNotes = new GameObject[noteTexts.Length];

        for (int i = 0; i < noteTexts.Length; i++)
        {
            GameObject newNote = Instantiate(notePrefab, notesParent);
            TextMeshProUGUI tmp = newNote.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = noteTexts[i];
            }
            
            newNote.SetActive(false); 
            spawnedNotes[i] = newNote;
        }
    }
    public void ShowNoteByIndex(int index)
    {
        if (index >= 0 && index < spawnedNotes.Length)
        {
            spawnedNotes[index].SetActive(true);
        }
    }
}
