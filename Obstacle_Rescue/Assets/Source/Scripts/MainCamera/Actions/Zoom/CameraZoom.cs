using UnityEngine;

public class CameraZoom : MainCamera
{
    private readonly int _ZoomSize = 11;
    private readonly int _unzoomSize = 16;

    private int _zoomSize;

    public CameraZoom(Camera camera) : base(camera) { }

    public override void Execute(bool logic)
    {
        switch (logic)
        {
            case true: _zoomSize = _ZoomSize; break;
            case false: _zoomSize = _unzoomSize; break;
        }

        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _zoomSize, Time.deltaTime);
    }
}