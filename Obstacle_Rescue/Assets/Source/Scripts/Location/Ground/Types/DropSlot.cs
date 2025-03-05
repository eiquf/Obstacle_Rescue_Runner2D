using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    private WordPanel _wordPanel;
    private string _correctLetter;
    private void Start()
    {
        _correctLetter = gameObject.name;
        _wordPanel = GetComponentInParent<WordPanel>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        DraggableLetter draggable = eventData.pointerDrag.GetComponent<DraggableLetter>();

        if (draggable != null && draggable.name == _correctLetter)
        {
            _wordPanel.OnRemoved?.Invoke(draggable);
            draggable.transform.SetParent(transform);
            draggable.transform.position = transform.position;
        }
    }
}