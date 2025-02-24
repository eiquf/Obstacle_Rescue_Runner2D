using System.Collections.Generic;

public class WordPicker : IWord<string, string[]>
{
    private List<string> _currentWords = new();
    public string Execute(string[] elements)
    {
        if (_currentWords.Count == 0) _currentWords.AddRange(elements);

        string word = _currentWords[UnityEngine.Random.Range(0, _currentWords.Count)];
        _currentWords.Remove(word);
        return word;
    }
}
