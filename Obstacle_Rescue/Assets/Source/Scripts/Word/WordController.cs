using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WordController : MonoBehaviour, IPlayerObserver
{
    public Action OnComplete { get; private set; }
    #region Components
    private readonly IWord<string, List<string>> _wordPicker = new WordPicker();
    private readonly IWord<char, List<char>> _letter = new Letter();
    private IWord<Transform, Transform> _letterMovement;
    private IWord<GameObject, char> _letterCreate;
    #endregion

    private readonly AnimationContext _anim = new();
    [Inject] private Player _player;
    [SerializeField] private GroundGenerator _groundGenerator;

    private readonly string[] WordsToPick = new string[]
    {
        "BRIDGE", "BUCKET", "BOAT"
    };
    List<string> _words;
    [SerializeField] private List<WordSettings> _wordSettings;
    private readonly Dictionary<string, WordSettings> _wordSettingsDictionary = new();

    [SerializeField] private List<char> _chars = new();
    [SerializeField] private GameObject _pref;
    private GameObject _letterObj;
    [SerializeField] private char _currentLetter;
    [SerializeField] private WordPanel _panel;
    private string _currentWord;
    private void OnEnable()
    {
        _player.AddObserver(this);
        OnComplete += NewWord;
    }
    private void OnDisable()
    {
        if (_letter is Letter letterInstance)
            letterInstance.OnLastLetterUsed -= HandleLastLetterUsed;
        OnComplete -= NewWord;
    }
    private void Start()
    {
        Initialize();
        NewWord();
    }

    private void FixedUpdate()
    {
        if (_letterObj != null) Movement();
    }
    private void Initialize()
    {
        _words = new List<string>();
        _words.AddRange(WordsToPick);

        foreach (var settings in _wordSettings)
            _wordSettingsDictionary.Add(settings.Word, settings);

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
        if (_words.Count <= 0) _words.AddRange(WordsToPick);

        _groundGenerator.ClearJ();
        _currentWord = _wordPicker.Execute(_words);

        _chars.Clear();
        _chars.AddRange(_currentWord);

        if (_chars.Count > 0)
            _currentLetter = _letter.Execute(_chars);

        _player.OnStop?.Invoke(false);

        if (_currentLetter != default)
            InvokeRepeating(nameof(Create), 1f, 9f);
    }
    private void HandleLastLetterUsed()
    {
        CancelInvoke(nameof(Create));
        _groundGenerator.OnObstacleChunk?.Invoke(_currentWord);

        WordSettings settings = _wordSettingsDictionary[_currentWord];
        _panel.Create(settings.Word, settings.Timer, settings.DottedImage);
    }
    public void OnNotify(PlayerStates state)
    {
        if (state == PlayerStates.Letter)
        {
            _anim.PlayAnimation(_letterObj.transform);
            _currentLetter = _letter.Execute(_chars);
        }
    }
}