using UnityEngine;

public sealed class Preferences : IUIPanelsInstantiate
{
    private bool _isFirstTap = false;
    private bool _buttonsActivated;

    private readonly bool _canSpawnHomeButton;
    private readonly bool _isUpsideDown;
    private readonly InjectContainer _inject;

    private PrefButtonsCreate _prefButtonsCreate;

    public Preferences(bool isUpsideDown, InjectContainer inject, bool canSpawnHome)
    {
        _isUpsideDown = isUpsideDown;
        _inject = inject;
        _canSpawnHomeButton = canSpawnHome;
    }

    public void Execute(Transform transform)
    {
        if (_isFirstTap)
            TogglePreferences();
        else
            InitializeComponents(transform);
    }

    private void InitializeComponents(Transform pos)
    {
        _prefButtonsCreate = new PrefButtonsCreate(_canSpawnHomeButton, pos, _inject);
        _prefButtonsCreate.Execute();
        _isFirstTap = true;
    }

    private void TogglePreferences()
    {
        _buttonsActivated = !_buttonsActivated;
        UpdateButtonsAnimation(_buttonsActivated);
    }

    private void UpdateButtonsAnimation(bool activate) =>
        new PreferencesAnimation
        (_prefButtonsCreate.ButtonsRectTransform,
            _isUpsideDown,
            activate)
        .Execute();
}