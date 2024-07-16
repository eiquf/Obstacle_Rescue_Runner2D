using System.Threading.Tasks;

public abstract class LoadingScreen<T>
{
    protected const int _additiveTimeToWait = 1;

    protected float _dotAnimationSpeed = 0.5f;
    protected string[] _loadingDotSequence = { ".", "..", "..." };
    protected int _currentDotIndex = 0;

    public abstract Task Execute(T thing);
}