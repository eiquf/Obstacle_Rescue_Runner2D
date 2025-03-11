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
        DraggableLetter draggable = eventData.pointerDrag?.GetComponent<DraggableLetter>();

        if (draggable != null && draggable.name == _correctLetter)
        {
            _wordPanel.RemoveOrder(draggable);
            draggable.transform.SetParent(transform, true);
            draggable.transform.localPosition = Vector3.zero;
            draggable.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

}