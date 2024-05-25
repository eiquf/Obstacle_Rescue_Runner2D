using UnityEngine;

public sealed class MenuController : MonoBehaviour
{
    private const bool _canSpawnHome = false;
    [SerializeField] private Transform _buttonsPanelPos;
    [SerializeField] private Transform[] _creatPos;

    private InjectContainer _container;
    private void Awake()
    {
        _container = FindFirstObjectByType<InjectContainer>();

        ButtonClickHandler buttonClickHandler = new(_buttonsPanelPos, _creatPos, _container, _canSpawnHome);
        buttonClickHandler.Execute();
    }
}