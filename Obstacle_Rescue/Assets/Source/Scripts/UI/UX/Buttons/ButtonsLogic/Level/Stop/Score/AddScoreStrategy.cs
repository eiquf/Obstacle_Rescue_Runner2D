public class AddScoreStrategy : IScoreStrategy
{
    private readonly int _points;
    public AddScoreStrategy(int points) => _points = points;
    public int CalculateScore(int currentScore) => currentScore + _points;
}
