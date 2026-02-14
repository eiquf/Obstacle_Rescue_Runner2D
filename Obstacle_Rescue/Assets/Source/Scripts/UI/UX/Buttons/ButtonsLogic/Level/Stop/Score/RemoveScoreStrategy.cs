public class RemoveScoreStrategy : IScoreStrategy
{
    private readonly int _points;
    public RemoveScoreStrategy(int points) => _points = points;
    public int CalculateScore(int currentScore)
    {
        if (currentScore <= 0) return 0;
        else return currentScore - _points;
    }
}