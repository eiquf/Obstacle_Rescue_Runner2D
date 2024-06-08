using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenFactory : MonoBehaviour
{
    public Action<string> OnChangeScene { get; set; }

    protected Text _loadingText;

    private void OnEnable() => OnChangeScene += Loading;
    private void OnDisable() => OnChangeScene -= Loading;
    private void Awake() => ComponentsInitialize();
    private void ComponentsInitialize()
    {
        _loadingText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        gameObject.SetActive(false);
    }
    private void Loading(string sceneName)
    {
        _ = new LoadingScreenLoading(sceneName).Execute();
        _ = new LoadingScreenAnimation(_loadingText).Execute();
    }
}