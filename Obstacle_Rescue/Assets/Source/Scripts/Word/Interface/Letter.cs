using System.Collections.Generic;

public class Letter : IWord<char, List<char>>
{
    public char Execute(List<char> chars)
    {
        char firstChar = chars[0]; // Сначала сохраняем букву
        chars.RemoveAt(0); // Удаляем букву из списка
        return firstChar;
    }
}