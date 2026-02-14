using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SafeZone : MonoBehaviour
{
    [Tooltip("Put panels that should be in Safe Zone"), Header("Панели для Safe Zone")]
    [SerializeField] private List<RectTransform> _rectTransforms = new();

    private Rect _lastSafeArea;
    private CanvasScaler _canvasScaler;

    private readonly DeviceChecker _checker = new();

    private void Awake()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
        _checker.OnChangedMatch?.Invoke(_canvasScaler);
    }
    private void LateUpdate()
    {
        if (_lastSafeArea != Screen.safeArea)
        {
            _lastSafeArea = Screen.safeArea;
            Refresh();
        }
    }

    private void Refresh()
    {
        Vector2 anchorMin = _lastSafeArea.position;
        Vector2 anchorMax = _lastSafeArea.position + _lastSafeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        foreach (var rectTransform in _rectTransforms)
        {
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}