using System;
using System.Collections.Generic;

public class Letter : IWord<char, List<char>>
{
    public event Action OnLastLetterUsed;
    public char Execute(List<char> chars)
    {
        if (chars.Count == 0) return default;

        int index = UnityEngine.Random.Range(0, chars.Count);
        char firstChar = chars[index];
        chars.RemoveAt(index);

        if (chars.Count == 0)
            OnLastLetterUsed?.Invoke();

        return firstChar;
    }
}