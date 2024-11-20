using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions
{
    private readonly Play _play;
    private readonly Preferences _preferences;
    private readonly EducationPanelSpawn _educPanelSpawn;
    private readonly StopMenuPanelActivator _stop;
    private readonly DictionaryPanelSpawn _dictionary;

    public MenuActions(InjectContainer container, Transform[] createPos)
    {
        _play = new Play(container);

        if (SceneManager.GetActiveScene().name == "Menu")
            _preferences = new Preferences(container, createPos[0]);

        _educPanelSpawn = new EducationPanelSpawn(createPos[1]);

        _stop = new StopMenuPanelActivator(createPos[0]);
        _dictionary = new DictionaryPanelSpawn(createPos[1]);
    }

    public IButtonAction GetMainMenuAction(int index)
    {
        return index switch
        {
            UIButtonsCount.Play => _play,
            UIButtonsCount.Preferences => _preferences,
            UIButtonsCount.Education => _educPanelSpawn,
            _ => null
        };
    }

    public IButtonAction GetOtherMenuAction(int index)
    {
        return index switch
        {
            UIButtonsCount.Stop => _stop,
            UIButtonsCount.Dictionary => _dictionary,
            _ => null
        };
    }
}