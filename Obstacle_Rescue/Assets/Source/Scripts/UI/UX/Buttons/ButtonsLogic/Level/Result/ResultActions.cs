public class ResultActions
{
    private readonly Reload _reload = new();
    private readonly Load _load;
    public ResultActions(InjectContainer container) => _load = new(container.LoadingScreen);
    public IButtonAction GetAction(int index)
    {
        return index switch
        {
            1 => _reload,
            0 => _load,
            _ => null
        };
    }
}