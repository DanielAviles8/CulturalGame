using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class InventoryContent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite _largeSprite;
    [SerializeField] private string _description;

    private InventoryPreview _previewUI;

    private void Start()
    {
        _previewUI = FindObjectOfType<InventoryPreview>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse sobre: " + gameObject.name);
        _previewUI.ShowPreview(_largeSprite, _description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _previewUI.HidePreview();
    }
}
