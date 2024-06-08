using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class DictionaryPanelSpawn : IUIPanelsInstantiate
{
    private const string NAME = "DictionaryPanel";

    private bool _isFirstActivation = false;

    private const int MINTAPS = 0;
    private int _currentTapsCount = 3;

    private string _textOfTheButton;
    private Transform _spawnPos;

    private Text _text;
    private readonly Transform _buttonsPanelPos;
    private readonly InjectContainer _container;

    public DictionaryPanelSpawn(InjectContainer container, Transform buttonsPanelPos)
    {
        _container = container;
        _buttonsPanelPos = buttonsPanelPos;
    }
    public void Execute(Transform transform)
    {
        if (_currentTapsCount != MINTAPS)
        {
            _spawnPos = transform;
            _currentTapsCount--;
            LoadPanel();
            Text();
        }

        if (!_isFirstActivation)
        {
            _text = _buttonsPanelPos.GetChild(1).GetChild(0).GetComponent<Text>();
            _isFirstActivation = true;
        }
    }
    private void LoadPanel() => Addressables.LoadAssetAsync<GameObject>(NAME).Completed += OnLoadDone;

    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject loadedPrefab = obj.Result;
            Object.Instantiate(loadedPrefab, _spawnPos);
        }
        else
            Debug.LogError("Failed to load addressable: " + NAME);
    }
    private void Text()
    {
        //_container.SoundController.IsSoundPlay?.Invoke(SoundsCount.DICTIONARY);

        _textOfTheButton = "x" + _currentTapsCount;
        _text.text = _textOfTheButton;
    }
}