public class Play : IMenu
{
    private InjectContainer _container;
    public Play(InjectContainer injector) => _container = injector;
    public void Execute()
    {
        _container.LoadingScreen.gameObject.SetActive(true);
        _container.LoadingScreen.OnChangeScene?.Invoke(LevelsKeys.levelKey);
    }
}