using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClickHandler : IMenu
{
    private const bool _isAnimationUpsideDown = false;

    private Button[] _defaultButtons;
    private readonly Transform[] _creatPos;
    private readonly Transform _buttonsPanelPos;

    #region Scripts
    private readonly InjectContainer _container;

    private readonly Play _play;
    private readonly Preferences _preferences;
    private readonly EducationPanelSpawn _educationPanel = new();
    private readonly StopMenuPanelActivator _stopMenuPanelActivator = new();
    private readonly DictionaryPanelSpawn _dictionaryPanelSpawn;

    private ButtonInitializer _buttonInitializer;
    #endregion

    public ButtonClickHandler(Transform buttonsPanelPos, Transform[] creatPos, InjectContainer container, bool canSpawn)
    {
        _creatPos = creatPos;

        _container = container;
        _buttonsPanelPos = buttonsPanelPos;

        _play = new Play(_container);
        _preferences = new Preferences(_isAnimationUpsideDown, _container, canSpawn);
        _dictionaryPanelSpawn = new DictionaryPanelSpawn(_container, _buttonsPanelPos);
    }

    public void Execute()
    {
        _buttonInitializer = new ButtonInitializer(_buttonsPanelPos, OnButtonClick, SoundPlay);
        _defaultButtons = _buttonInitializer.Execute();
    }

    private void SoundPlay() => _container.SoundController.IsSoundPlay?.Invoke(0);
    private void OnButtonClick(int index)
    {
        ButtonsTapAnimation(_defaultButtons[index].transform);

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            switch (index)
            {
                case UIButtonsCount.Play:
                    Play();
                    break;
                case UIButtonsCount.Preferences:
                    Preferences();
                    break;
                case UIButtonsCount.Education:
                    Education();
                    break;
            }
        }
        else
        {
            switch (index)
            {
                case UIButtonsCount.Stop:
                    Stop();
                    break;
                case UIButtonsCount.Dictionary:
                    Dictionary();
                    break;
            }
        }
    }
    #region Buttons Logic 
    private void Play() => _play.Execute();
    private void Preferences() => _preferences.Execute(_creatPos[0]);
    private void Education() => _educationPanel.Execute(_creatPos[1]);
    private void Stop() => _stopMenuPanelActivator.Execute(_creatPos[0]);
    private void Dictionary() => _dictionaryPanelSpawn.Execute(_creatPos[1]);
    #endregion
    private void ButtonsTapAnimation(Transform transform) => new ButtonTapAnimation().Execute(transform);
}