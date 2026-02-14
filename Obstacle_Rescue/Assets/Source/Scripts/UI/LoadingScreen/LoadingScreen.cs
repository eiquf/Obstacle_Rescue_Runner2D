using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Action OnChangeScene { get; set; }

    protected Text _loadingText;

    private readonly AnimationContext _anim = new();
    private readonly LoadScene _loadScene = new();
    private readonly SceneChecker _sceneChecker = new();
    private void OnApplicationQuit() => OnChangeScene -= Loading;
    private void Awake()
    {
        OnChangeScene += Loading;
        ComponentsInitialize();
    }
    private void ComponentsInitialize()
    {
        _loadingText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        _anim.SetAnimationStrategy(new LoadingScreenAnimation(_loadingText));
        gameObject.SetActive(false);
    }
    async void Loading()
    {
        gameObject.SetActive(true); // Включаем экран загрузки

        _sceneChecker.Execute();
        var loadOperation = _loadScene.Execute(_sceneChecker.CurrentScene.name);
        _anim.PlayAnimation(null);

        await loadOperation; // Ждём завершения загрузки сцены

        gameObject.SetActive(false); // Выключаем экран загрузки после окончания
    }

}