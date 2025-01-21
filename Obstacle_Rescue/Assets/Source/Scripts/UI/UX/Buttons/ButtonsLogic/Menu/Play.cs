public class Play : IButtonAction
{
    private readonly Load _load;
    public Play(InjectContainer injector) => _load = new(injector.LoadingScreen);
    public void Execute() => _load.Execute();
}