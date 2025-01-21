using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResultPanelGenerator /*: IUIPanelsInstantiate*/
{
    private const string _panelName = "";
    private Transform _spawnTransform;
    private LoadingScreen _loadingScreen;
    private ScoreCounter _scoreCounter;
    public ResultPanelGenerator(Transform spawnTransform, LoadingScreen loadingScreen, ScoreCounter scoreCounter)
    {
        _spawnTransform = spawnTransform;
        _loadingScreen = loadingScreen;
        _scoreCounter = scoreCounter;
    }
    //public void Execute() => LoadPanel();

    //public void Execute(Transform transform)
    //{
    //    throw new System.NotImplementedException();
    //}

    //private void LoadPanel()
    //{
    //    Addressables.InstantiateAsync(_panelName, _spawnTransform).Completed += handle =>
    //    {
    //        ResultPanel resultPanel = handle.Result.GetComponent<ResultPanel>();
    //        resultPanel.Inject(_scoreCounter, _loadingScreen);
    //    };
    //}
}