using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryPreview : MonoBehaviour
{
    [SerializeField] private Image _largeImage;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    private void Start()
    {
        _largeImage.enabled = false;
        _descriptionText.enabled = false;
    }
    public void ShowPreview(Sprite sprite, string description)
    {
        _largeImage.sprite = sprite;
        _descriptionText.text = description;
        _largeImage.enabled = true;
        _descriptionText.enabled = true;
    }

    public void HidePreview()
    {
        _largeImage.enabled = false;
        _descriptionText.enabled = false;
    }
}
