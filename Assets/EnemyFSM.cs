using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public Transform player;            // ������ �� ������
    public float patrolSpeed = 2f;      // �������� ��������������
    public float chaseSpeed = 4f;       // �������� ������
    public float attackDistance = 3f;   // ��������� �����
    public float chaseDistance = 10f;   // ��������� �������������

    private enum State { Patrol, Chase, Attack } // ������������ ���������
    private State currentState = State.Patrol;   // ������� ���������

    void Update()
    {
        // ������� ���������� �� ������
        float distance = Vector3.Distance(transform.position, player.position);

        // ������������ ����� �����������
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                if (distance < chaseDistance) currentState = State.Chase;
                break;

            case State.Chase:
                Chase();
                if (distance < attackDistance) currentState = State.Attack;
                else if (distance > chaseDistance) currentState = State.Patrol;
                break;

            case State.Attack:
                Attack();
                if (distance > attackDistance) currentState = State.Chase;
                break;
        }
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * patrolSpeed * Time.deltaTime);
    }

    void Chase()
    {
        transform.LookAt(player);
        transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);
    }

    void Attack()
    {
        Debug.Log("�����!");
    }
}
