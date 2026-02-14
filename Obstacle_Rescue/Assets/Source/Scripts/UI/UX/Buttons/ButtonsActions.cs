using UnityEngine;

public class ButtonsActions
{
    private MenuActions _menu;
    private PreferencesActions _prefs;
    private ResultActions _result;
    private readonly SocialMediaActions _socialMedia = new();
    public IButtonAction GetMenuButtonAction(int index, Transform[] createPos, InjectContainer container)
    {
        _menu ??= new(container, createPos);
        return _menu.GetAction(index);
    }

    public IButtonAction GetSMButtonAction(int index) =>
        _socialMedia.GetSocialMediaAction(index);

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

    public IButtonAction GetResultActions(int index, InjectContainer container)
    {
        _result ??= new(container);
        return _result.GetAction(index);
    }
}
public interface IButtonAction
{
    void Execute();
}