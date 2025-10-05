using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public Transform player;            // ссылка на игрока
    public float patrolSpeed = 2f;      // скорость патрулирования
    public float chaseSpeed = 4f;       // скорость погони
    public float attackDistance = 3f;   // дистанция атаки
    public float chaseDistance = 10f;   // дистанция преследования

    private enum State { Patrol, Chase, Attack } // перечисление состояний
    private State currentState = State.Patrol;   // текущее состояние

    void Update()
    {
        // считаем расстояние до игрока
        float distance = Vector3.Distance(transform.position, player.position);

        // переключение между состояниями
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
        Debug.Log("Атака!");
    }
}
