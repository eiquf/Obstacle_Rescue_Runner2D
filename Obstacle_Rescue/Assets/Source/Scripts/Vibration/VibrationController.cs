using System;
using UnityEngine;
using UnityEngine.UI;

public class VibrationController : MonoBehaviour
{
    public Action Vibration;
    public Action IsVibrationChanged;
    public Action<Image> IsImagesSetted;
    public Sprite[] ButtonsSprites { get; private set; }
    public Image Image { get; private set; }

    #region Scripts
    private readonly VibrationSpritesLoader _spritesLoader = new();

    private readonly VibrationInitialization _vibrationInitialization = new();
    private readonly VibrationImageInitialization _vibrationImageInitialization = new();

    private readonly Vibration _vibration = new();
    #endregion
    private void OnEnable()
    {
        IsVibrationChanged += TurnVibration;
        Vibration += Vibrate;
        IsImagesSetted += LoadImage;
    }
    private void OnDisable()
    {
        IsVibrationChanged -= TurnVibration;
        Vibration += Vibrate;
        IsImagesSetted -= LoadImage;
    }
    private void Awake()
    {
        LoadSprites();
        Initialization();
    }
    private void LoadImage(Image imageButton)
    {
        Image = imageButton;
        _vibrationImageInitialization.Execute(this);
    }
    private void LoadSprites() => ButtonsSprites = _spritesLoader.Execute();
    private void Initialization() => _vibrationInitialization.Execute(this);
    private void Vibrate() => Handheld.Vibrate();
    private void TurnVibration() => _vibration.Execute(this);
}