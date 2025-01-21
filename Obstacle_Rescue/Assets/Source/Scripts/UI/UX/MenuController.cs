using UnityEngine;
using Zenject;

public sealed class MenuController : MonoBehaviour
{
    [SerializeField] private Transform _buttonsPanelPos;
    [SerializeField] private Transform[] _creatPos;
    [SerializeField] private GameObject Attenuation;

    private readonly AnimationContext _animationContext = new();

    [Inject] SoundController _soundController;
    private InjectContainer _container;
    private PrefButtonsCreate _prefButtonsCreate;

    private void OnEnable()
    {
        _animationContext.SetAnimationStrategy(new Attenuation(1.5f));
        _animationContext.PlayAnimation(Attenuation.transform);
    }
    private void Start()
    {
        _container = FindFirstObjectByType<InjectContainer>();
        ButtonInitialize();
    }
    private void ButtonInitialize()
    {
        ButtonClickHandler buttonClickHandler = new(_buttonsPanelPos, _creatPos, _container);
        buttonClickHandler.Execute();

        _prefButtonsCreate = new PrefButtonsCreate(_creatPos[0], _container);
        _prefButtonsCreate.Execute();
    }
}