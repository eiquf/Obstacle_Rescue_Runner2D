using UnityEngine;

public class ButtonsActions
{
    private MenuActions _menuActions;
    private PreferencesActions _prefs;
    private readonly SocialMediaActions _socialMediaActions = new();
    public IButtonAction GetMenuButtonAction(int index, Transform[] createPos, InjectContainer container)
    {
        _menuActions ??= new(container, createPos);
        return _menuActions.GetAction(index);
    }

    public IButtonAction GetSMButtonAction(int index) =>
        _socialMediaActions.GetSocialMediaAction(index);

    public IButtonAction GetPreferencesAction
        (int index,
        InjectContainer container, 
        BGM bgm, 
        SFX sfx,
        Vibration vibration)
    {
        _prefs ??= new(container, vibration, sfx, bgm);
        return _prefs.GetPrefAction(index);
    }
}
public interface IButtonAction
{
    void Execute();
}