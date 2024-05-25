using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenFactory : MonoBehaviour
{
    public Action<string> OnChangeScene { get; set; }

    protected Text _loadingText;

    private LoadingScreenAnimation _screenAnimation;
    private LoadingScreenLoading _loading;

    private void OnEnable() => OnChangeScene += Loading;
    private void OnDisable() => OnChangeScene -= Loading;
    private void Awake() => ComponentsInitialize();
    private void ComponentsInitialize()
    {
        _loadingText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        _screenAnimation = new(_loadingText);
        gameObject.SetActive(false);
    }
    private void Loading(string sceneName)
    {
        _loading = new(sceneName);
        StartCoroutine(_loading.Execute());
        StartCoroutine(_screenAnimation.Execute());
    }
}