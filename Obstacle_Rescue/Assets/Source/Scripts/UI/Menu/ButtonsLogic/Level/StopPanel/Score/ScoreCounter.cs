using UnityEngine;
using Zenject;

public class ScoreCounter : MonoBehaviour
{
    //[Inject] private Player _playerMove;
    public int distance { get;  private set; }
    private void FixedUpdate()
    {
        //if (_playerMove != null) distance += (int)(_playerMove.Velocity.x * Time.fixedDeltaTime);
    }
}