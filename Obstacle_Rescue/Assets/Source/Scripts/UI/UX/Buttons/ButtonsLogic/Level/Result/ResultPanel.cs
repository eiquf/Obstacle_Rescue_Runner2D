using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    private readonly AnimationContext _animationContext = new();

    private const int HomeIndex = 0;

    private int _score;

    private readonly string[] _supportTitle = new string[] { "Great!", "Wow!", "Nice!", "Perfect!", "Good!" };

    private Transform _buttonsPanelTransform;
    private Text _scoreText, _titleText, _moneyText;

    private Button[] _buttons;

    private LoadingScreenFactory _lvlChanger;
    private ScoreCounter _scoreCounter;


    public void Inject(ScoreCounter scoreCounter, LoadingScreenFactory loadingScreen)
    {
        _lvlChanger = loadingScreen;
        _scoreCounter = scoreCounter;
    }
    private void Awake()
    {
        _buttonsPanelTransform = transform.GetChild(0).GetChild(1);
        TextComponentsInitialization();
    }
    private void Start()
    {
        AccessButtons();
        Results();
    }
    private void TextComponentsInitialization()
    {
        _titleText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        _moneyText = transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>();
        _scoreText = transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Text>();
    }
    private void Results()
    {
        Title();
        _scoreText.text = _scoreCounter.distance.ToString();
        MoneyResult();
    }
    private void Title()
    {
        int random = Random.Range(0, _supportTitle.Length);
        _titleText.text = _supportTitle[random];
    }
    private void MoneyResult()
    {
        _score = _scoreCounter.distance / 500;
        _moneyText.text = _score.ToString();
    }
    private void AccessButtons()
    {
        _buttons = new Button[_buttonsPanelTransform.childCount];
        _buttons = _buttonsPanelTransform.GetComponentsInChildren<Button>();

        for (int i = 0; i < _buttons.Length; i++)
        {
            int index = i;
            _buttons[i].onClick.AddListener(() => { OnAddedButtonClick(index); ButtonsTapAnimation(_buttons[index].transform); });
        }
    }
    private void OnAddedButtonClick(int index)
    {
        if (index == HomeIndex) Home();
        else Restart();
    }
    private void ButtonsTapAnimation(Transform transform)
    {
        _animationContext.SetAnimationStrategy(new ButtonTapAnimation());
        _animationContext.PlayAnimation(transform);
    }
    #region Buttons logic
    private void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    private void Home() => _lvlChanger.OnChangeScene?.Invoke(LevelsKeys.mainMenuLevelKey);
    #endregion
}