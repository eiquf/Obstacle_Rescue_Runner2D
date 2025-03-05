using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordPanel : MonoBehaviour
{
    public Action<DraggableLetter> OnRemoved;
    public Action<string, int, Sprite, GroundTrap> OnAdded;

    [SerializeField] private DraggableLetter _letter;
    [SerializeField] private DropSlot _slot;

    private List<DraggableLetter> _letters;

    public float minY = 100f, maxY = 800f;
    private string _word;
    private int _timer;

    private float screenWidth;
    private float leftZoneMin, leftZoneMax, rightZoneMin, rightZoneMax;

    private Image _image;
    private Transform _slotsPos, _lettersPos;
    private GroundTrap _currentGround;

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
        _image = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        _lettersPos = transform.GetChild(1);
        _slotsPos = transform.GetChild(1).GetChild(1);

        screenWidth = Screen.width;

        leftZoneMin = screenWidth * 0.1f;
        leftZoneMax = screenWidth * 0.3f;
        rightZoneMin = screenWidth * 0.7f;
        rightZoneMax = screenWidth * 0.9f;

        gameObject.SetActive(false);
    }
    private void Create(string word, int time, Sprite sprite, GroundTrap trap)
    {
        gameObject.SetActive(true);
        _image.sprite = sprite;
        _word = word;
        _timer = time;
        _currentGround = trap;

        SpawnSlots();
        SpawnLetters();
    }
    private void RemoveOrder(DraggableLetter letter)
    {
        DraggableLetter slotToRemove = _letters.Find(slot => slot.name == letter.name);
        if (slotToRemove != null)
        {
            _letters.Remove(slotToRemove);
            Destroy(slotToRemove.gameObject);

            if (_letters.Count <= 0)
            {
                gameObject.SetActive(false);
                _currentGround.OnEnded?.Invoke();
            }
        }
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
            Vector2 spawnPosition = GetRandomPosition();
            DraggableLetter letter = Instantiate(_letter, _lettersPos);
            letter.GetComponent<RectTransform>().anchoredPosition = spawnPosition;

            letter.GetComponentInChildren<Text>().text = GetRandomLetter();

            _letters.Add(letter);
        }
    }
    private Vector2 GetRandomPosition()
    {
        float x;
        float y = UnityEngine.Random.Range(minY, maxY);

        if (UnityEngine.Random.value > 0.5f) x = UnityEngine.Random.Range(leftZoneMin, leftZoneMax);
        else x = UnityEngine.Random.Range(rightZoneMin, rightZoneMax);

        return new Vector2(x, y);
    }
    private string GetRandomLetter() => _word[UnityEngine.Random.Range(0, _word.Length)].ToString();
}