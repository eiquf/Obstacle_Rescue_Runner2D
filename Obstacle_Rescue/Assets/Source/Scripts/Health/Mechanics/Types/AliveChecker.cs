public class AliveChecker : Health
{
    private readonly PlayerDeath _playerDeath = new();
    public override void Execute()
    {
        if (hurts.Count == _livesSettings.MinLives)
            _playerDeath.Execute();
    }
}