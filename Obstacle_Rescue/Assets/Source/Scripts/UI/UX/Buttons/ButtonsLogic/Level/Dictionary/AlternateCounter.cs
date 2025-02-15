public class AlternateCounter
{
    private int _count = 3;
    private bool _shouldDecrease;
    public int GetCurrent() => _count;
    public string GetText()
    {
        if (_shouldDecrease) _count--;
        _shouldDecrease = !_shouldDecrease;
        return "x" + _count;
    }
}
