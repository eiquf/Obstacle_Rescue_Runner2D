using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEditor.Progress;

public class WordController : MonoBehaviour, IPlayerObserver
{
    #region Components
    private readonly IWord<string, string[]> _wordPicker = new WordPicker();
    private readonly IWord<char, List<char>> _letter = new Letter();
    private IWord<Transform, Transform> _letterMovement;
    private IWord<GameObject, char> _letterCreate;
    #endregion
    public bool _colledted;

    private AnimationContext _anim = new();
    [Inject] private readonly Player _player;
    [SerializeField] private GroundGenerator _groundGenerator;

    private readonly string[] WordsToPick = new string[]
    {
        "BRIDGE", "BUCKET", "BOAT"
    };
    [SerializeField] private List<char> _chars = new();
    [SerializeField] private GameObject _pref;
    private GameObject _letterObj;
    [SerializeField] private char _currentLetter;
    private string _currentWord;

    private void OnEnable() => _player.AddObserver(this);
    private void OnDisable() { if (_letter is Letter letterInstance) letterInstance.OnLastLetterUsed -= HandleLastLetterUsed; }
    private void Start()
    {
        Initialize();
        NewWord();

        if (_currentLetter != default)
            InvokeRepeating(nameof(Create), 1f, 15f);
    }

    private void FixedUpdate()
    {
        if (_letterObj != null) Movement();

        if (_colledted == true)
        {
            HandleLastLetterUsed();
            _colledted = false;
        }
    }

    private void Initialize()
    {
        _anim.SetAnimationStrategy(new ObtainAnimation());
        _letterCreate = new LetterCreate(_player, _pref);
        _letterMovement = new LetterMovement(_player);

        if (_letter is Letter letterInstance)
            letterInstance.OnLastLetterUsed += HandleLastLetterUsed;
    }
    private void Create() => _letterObj = _letterCreate.Execute(_currentLetter);
    private void Movement() => _letterObj.transform.Translate(_player.Velocity.x * Time.fixedDeltaTime * Vector2.left);
    private void NewWord()
    {
        _currentWord = _wordPicker.Execute(WordsToPick);

        _chars.Clear();
        _chars.AddRange(_currentWord);

        if (_chars.Count > 0)
            _currentLetter = _letter.Execute(_chars);
    }
    private void HandleLastLetterUsed() => _groundGenerator.OnObstacleChunk?.Invoke(_currentWord);
    public void OnNotify(PlayerStates state)
    {
        if (state == PlayerStates.Letter)
        {
            _anim.PlayAnimation(_letterObj.transform);
            _currentLetter = _letter.Execute(_chars);
        }
    }
}
public class ObtainAnimation : IAnimation
{
    private readonly float _moveDuration = 0.5f;
    private readonly float _heightOffset = -1.5f;
    private readonly float _waitBeforeFade = 2.5f;
    private readonly float _fadeDuration = 0.5f;

    public void PlayAnimation(Transform transform)
    {
        Camera cam = Camera.main;
        Vector3 screenTopCenter = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, cam.transform.position.z * -1));
        screenTopCenter.y += _heightOffset;
       
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(screenTopCenter, _moveDuration).SetEase(Ease.OutBack));
        sequence.AppendInterval(_waitBeforeFade);
        sequence.Append(transform.DOScale(Vector3.zero, _fadeDuration).SetEase(Ease.InBack))
                .OnComplete(() => Object.Destroy(transform.gameObject));
    }
}