using System;
using UnityEngine;

public sealed class GameCamera : MonoBehaviour
{
    public Action<bool> IsZoomed;
    public Action<bool> IsShaking;

    private Camera _camera;

    #region Scripts
    private CameraShake _cameraShake;
    private CameraZoom _cameraZoom;
    #endregion
    private void OnEnable()
    {
        IsShaking += Shake;
        IsZoomed += Zoom;
    }
    private void OnDisable()
    {
        IsShaking -= Shake;
        IsZoomed -= Zoom;
    }
    private void Awake() => InitializeComponents();
    private void InitializeComponents()
    {
        _camera = GetComponent<Camera>();

        _cameraShake = new(_camera);
        _cameraZoom = new(_camera);
    }
    private void Zoom(bool isZoom) => _cameraZoom.Execute(isZoom);
    private void Shake(bool isShake) => _cameraShake.Execute(isShake);
}