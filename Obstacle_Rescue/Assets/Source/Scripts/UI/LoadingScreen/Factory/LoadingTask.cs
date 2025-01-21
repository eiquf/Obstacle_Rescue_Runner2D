using System.Threading.Tasks;

public abstract class LoadingTask<T>
{
    public abstract Task Execute(T thing);
}