using System;
using UnityEngine;

[Serializable]
public class ButtonActionsFactory
{
    private InjectContainer _container;
    private Transform[] _createPos;

    private MenuActions _menuActions;
    private readonly SocialMediaActions _socialMediaActions = new();
    public void SetComponents(InjectContainer container, Transform[] createPos)
    {
        _container = container;
        _createPos = createPos;

        _menuActions = new MenuActions(_container, _createPos);
    }
    public IButtonAction GetMenuButtonAction(int index, string sceneName) => 
        sceneName == "Menu" 
        ? _menuActions.GetMainMenuAction(index) 
        : _menuActions.GetOtherMenuAction(index);

    public IButtonAction GetSMButtonAction(int index) =>
        _socialMediaActions.GetSocialMediaAction(index);
}
public interface IButtonAction
{
    void Execute();
}