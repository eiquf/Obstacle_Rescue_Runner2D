using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PrefButtonsCreate : IPreferences
{
    private readonly Transform _preferencesCreatePos;
    private readonly bool _canSpawnHomeButton;
    public RectTransform[] ButtonsRectTransform { get; private set; }
    public Button[] _buttons;
    private Image[] _images;

    private readonly InjectContainer _container;

    private readonly string[] _buttonsReferences =
    {
        "SFXButton",
        "VibrationButton",
        "BGMButton",
        "HomeButton"
    };
    public PrefButtonsCreate(bool canSpawnHomeButton, Transform preferencesCreatePos, InjectContainer container)
    {
        _canSpawnHomeButton = canSpawnHomeButton;
        _preferencesCreatePos = preferencesCreatePos;
        _container = container;
    }

    public void Execute()
    {
        ButtonsPrefs();
    }
    private void ButtonsPrefs()
    {
        int indexToInstantiate = _canSpawnHomeButton ? _buttonsReferences.Length : _buttonsReferences.Length - 1;
        _buttons = new Button[indexToInstantiate];
        int buttonsLoaded = 0;

        for (int i = 0; i < indexToInstantiate; i++)
        {
            int index = i;
            LoadButtonPrefab(_buttonsReferences[i], index, _buttons, () =>
            {
                buttonsLoaded++;
                if (buttonsLoaded == indexToInstantiate)
                {
                    InitializeComponents();
                }
            });
        }
    }

    private void LoadButtonPrefab(string prefabReference, int index, Button[] buttons, System.Action onLoaded)
    {
        Addressables.LoadAssetAsync<GameObject>(prefabReference).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject prefab = handle.Result;
                if (prefab != null)
                {
                    Button button = Object.Instantiate(prefab.GetComponent<Button>(), _preferencesCreatePos);
                    button.transform.localScale = Vector3.zero;
                    buttons[index] = button;

                    button.onClick.AddListener(() =>
                    {
                        OnButtonClick(index);
                        PlayButtonTapAnimation(button.transform);
                    });
                }
                Addressables.Release(handle);
            }
            onLoaded?.Invoke();
        };
    }

    private void InitializeComponents()
    {
        _images = _preferencesCreatePos.GetComponentsInChildren<Image>();
        ButtonsRectTransform = _preferencesCreatePos.GetComponentsInChildren<RectTransform>();

        if (_images.Length > 0)
        {
            _container.SoundController.IsImagesSetted?.Invoke(_images);
            _container.VibrationController.IsImagesSetted?.Invoke(_images[0]);
        }
    }
    private void OnButtonClick(int index)
    {
        switch (index)
        {
            case UIButtonsCount.SFX:
                _container.SoundController.IsSoundVolumeChanged?.Invoke(UIButtonsCount.SFX);
                break;
            case UIButtonsCount.BGM:
                _container.SoundController.IsSoundVolumeChanged?.Invoke(UIButtonsCount.BGM);
                break;
            case UIButtonsCount.Vibration:
                _container.VibrationController.IsVibrationChanged?.Invoke();
                break;
            case UIButtonsCount.Home:
                _container.LoadingScreen.OnChangeScene?.Invoke(LevelsKeys.mainMenuLevelKey);
                break;
        }
    }
    private void PlayButtonTapAnimation(Transform transform) => new ButtonTapAnimation().Execute(transform);

}