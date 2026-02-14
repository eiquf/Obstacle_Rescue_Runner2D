using System.Collections.Generic;

public class WordPicker : IWord<string, List<string>>
{
    public string Execute(List<string> elements)
    {
        string word = elements[UnityEngine.Random.Range(0, elements.Count)];
        elements.Remove(word);
        return word;
    }
}
