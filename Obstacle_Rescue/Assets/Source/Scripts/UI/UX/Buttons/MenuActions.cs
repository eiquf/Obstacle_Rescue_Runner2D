using System.Collections.Generic;
using UnityEngine;

public class MenuActions
{
    private readonly Dictionary<int, IButtonAction> _buttonActions = new();

    public MenuActions(InjectContainer container, Transform[] createPos)
    {
        var sceneChecker = new SceneChecker();
        sceneChecker.Execute();

        if (sceneChecker.CurrentScene.name == "Menu")
        {
            _buttonActions[UIButtonsCount.Play] = new Play(container);
            _buttonActions[UIButtonsCount.Preferences] = new Preferences(createPos[0]);
            _buttonActions[UIButtonsCount.Education] = new EducationPanelSpawn(createPos[1]);
        }
        else
        {
            _buttonActions[UIButtonsCount.Stop] = new StopMenu(createPos[1], createPos[0]);
            _buttonActions[UIButtonsCount.Dictionary] = new Dictionary(createPos[2], createPos[3]);
        }
    }

    public IButtonAction GetAction(int index) => _buttonActions.ContainsKey(index) ? _buttonActions[index] : null;
}
