using UnityEngine;
using Zenject;

public sealed class Cheker : MonoBehaviour
{
    [Inject] private Player _playerMove;
    private void FixedUpdate()
    {
        if (_playerMove != null)
            transform.position = _playerMove.transform.position;
        else Destroy(gameObject);
    }
}