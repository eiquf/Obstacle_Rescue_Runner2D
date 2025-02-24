using TMPro;
using UnityEngine;

public class LetterCreate : IWord<GameObject, char>
{
    private readonly float _startX = 50f;
    private readonly GameObject _prefab;
    private readonly Player _player;
    public LetterCreate(Player player, GameObject pref)
    {
        _prefab = pref;
        _player = player;
    }
    public GameObject Execute(char symbol)
    {
        float posY = Random.Range(_player.MinY, _player.MaxY);
        Vector2 pos = new(_startX, posY);

        GameObject currentLetter = Object.Instantiate(_prefab, pos, Quaternion.identity);
        TextMeshProUGUI text = currentLetter.GetComponentInChildren<TextMeshProUGUI>();
        text.text = symbol.ToString();

        return currentLetter;
    }
}
