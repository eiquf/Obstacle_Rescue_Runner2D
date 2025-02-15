using UnityEngine;

public sealed class MenuController : MonoBehaviour
{
    [SerializeField] private Transform _buttonsPanelPos;
    [SerializeField] private Transform[] _pos;
    [SerializeField] private GameObject Attenuation;

    private readonly AnimationContext _animationContext = new();

    private InjectContainer _container;
    private PrefButtonsInitialize _prefButtonsCreate;

    private void OnEnable()
    {
        _animationContext.SetAnimationStrategy(new Attenuation(1.5f));
        _animationContext.PlayAnimation(Attenuation.transform);
    }
    private void Start()
    {
        _container = FindAnyObjectByType<InjectContainer>();
        ButtonInitialize();
    }
    private void ButtonInitialize()
    {
        ButtonClickHandler buttonClickHandler = new(_buttonsPanelPos, _pos, _container);
        buttonClickHandler.Execute();

        _prefButtonsCreate = new PrefButtonsInitialize(_pos[0], _container);
        _prefButtonsCreate.Execute();
    }
}