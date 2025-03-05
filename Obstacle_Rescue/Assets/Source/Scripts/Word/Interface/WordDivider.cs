using System.Collections.Generic;

public class WordDivider : IWord<List<char>, string>
{
    private readonly List<char> _chars = new();

    public List<char> Execute(string word)
    {
        _chars.AddRange(word);
        return _chars;
    }
}
