using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableLetter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private string _char;
    private TextMeshProUGUI _text;

    private RectTransform _rectTransform;
    private Vector3 _startPosition;
    private Transform _startParent;

    private void Start()
    {
        _char = gameObject.name;
        _rectTransform = GetComponent<RectTransform>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _text.text = _char;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _startParent = transform.parent;
    }
    public void OnDrag(PointerEventData eventData) => _rectTransform.position = eventData.position;
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == _startParent)
            transform.position = _startPosition;
    }
}
