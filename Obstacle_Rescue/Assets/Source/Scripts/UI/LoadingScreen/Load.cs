public class Load : IButtonAction
{
    private readonly LoadingScreen _loadingScreen;
    public Load(LoadingScreen screen) => _loadingScreen = screen;
    public void Execute() => _loadingScreen.OnChangeScene?.Invoke();
}