using UnityEngine;
using Zenject;

public class InjectContainer : MonoBehaviour
{
    public SoundController SoundController { get; private set; }
    public VibrationController VibrationController { get; private set; }
    public LoadingScreen LoadingScreen { get; private set; }
    [Inject]
    private void Container
        (SoundController soundController,
         VibrationController vibrationController,
         LoadingScreen loadingScreen)
    {
        SoundController = soundController;
        VibrationController = vibrationController;
        LoadingScreen = loadingScreen;
    }
}