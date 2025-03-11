using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WordPanel : MonoBehaviour
{
    public Action<DraggableLetter> OnRemoved;
    public Action<string, int, Sprite> OnAdded;

    [SerializeField] private DraggableLetter _letter;
    [SerializeField] private DropSlot _slot;
    [SerializeField] private WordController _controller;
    [SerializeField] private RectTransform _leftPanel;
    [SerializeField] private RectTransform _rightPanel;
    [SerializeField] private TextMeshProUGUI _timeText;
    [Inject] Health _health;
    [Inject] Player _player;

    private GroundTrap trap;
    private List<DraggableLetter> _letters;

    [SerializeField] private string _word;
    [SerializeField] private int _timer;

    private Image _image;
    private Coroutine _timerCoroutine;
    private Transform _slotsPos;

    private void Start()
    {
        OnRemoved += RemoveOrder;
        OnAdded += Create;

        Initialize();
    }

    private void OnDisable()
    {
        OnRemoved -= RemoveOrder;
        OnAdded -= Create;
    }
    private void Initialize()
    {
        trap = null;
        _image = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        _slotsPos = transform.GetChild(1).GetChild(3);

        gameObject.SetActive(false);
    }
    public void Create(string word, int time, Sprite sprite)
    {
        if (gameObject.activeSelf) return; // Если панель уже активна, не создаем новое слово

        _letters = new List<DraggableLetter>();

        _image.sprite = sprite;
        _word = word;
        _timer = time;

        SpawnLetters();
        SpawnSlots();
    }

    public void RemoveOrder(DraggableLetter letter)
    {
        DraggableLetter foundLetter = _letters.Find(l => l.name == letter.name);
        if (foundLetter != null)
        {
            _letters.Remove(foundLetter);

            if (_letters.Count <= 0)
            {
                foreach (var component in _slotsPos.GetComponentsInChildren<DropSlot>())
                {
                    Destroy(component.gameObject);
                }

                _controller.OnComplete?.Invoke();

                // Останавливаем таймер и деактивируем панель
                if (_timerCoroutine != null)
                {
                    StopCoroutine(_timerCoroutine);
                    _timerCoroutine = null;
                }

                gameObject.SetActive(false);
                trap.SetObj(); // Убедимся, что trap не null
                _player.Animation.IsStop?.Invoke(false);
            }
        }
    }

    public void Remove() => _health._removeHealthSystem?.Execute();
    public void Activate(GroundTrap trap)
    {
        _player.Animation.IsStop?.Invoke(true);

        this.trap = trap;
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        _timerCoroutine = StartCoroutine(TimerCountdown());
    }
    private void SpawnSlots()
    {
        foreach (char symbol in _word)
        {
            DropSlot slot = Instantiate(_slot, _slotsPos);
            slot.name = symbol.ToString();
        }
    }
    private void SpawnLetters()
    {
        for (int i = 0; i < _word.Length; i++)
        {
            RectTransform panel = i < _word.Length / 2 ? _leftPanel : _rightPanel;
            DraggableLetter letter = Instantiate(_letter, panel);
            letter.name = _word[i].ToString();
            _letters.Add(letter);
        }
    }
    private IEnumerator TimerCountdown()
    {
        while (_timer >= 0)
        {
            _timeText.text = _timer.ToString();
            yield return new WaitForSeconds(1f);
            _timer--;

            if (_timer <= 0)
            {
                gameObject.SetActive(false);
                _player.Dead();
            }
        }
    }
}