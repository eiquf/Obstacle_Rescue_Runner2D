using System;
using UnityEngine;
using Zenject;

public class Boat : MonoBehaviour
{
    [Inject] Player _player;
    public Transform player; // »грок, который будет двигатьс€ с лодкой
    public Transform targetPoint; // “очка, до которой лодка должна двигатьс€
    public float moveSpeed = 5f; // —корость движени€ лодки

    private bool isPlayerInBoat = false; // ‘лаг, указывающий, что игрок в лодке

    private void Start()
    {
        player = _player.transform;
    }
    private void Update()
    {
        if (isPlayerInBoat)
        {
            MoveBoat();
        }
    }

    private void MoveBoat()
    {
        // ƒвигаем лодку к цели
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

        // ≈сли лодка достигла цели, останавливаем движение
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // ќстановить лодку или выполнить другие действи€ по прибытии
            isPlayerInBoat = false; // ѕример: лодка больше не движетс€
            // ƒополнительные действи€, если нужно
        }
    }

    // Ётот метод вызываетс€, когда игрок садитс€ в лодку
    public void EnterBoat()
    {
        isPlayerInBoat = true;
        // ƒополнительные действи€, если нужно, например, прив€зать игрока к лодке
        player.SetParent(transform); // ѕрив€зываем игрока к лодке, если нужно
    }

    // Ётот метод вызываетс€, когда игрок выходит из лодки
    public void ExitBoat()
    {
        isPlayerInBoat = false;
        // ƒополнительные действи€, если нужно, например, отпустить игрока от лодки
        player.SetParent(null); // ќтпускаем игрока от лодки
    }
}
