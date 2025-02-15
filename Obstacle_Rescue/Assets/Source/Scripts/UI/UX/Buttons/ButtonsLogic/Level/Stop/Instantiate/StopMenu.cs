using UnityEngine;

public class StopMenu : IButtonAction
{
    private bool _isGameStopped;
    private readonly Preferences _preferences;
    private readonly NewsPanel _newsPanel;
    private readonly Transform _stop;

    public StopMenu(Transform stopMenuTransform, Transform preferencesTransform)
    {
        _stop = stopMenuTransform;
        _newsPanel = new NewsPanel(_stop.GetChild(0), _stop.GetChild(1));
        _preferences = new Preferences(preferencesTransform);
    }

    public void Execute()
    {
        _preferences.Execute();
        _isGameStopped = !_isGameStopped;
        _stop.gameObject.SetActive(_isGameStopped);
        _newsPanel.Toggle(_isGameStopped);
        EventBus.RaiseGameStopped(_isGameStopped);
    }
}