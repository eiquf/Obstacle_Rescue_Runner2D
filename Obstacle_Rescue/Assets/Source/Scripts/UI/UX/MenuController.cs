using UnityEngine;

public sealed class MenuController : MonoBehaviour
{
    [SerializeField] private Transform _buttonsPanelPos;
    [SerializeField] private Transform[] _creatPos;
    [SerializeField] private GameObject Attenuation;

    private readonly AnimationContext _animationContext = new();

    private InjectContainer _container;
    private void OnEnable()
    {
        _animationContext.SetAnimationStrategy(new Attenuation(1.5f));
        _animationContext.PlayAnimation(Attenuation.transform);
    }
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