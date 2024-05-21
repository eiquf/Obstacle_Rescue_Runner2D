using System;
using UnityEngine;
using UnityEngine.UI;

public class VibrationController : MonoBehaviour
{
    public Action Vibration;
    public Action IsVibrationChanged;
    public Sprite[] ButtonsSprites { get; private set; }
    public Image image { get; private set; }

    #region Scripts
    private readonly VibrationSpritesLoader _spritesLoader = new();

    private readonly VibrationInitialization _vibrationInitialization = new();
    private readonly Vibration _vibration = new();
    #endregion
    private void OnEnable()
    {
        IsVibrationChanged += TurnVibration;
        Vibration += Vibrate;
    }
    private void OnDisable()
    {
        IsVibrationChanged -= TurnVibration;
        Vibration += Vibrate;
    }
    private void Awake()
    {
        LoadSprites();
        Initialization();
        ComponentsSet();
    }
    public void LoadImage(Image imageButton)
    {
        image = imageButton;
        //_startVibrationImageInitialization.Execute();
    }
    private void LoadSprites() => ButtonsSprites = _spritesLoader.Execute();
    private void Initialization() => _vibrationInitialization.Execute(this);
    private void Vibrate() => Handheld.Vibrate();
    private void ComponentsSet()
    {
        //_startVibrationImageInitialization = new StartVibrationImageInitialization(this);
    }
    private void TurnVibration() => _vibration.Execute(this);
}