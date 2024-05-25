using System;
using UnityEngine.SceneManagement;

public class SceneChecker : IMenu
{
    public event Action OnNotify;

    private bool _isSetted = false;
    public Scene CurrentScene { get; private set; }

    public void Execute()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (_isSetted == false)
        {
            CurrentScene = currentScene;
            OnNotify?.Invoke();
            _isSetted = true;
        }

        if (currentScene != CurrentScene)
        {
            OnNotify?.Invoke();
            CurrentScene = currentScene;
        }
    }
}