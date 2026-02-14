public class PreferencesActions
{
    private readonly InjectContainer _container;

    private readonly Vibration _vibration;
    private readonly Load _load;
    private readonly SFX _sfx;
    private readonly BGM _bgm;
    public PreferencesActions(InjectContainer container, Vibration vibration, SFX sfx, BGM bgm)
    {
        _container = container;

        _vibration = vibration;
        _bgm = bgm;
        _sfx = sfx;
        _load = new(_container.LoadingScreen);
    }
    public IButtonAction GetPrefAction(int index)
    {
        return index switch
        {
            1 => _sfx,
            0 => _vibration,
            2 => _bgm,
            3 => _load,
            _ => null
        };
    }
}
