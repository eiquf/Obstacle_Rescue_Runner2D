using UnityEngine;
using UnityEngine.AddressableAssets;

public class UISetter : MonoBehaviour
{
    protected const string GAME_LEVEL_NAME = "GameLevel";
    protected const string COMPONENTS_NAME = "ComponentsLoading";

    private readonly string[] _panelsNames = new[]
    {
        "LevelUI",
        "MainMenuUI"
    };

    private SceneChecker _sceneChecker;
    private void OnEnable()
    {
        if (_sceneChecker != null)
            _sceneChecker.OnNotify += ChangeUIPanel;
    }
    private void OnDisable()
    {
        if (_sceneChecker != null)
            _sceneChecker.OnNotify -= ChangeUIPanel;
    }

    private void Awake() => _sceneChecker = new();
    private void FixedUpdate() => _sceneChecker.Execute();
    private void ChangeUIPanel()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        CreatePanels();
    }
    private void CreatePanels()
    {
        if (_sceneChecker.CurrentScene.name != COMPONENTS_NAME)
        {
            string panelName = _sceneChecker.CurrentScene.name == GAME_LEVEL_NAME ? _panelsNames[0] : _panelsNames[1];
            Addressables.InstantiateAsync(panelName, transform);
        }
    }
}