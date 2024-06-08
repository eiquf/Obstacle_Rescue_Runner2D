using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PrefButtonsCreate : IPreferences
{
    private readonly Transform _preferencesCreatePos;
    private readonly bool _canSpawnHomeButton;
    public RectTransform[] ButtonsRectTransform { get; private set; }
    private Button[] _buttons;
    private Image[] _images;

    private readonly InjectContainer _container;

    private readonly string[] _buttonsReferences =
    {
        "VibrationButton",
        "SFXButton",
        "BGMButton",
        "HomeButton"
    };

    public PrefButtonsCreate(bool canSpawnHomeButton, Transform preferencesCreatePos, InjectContainer container)
    {
        _canSpawnHomeButton = canSpawnHomeButton;
        _preferencesCreatePos = preferencesCreatePos;
        _container = container;
    }

    public void Execute() => ButtonsPrefs();
    private void ButtonsPrefs()
    {
        int buttonsCount = _canSpawnHomeButton ? _buttonsReferences.Length : _buttonsReferences.Length - 1;
        _buttons = new Button[buttonsCount];

        for (int i = 0; i < buttonsCount; i++)
        {
            int index = i;
            LoadButtonPrefab(_buttonsReferences[i], index);
        }
    }
    private void LoadButtonPrefab(string prefabReference, int index)
    {
        Addressables.LoadAssetAsync<GameObject>(prefabReference).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                Button button = Object.Instantiate(handle.Result.GetComponent<Button>(), _preferencesCreatePos);
                button.transform.localScale = Vector3.zero;
                _buttons[index] = button;

                button.onClick.AddListener(() =>
                {
                    OnButtonClick(_buttonsReferences[index]);
                    new ButtonTapAnimation().Execute(button.transform);
                });

                if (_buttons.All(b => b != null))
                    InitializeComponents();
            }
            Addressables.Release(handle);
        };
    }

    private void InitializeComponents()
    {
        _images = _preferencesCreatePos.GetComponentsInChildren<Image>();
        ButtonsRectTransform = _preferencesCreatePos.GetComponentsInChildren<RectTransform>();

        if (_images.Length > 0)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                Image buttonImage = _buttons[i].GetComponentInChildren<Image>();
                AssignImageToButton(_buttonsReferences[i], buttonImage);
            }
        }
    }
    private void AssignImageToButton(string buttonReference, Image image)
    {
        switch (buttonReference)
        {
            case "SFXButton":
                _container.SoundController.IsImagesSet?.Invoke(image);
                break;
            case "VibrationButton":
                _container.VibrationController.IsImagesSetted?.Invoke(image);
                break;
            case "BGMButton":
                _container.SoundController.IsImagesSet?.Invoke(image);
                break;
            case "HomeButton":
                // If HomeButton should set images, uncomment the following line
                // _container.SoundController.IsImagesSetted?.Invoke(new[] { image });
                break;
        }
    }
    private void OnButtonClick(string buttonReference)
    {
        switch (buttonReference)
        {
            case "SFXButton":
                _container.SoundController.IsSoundVolumeChanged?.Invoke(UIButtonsCount.SFX);
                break;
            case "BGMButton":
                _container.SoundController.IsSoundVolumeChanged?.Invoke(UIButtonsCount.BGM);
                break;
            case "VibrationButton":
                _container.VibrationController.IsVibrationChanged?.Invoke();
                break;
            case "HomeButton":
                _container.LoadingScreen.OnChangeScene?.Invoke(LevelsKeys.mainMenuLevelKey);
                break;
        }
    }
}