using System;
using UnityEngine;
using UnityEngine.UI;

public class VibrationController : MonoBehaviour
{
    public Action<Transform> IsImageSet;
    public Action<int> SetIndex;
    public event Action IsVibrate;
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
    public Image Image { get; private set; }
    public readonly Vibration Vibration = new();
    private int _volume;

    private void OnEnable()
    {
        IsImageSet += InitializeImage;
        IsVibrate += Vibrate;
        SetIndex += Index;
    }

    private void OnDisable()
    {
        IsImageSet -= InitializeImage;
        IsVibrate -= Vibrate;
        SetIndex -= Index;
    }

    private void Start() => Vibration.Execute(this);

    private void InitializeImage(Transform buttonsParent)
    {
        Image = null;
        Image = buttonsParent.GetChild(0).GetComponent<Image>();
        new VibrationInitialization().Execute(this);
    }

    private void Vibrate()
    {
        if (_volume > 0) Handheld.Vibrate();
    }
    private void Index(int index) => _volume = index;
}
