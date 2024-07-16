using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenFactory : MonoBehaviour
{
    public Action<string> OnChangeScene { get; set; }

    protected Text _loadingText;

    private LoadingScreenLoading _loadingScreen;
    private LoadingScreenAnimation _loadingScreenAnimation;
    private void OnEnable() => OnChangeScene += Loading;
    private void OnDisable() => OnChangeScene -= Loading;
    private void Awake() => ComponentsInitialize();
    private void ComponentsInitialize()
    {
        _loadingScreen = new(this);
        _loadingScreenAnimation = new();

        _loadingText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        gameObject.SetActive(false);
    }
    private void Loading(string sceneName)
    {
        _ = _loadingScreen.Execute(sceneName);
        _ = _loadingScreenAnimation.Execute(_loadingText);
    }
}