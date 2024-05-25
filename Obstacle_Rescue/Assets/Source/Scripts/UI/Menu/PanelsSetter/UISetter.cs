using UnityEngine;
using UnityEngine.AddressableAssets;

public class UISetter : MonoBehaviour
{
    protected const string GAME_LEVEL_NAME = "GameLevel";

    private readonly string[] _panelsNames = new[]
{
        "LevelUI",
        "MainMenuUI"
    };

    private SceneChecker _sceneChecker = new();
    private void OnEnable() => _sceneChecker.OnNotify += ChangeUIPanel;
    private void OnDisable() => _sceneChecker.OnNotify -= ChangeUIPanel;
    private void FixedUpdate() => _sceneChecker.Execute();
    private void ChangeUIPanel()
    {
        if (_sceneChecker.CurrentScene.name != null)
        {
            if (transform.childCount >= 0)
            {
                foreach (Transform child in transform)
                    Destroy(child.gameObject);
            }
            CreatePanels();
        }
    }
    private void CreatePanels()
    {
        if (_sceneChecker.CurrentScene.name == GAME_LEVEL_NAME)
            Addressables.InstantiateAsync(_panelsNames[0], transform);
        else
            Addressables.InstantiateAsync(_panelsNames[1], transform);
    }
}