using UnityEngine;

public class VibrationController : MonoBehaviour
{
    public Sprite[] ButtonsSprites { get; private set; }
    public Vibration Vibration { get; private set; }
    private void Awake()
    {
        Initialization();
        LoadSprites();
        new VibrationInitialization().Execute(this);

    }
    private void LoadSprites() => ButtonsSprites = new VibrationSpritesLoader().Execute();
    private void Initialization()
    {
        Vibration = new(this);
    }
}