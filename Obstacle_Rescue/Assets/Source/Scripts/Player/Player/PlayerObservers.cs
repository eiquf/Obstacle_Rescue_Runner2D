using UnityEngine;
using Zenject;

public class PlayerObservers : MonoBehaviour
{
    [SerializeField] private GameObject[] _observers;

    [Inject] private Player _player;
    void Start()
    {
        foreach (var observer in _observers)
        {
            var obj = observer.GetComponent<IPlayerObserver>();
            _player.AddObserver(obj);
        }
    }
}
