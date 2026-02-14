using UnityEngine;

public class CameraShake : MainCamera
{
    private Vector3 _shakeOffset;
    private Vector3 _initialPosition;

    private readonly float _shakeDistance = 0.1f;
    private float _shakeSpeed = 1;

    public CameraShake(Camera camera) : base(camera) { }

    public override void Execute(bool logic)
    {
        switch (logic)
        {
            case true: Shaking(); break;
            case false: StopShake(); break;
        }
    }
    private void Shaking()
    {
        Vector3 pos = _camera.transform.position;
        Vector3 offsetPos = pos + _shakeOffset;
        float currentDistance = offsetPos.y - _initialPosition.y;
        if (_shakeSpeed >= 0)
        {
            if (currentDistance > _shakeDistance)
                _shakeSpeed *= -1;
        }
        else
        {
            if (currentDistance < -_shakeDistance)
                _shakeSpeed *= -1;
        }
        _shakeOffset.y += _shakeSpeed * Time.deltaTime;
        _shakeOffset.y = Mathf.Clamp(_shakeOffset.y, -_shakeDistance, _shakeDistance);
        _camera.transform.position = _initialPosition + _shakeOffset;
    }
    private void StopShake() => _camera.transform.position = _initialPosition;
}