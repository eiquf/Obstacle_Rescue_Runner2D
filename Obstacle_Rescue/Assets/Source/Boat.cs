using System;
using UnityEngine;
using Zenject;

public class Boat : MonoBehaviour
{
    [Inject] Player _player;
    public Transform player; // �����, ������� ����� ��������� � ������
    public Transform targetPoint; // �����, �� ������� ����� ������ ���������
    public float moveSpeed = 5f; // �������� �������� �����

    private bool isPlayerInBoat = false; // ����, �����������, ��� ����� � �����

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
        // ������� ����� � ����
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

        // ���� ����� �������� ����, ������������� ��������
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // ���������� ����� ��� ��������� ������ �������� �� ��������
            isPlayerInBoat = false; // ������: ����� ������ �� ��������
            // �������������� ��������, ���� �����
        }
    }

    // ���� ����� ����������, ����� ����� ������� � �����
    public void EnterBoat()
    {
        isPlayerInBoat = true;
        // �������������� ��������, ���� �����, ��������, ��������� ������ � �����
        player.SetParent(transform); // ����������� ������ � �����, ���� �����
    }

    // ���� ����� ����������, ����� ����� ������� �� �����
    public void ExitBoat()
    {
        isPlayerInBoat = false;
        // �������������� ��������, ���� �����, ��������, ��������� ������ �� �����
        player.SetParent(null); // ��������� ������ �� �����
    }
}
