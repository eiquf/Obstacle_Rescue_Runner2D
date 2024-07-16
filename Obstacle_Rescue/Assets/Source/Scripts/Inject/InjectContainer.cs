using UnityEngine;
using Zenject;

public class InjectContainer : MonoBehaviour
{
    public SoundController SoundController { get; private set; }
    public VibrationController VibrationController { get; private set; }
    public LoadingScreenFactory LoadingScreen { get; private set; }

    [Inject]
    private void Container
        (SoundController soundController,
         VibrationController vibrationController,
         LoadingScreenFactory loadingScreen)
    {
        SoundController = soundController;
        VibrationController = vibrationController;
        LoadingScreen = loadingScreen;
    }
}