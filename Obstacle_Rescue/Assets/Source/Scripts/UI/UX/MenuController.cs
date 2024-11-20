using UnityEngine;

public sealed class MenuController : MonoBehaviour
{
    [SerializeField] private Transform _buttonsPanelPos;
    [SerializeField] private Transform[] _creatPos;

    private GameObject _attenuation;

    private InjectContainer _container;
    private void Awake()
    {
        _container = FindFirstObjectByType<InjectContainer>();
        ButtonInitialize();
    }
    private void ButtonInitialize()
    {
        ButtonClickHandler buttonClickHandler = new(_buttonsPanelPos, _creatPos, _container);
        buttonClickHandler.Execute();
    }
}