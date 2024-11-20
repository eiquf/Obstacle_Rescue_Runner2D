using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Preferences : IButtonAction
{
    private readonly PrefButtonsCreate _prefButtonsCreate;
    private readonly bool _isUpsideDown;
    private bool _buttonsActivated;

    public Preferences(InjectContainer inject, Transform spawnPosition)
    {
        _prefButtonsCreate = new PrefButtonsCreate(spawnPosition, inject);
        _prefButtonsCreate.Execute();

        bool isMenuScene = SceneManager.GetActiveScene().name == "Menu";
        _isUpsideDown = !isMenuScene;
        _buttonsActivated = !isMenuScene;
    }

    public void Execute()
    {
        _buttonsActivated = !_buttonsActivated;
        new PreferencesAnimation(
            _prefButtonsCreate.ButtonsRectTransform,
            _isUpsideDown,
            _buttonsActivated
        ).Execute();
    }
}