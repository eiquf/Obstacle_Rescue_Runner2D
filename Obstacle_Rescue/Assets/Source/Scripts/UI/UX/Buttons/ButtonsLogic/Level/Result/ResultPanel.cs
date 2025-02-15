using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResultPanel : MonoBehaviour, IPlayerObserver
{
    [SerializeField] private Text _scoreText, _titleText, _moneyText;
    private readonly string[] _supportTitle = new string[] { "Great!", "Wow!", "Nice!", "Perfect!", "Good!" };

    private Transform _buttonsPanelTransform;
    private Button[] _buttons;

    [SerializeField] private ScoreCounter _scoreCounter;
    private InjectContainer _container;

    private readonly AnimationContext _animationContext = new();
    private readonly ButtonsActions _actions = new();

    [Inject]
    private void Construct(InjectContainer container, Player player)
    {
        _container = container;
        player.AddObserver(this);
    }
    private void Start()
    {
        _buttonsPanelTransform = transform.GetChild(0).GetChild(1);
        _animationContext.SetAnimationStrategy(new ButtonTapAnimation());

        AccessButtons();
        gameObject.SetActive(false);
        EventBus.RaiseGameStopped(false);
    }
    private void Results()
    {
        gameObject.SetActive(true);
        _titleText.name = Title();
        _scoreCounter.OnShow?.Invoke(_scoreText);
        EventBus.RaiseGameStopped(true);
    }
    private string Title()
    {
        int random = Random.Range(0, _supportTitle.Length);
        return _supportTitle[random];
    }
    private void AccessButtons()
    {
        _buttons = new Button[_buttonsPanelTransform.childCount];
        _buttons = _buttonsPanelTransform.GetComponentsInChildren<Button>();

        for (int i = 0; i < _buttons.Length; i++)
        {
            int index = i;
            _buttons[i].onClick.AddListener(() => { OnButtonClick(index); _animationContext.PlayAnimation(_buttons[index].transform); });
        }
    }
    private void OnButtonClick(int index)
    {
        IButtonAction action = _actions.GetResultActions(index, _container);
        action?.Execute();
    }

    public void OnNotify(PlayerStates state)
    {
        if (state == PlayerStates.Dead) Results();
    }
}
