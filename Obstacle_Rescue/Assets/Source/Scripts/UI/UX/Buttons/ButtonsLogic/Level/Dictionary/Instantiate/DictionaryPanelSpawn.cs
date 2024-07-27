using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class DictionaryPanelSpawn : IUIPanelsInstantiate
{
    private const string NAME = "DictionaryPanel";
    private const int MINTAPS = 0;

    private bool _isFirstActivation = false;
    private int _currentTapsCount = 6;
    private Transform _spawnPos;
    private GameObject _pref;
    private Text _text;
    private Button _button;

    private readonly Transform _buttonsPanelPos;
    private readonly InjectContainer _container;

    public DictionaryPanelSpawn(InjectContainer container, Transform buttonsPanelPos)
    {
        _container = container;
        _buttonsPanelPos = buttonsPanelPos;
    }

    public void Execute(Transform transform)
    {
        if (_currentTapsCount == MINTAPS) return;

        _spawnPos = transform;
        _currentTapsCount--;

        if (!_isFirstActivation)
        {
            LoadPanel();
            _text = _buttonsPanelPos.GetChild(1).GetChild(0).GetComponent<Text>();
            _button = _buttonsPanelPos.GetChild(1).GetComponent<Button>();
            _isFirstActivation = true;
        }
        else
            TogglePanel();

        UpdateTaps();
    }

    private void LoadPanel() => Addressables.LoadAssetAsync<GameObject>(NAME).Completed += OnLoadDone;
    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            _pref = Object.Instantiate(obj.Result, _spawnPos);
            _pref.SetActive(true);
        }
    }

    private void UpdateTaps()
    {
        //_container.SoundController.IsSoundPlay?.Invoke(SoundsCount.DICTIONARY);
        _text.text = "x" + (_currentTapsCount - 3);

        //if (_currentTapsCount == 0)
    }

    private void TogglePanel()
    {
        if (_pref != null)
            _pref.SetActive(!_pref.activeSelf);
    }
}
