using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Linq;

public class PrefButtonsCreate : IPreferences
{
    private readonly Transform _preferencesCreatePos;
    public RectTransform[] ButtonsRectTransform { get; private set; }
    private Button[] _buttons;
    private Image[] _images;

    private bool _showHomeButton;

    private readonly InjectContainer _container;

    private readonly List<string> _buttonsReferences = new List<string>
    {
        "VibrationButton",
        "SFXButton",
        "BGMButton",
        "HomeButton"
    };
    private readonly AnimationContext _animationContext = new();

    public PrefButtonsCreate(Transform preferencesCreatePos, InjectContainer container)
    {
        _preferencesCreatePos = preferencesCreatePos;
        _container = container;

        _showHomeButton = SceneManager.GetActiveScene().name != "Menu";

        if (!_showHomeButton)
            _buttonsReferences.Remove("HomeButton");
    }

    public void Execute() => ButtonsPrefs();

    private void ButtonsPrefs()
    {
        _buttons = new Button[_buttonsReferences.Count];

        for (int i = 0; i < _buttonsReferences.Count; i++)
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

                    _animationContext.SetAnimationStrategy(new ButtonTapAnimation());
                    _animationContext.PlayAnimation(button.transform);
                });

                if (_buttons.All(b => b != null))
                    FinalizeInitialization();
            }
            Addressables.Release(handle);
        };
    }

    private void FinalizeInitialization()
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