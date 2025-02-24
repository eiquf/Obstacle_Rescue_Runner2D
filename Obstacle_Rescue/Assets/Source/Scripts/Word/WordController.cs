using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WordController : MonoBehaviour, IPlayerObserver
{
    #region Components
    private readonly IWord<string, string[]> _wordPicker = new WordPicker();
    private readonly IWord<List<char>, string> _wordDivider = new WordDivider();
    private readonly IWord<char, List<char>> _letter = new Letter();
    private IWord<Vector2, Transform> _letterMovement;
    private IWord<GameObject, char> _letterCreate;
    #endregion

    [Inject] private readonly Player _player;
    private GroundGenerator _groundGenerator;

    private readonly string[] WordsToPick = new string[]
    {
        "BRIDGE", "BUCKET", "BOAT"
    };
    [SerializeField] private List<char> _chars = new();
    [SerializeField] private GameObject _pref;
    private GameObject _letterObj;
    [SerializeField] private char _currentLetter;

    private void Start()
    {
        Initialize();
        NewWord();
        Invoke(nameof(Create), 2f);
        InvokeRepeating(nameof(Create), 10f, 10f); // Вызывает метод Create каждые 10 сек
    }

    private void FixedUpdate()
    {
        if (_letterObj != null) Movement();
    }

    private void Initialize()
    {
        _letterCreate = new LetterCreate(_player, _pref);
        _letterMovement = new LetterMovement(_player);
    }
    private void Create() => _letterObj = _letterCreate.Execute(_currentLetter);
    private void Movement() => _letterObj.transform.position = _letterMovement.Execute(_letterObj.transform);
    private void NewWord()
    {
        string word = _wordPicker.Execute(WordsToPick);
        _chars = new List<char>(word);

        if (_chars.Count > 0)
            _currentLetter = _letter.Execute(_chars);
    }

    public void OnNotify(PlayerStates state)
    {
        if (state == PlayerStates.Letter)
        {
            _currentLetter = _letter.Execute(_chars);

        }
    }
}